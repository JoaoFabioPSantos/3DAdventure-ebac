using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Studio.Core.Singleton;
using Cloth;
public class Player : Singleton<Player>//, IDamageable
{
    public List<Collider> colliders;

    [Header("References")]
    public CharacterController characterController;
    public Animator animator;

    [Header("UI Menu")]
    public GameObject uiMenu;

    [Header("Movement")]
    public float speed = 20f;
    public float speedRun = 1.5f;
    public float turnSpeed = 1f;
    public float gravity = 9.8f;
    public float jumpSpeed = 15f;

    [Header("KeyCodes")]
    public KeyCode jumpKeyCode = KeyCode.Space;
    public KeyCode runKeyCode = KeyCode.LeftShift;

    private float _vsSpeed = 0f;

    [Header("Flash")]
    public List<FlashColor> flashColors;

    [Header("Life")]
    public HealthBase healthBase;
    public GameObject deathImage;

    [Space]
    [SerializeField]private ClothChanger _clothChanger;

    private bool _alive = true;
    private bool _jumping = false;
    private bool _isPaused = false;

    private float _defaultSpeed = 20f;
    private bool _isSpeed = false;

    private void OnValidate()
    {
        if(healthBase== null)healthBase = GetComponent<HealthBase>();
    }

    protected override void Awake()
    {
        base.Awake();
        OnValidate();

        SaveManager.Instance.Load();

        healthBase.OnDamage += Damage;
        healthBase.OnKill += OnKill;
    }

    private void Start()
    {
        GetOnLoad();
        Respawn();
    }

    #region LIFE
    public float GetLife()
    {
        return healthBase.GetCurrentLife();
    }

    public void Damage(HealthBase h)
    {
        flashColors.ForEach(i => i.Flash());
        if (ShakeCamera.Instance != null)ShakeCamera.Instance.Shake();
        if(EffectsManager.Instance!=null)EffectsManager.Instance.ChangeVignette();
    }

    public void Damage(float damage, Vector3 dir)
    {
        //Damage(damage);
    }

    private void Revive()
    {
        _alive = true;
        healthBase.ResetLife();
        Respawn();
        animator.SetTrigger("Revive");
        deathImage.SetActive(false);
        Invoke(nameof(TurnOnColliders), .1f);
    }

    private void TurnOnColliders()
    {
        colliders.ForEach(i => i.enabled = true);
    }

    private void OnKill(HealthBase h)
    {
        if (_alive)
        {
            _alive = false;
            animator.SetTrigger("Death");
            colliders.ForEach(i => i.enabled = false);
            deathImage.SetActive(true);
            Invoke(nameof(Revive), 3f);
        }
    }
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        else
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

            var inputAxisVertical = Input.GetAxis("Vertical");
            var speedVector = transform.forward * inputAxisVertical * speed;
            var isWalking = inputAxisVertical != 0;

            if (characterController.isGrounded)
            {
                if (_jumping)
                {
                    _jumping = false;
                    animator.SetTrigger("Land");
                }

                _vsSpeed = 0;
                if (Input.GetKeyDown(jumpKeyCode))
                {
                    _vsSpeed = jumpSpeed;

                    if (!_jumping)
                    {
                        _jumping = true;
                        animator.SetTrigger("Jump");
                    }
                }
            }

            _vsSpeed -= gravity * Time.deltaTime;
            speedVector.y = _vsSpeed;

            if (isWalking)
            {
                if (Input.GetKey(runKeyCode))
                {
                    speedVector *= speedRun;
                    animator.speed = speedRun;

                }
                else
                {
                    animator.speed = 1f;
                }
            }

            characterController.Move(speedVector * Time.deltaTime);

            animator.SetBool("isRunning", inputAxisVertical != 0);

        }
        /*Mesmo que o acima, mas melhor
        if (inputAxisVertical != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        */
    }

    public void TogglePause()
    {
        _isPaused = !_isPaused;
        uiMenu.SetActive(_isPaused);

        if (_isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f; 
        }
    }

    public void Respawn()
    {
        if(CheckpointManager.Instance.HasCheckpoint())
        {
            transform.position = CheckpointManager.Instance.GetPositionRespawn();
        }
    }

    public void ChangeSpeed(float speed, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(speed, duration));
    }

    public void ChangeSpeed(float speedBonus)
    {
        _isSpeed = true;
        speed = speedBonus;
    }

    IEnumerator ChangeSpeedCoroutine(float localSpeed, float duration)
    {
        var defaultSpeed = speed;
        speed = localSpeed;
        yield return new WaitForSeconds(duration);
        speed = defaultSpeed;
    }

    public void ChangeTexture(ClothSetup setup, float duration)
    {
        StartCoroutine(ChangeTextureCoroutine(setup, duration));
    }

    IEnumerator ChangeTextureCoroutine(ClothSetup setupCloth, float duration)
    {
        _clothChanger.ChangeTexture(setupCloth);
        yield return new WaitForSeconds(duration);
        _clothChanger.ResetTexture();
    }

    //Mudando apenas a roupa, sem tempo.
    public void ChangeTexture(ClothSetup setup, bool isLoaded)
    {
        if (_isSpeed && !isLoaded)
        {
            speed = _defaultSpeed;
            _isSpeed = false;
        }
        else if(healthBase.GetStatusDamageMultiplier() && !isLoaded)
        {
            healthBase.ResetDamageMultiplier();
        }
        _clothChanger.ChangeTexture(setup);
    }

    public void GetOnLoad()
    {
        ChangeTexture(SaveManager.Instance.Setup.clothSetup, true);
        Debug.Log(SaveManager.Instance.Setup.clothSetup);
        healthBase.ChangeStartLife(SaveManager.Instance.Setup.health);
        CheckpointManager.Instance.LoadCheckpoint(SaveManager.Instance.Setup.checkPoint);
    }
}
