using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Animation;

namespace Enemies
{
    public class EnemieBase : MonoBehaviour, IDamageable
    {
        public Collider enemyCollider;
        public FlashColor flashColor;
        public ParticleSystem enemyParticleSystem;
        public float startLife = 10f;
        public bool lookAtPlayer = false;

        [SerializeField]
        protected float _currentLife;

        [Header("Animation")]
        [SerializeField]
        private AnimationBase _animationBase;

        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        [Header("Events")]
        public UnityEvent OnKillEvent;

        private Player _player;

        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            _player = GameObject.FindObjectOfType<Player>();
        }

        protected void ResetLife()
        {
            _currentLife = startLife;
        }

        protected virtual void Init()
        {
            ResetLife();
            if(startWithBornAnimation)BornAnimation();
        }

        protected virtual void Kill()
        {
            OnKill();
        }

        protected virtual void OnKill()
        {
            if (enemyCollider != null) enemyCollider.enabled = false;
            Destroy(gameObject, 3f);
            PlayAnimationByTrigger(AnimationType.DEATH);
            OnKillEvent?.Invoke();
        }

        public virtual void OnDamage(float f)
        {
            if(flashColor != null) flashColor.Flash();
            if (enemyParticleSystem != null) enemyParticleSystem.Emit(15);
            _currentLife -= f;

            if (_currentLife <= 0)
            {
                Kill();
            }
        }

        #region ANIMATION
        private void BornAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }

        #endregion

        public void Damage(float damage)
        {
            Debug.Log("Damage");
            OnDamage(damage);
        }

        public void Damage(float damage, Vector3 dir)
        { 
            OnDamage(damage);
            transform.DOMove(transform.position - dir, .1f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Player p = collision.transform.GetComponent<Player>();

            if(p != null)
            {
                p.healthBase.Damage();
            }
        }

        public virtual void Update()
        {
            if (lookAtPlayer)
            {
                transform.LookAt(_player.transform.position);
            }
        }
    }
}
