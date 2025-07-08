using System;

using UnityEngine;

using UnityEngine.Animations;

using ZL.Unity.SO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    public abstract class Enemy : SpawnedObject, IDamageable
    {
        [Line]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        [UsingCustomProperty]

        [SerializeField]

        private Collider mainCollider = null;

        public Collider MainCollider
        {
            get => mainCollider;
        }

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        #pragma warning disable CS0108

        protected Rigidbody rigidbody = null;

        #pragma warning restore CS0108

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        protected EnemyData enemyData = null;

        public EnemyData EnemyData
        {
            get => enemyData;
        }

        [SerializeField]

        private StringTable enemyNameTable = null;

        public StringTable EnemyNameTable
        {
            get => enemyNameTable;
        }

        private float currentHealth = 0f;

        public virtual float CurrentHealth
        {
            get => currentHealth;

            set
            {
                currentHealth = Math.Clamp(value, 0f, enemyData.MaxHealth);

                OnHealthChangedAction?.Invoke(currentHealth);

                if (currentHealth == 0f)
                {
                    OnKiiledAction?.Invoke();

                    Kill();
                }
            }
        }

        protected float rotationSpeed = 0f;

        protected float movementSpeed = 0f;

        protected bool isStoped = true;

        public event Action<float> OnHealthChangedAction = null;

        public event Action OnKiiledAction = null;

        public override float MovementSpeedMultiplier
        {
            set
            {
                movementSpeedMultiplier = value;

                if (gameObject.activeSelf == true)
                {
                    animatorGroup.SetFloat(nameof(movementSpeedMultiplier), value);
                }
            }
        }

        protected virtual void FixedUpdate()
        {
            if (isStoped == true)
            {
                return;
            }

            LookTowards();

            Movement();

            CheckDespawnCondition();
        }

        protected virtual void LookTowards()
        {
            if (finalDestination == null)
            {
                return;
            }

            float rotationSpeed = this.rotationSpeed * rotationSpeedMultiplier;

            if (rotationSpeed != 0f)
            {
                rigidbody.LookTowards(finalDestination.position, enemyData.RotationSpeed * Time.fixedDeltaTime, Axis.Y);
            }
        }

        protected virtual void Movement()
        {
            float movementSpeed = this.movementSpeed * movementSpeedMultiplier;

            if (movementSpeed != 0f)
            {
                rigidbody.MoveForward(movementSpeed * Time.fixedDeltaTime);

                animatorGroup.SetBool("isMoving", true);
            }

            else
            {
                animatorGroup.SetBool("isMoving", false);
            }
        }

        public override void Appear()
        {
            base.Appear();

            currentHealth = enemyData.MaxHealth;

            rotationSpeed = enemyData.RotationSpeed;

            movementSpeed = enemyData.MovementSpeed;
        }

        public override void OnAppeared()
        {
            base.OnAppeared();

            mainCollider.enabled = true;

            isStoped = false;

            MovementSpeedMultiplier = movementSpeedMultiplier;
        }

        public override void Disappear()
        {
            mainCollider.enabled = false;

            isStoped = true;

            base.Disappear();
        }

        public override void OnDisappeared()
        {
            finalDestination = null;

            rigidbody.velocity = Vector3.zero;

            OnHealthChangedAction = null;

            OnKiiledAction = null;

            base.OnDisappeared();
        }

        public virtual void TakeDamage(float damage, Vector3 contact)
        {
            CurrentHealth -= damage;
        }

        protected virtual void Kill()
        {
            Disappear();
        }
    }
}