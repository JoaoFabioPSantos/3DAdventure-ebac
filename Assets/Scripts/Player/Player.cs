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

    [Header("Movement")]
    public float speed = 1f;
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

    private void OnValidate()
    {
        if(healthBase== null)healthBase = GetComponent<HealthBase>();
    }

    protected override void Awake()
    {
        base.Awake();
        OnValidate();

        healthBase.OnDamage += Damage;
        healthBase.OnKill += OnKill;
    }

    #region LIFE
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

        if(isWalking)
        {
            if(Input.GetKey(runKeyCode))
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
}
