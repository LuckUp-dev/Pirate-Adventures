using PixelCrew.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrew.Utils;
using UnityEditor;
using UnityEditor.Animations;
using PixelCrew.Model;

namespace PixelCrew.Creatures
{

    public class Hero : Creature
    {
        [SerializeField] private CheckCircleOverlap _interactionCheck;
        [SerializeField] private LayerCheck _wallCheck;

        [SerializeField] private float _slamDownVelocity;
        [SerializeField] private float _interactionRadius;


        [SerializeField] private Cooldown _throwCooldown;
        [SerializeField] public AnimatorController _armed;
        [SerializeField] private AnimatorController _disarmed;

        [Space] [Header("Particles")]
        [SerializeField] private ParticleSystem _hitParticles;
        [SerializeField] private SpawnComponent _swordAtackParticles;

        private static readonly int ThrowKey = Animator.StringToHash("throw");
        private static readonly int IsOnWall = Animator.StringToHash("is-on-wall");


        private bool _allowDoubleJump;
        private bool _isOnWall;

        public GameSession _session;
        private float _defaultGravityScale;

        private int CoinsCount => _session.Data.Inventory.Count("Coin");
        private int SwordCount => _session.Data.Inventory.Count("Sword");


        AmountOfSword amount = new AmountOfSword(); //


        protected override void Awake()
        {
            base.Awake();
            _defaultGravityScale = Rigidbody.gravityScale;
        }

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            var health = GetComponent<HealthComponent>();
            _session.Data.Inventory.OnChanged += OnInventoryChanged;

            health.SetHealth(_session.Data.Hp);
            UpdateHeroWeapon();
        }

        private void OnDestroy()
        {
            _session.Data.Inventory.OnChanged -= OnInventoryChanged;

        }

        private void OnInventoryChanged(string id, int value)
        {
            if (id == "Sword")
                UpdateHeroWeapon();
        }

        public void OnHealthChanged(int currentHealth)
        {
            _session.Data.Hp = currentHealth;
        }


        protected override void Update()
        {
            base.Update();
            var moveToSameDirection = Direction.x * transform.lossyScale.x > 0;
            if (_wallCheck.IsTouchingLayer && moveToSameDirection)
            {
                _isOnWall = true;
                Rigidbody.gravityScale = 0;
            }
            else
            {
                _isOnWall = false;
                Rigidbody.gravityScale = _defaultGravityScale;
            }

            Animator.SetBool(IsOnWall, _isOnWall);
        }


        protected override float CalculateYVelocity()
        {
            var isJumpPressing = Direction.y > 0;

            if (IsGrounded || _isOnWall)
            {
                _allowDoubleJump = true;
            }

            if (!isJumpPressing && _isOnWall)
            {
                return 0f;
            }

            return base.CalculateYVelocity();
        }

        protected override float CalculateJumpVelocity(float yVelocity)
        {
            if (!IsGrounded && _allowDoubleJump && !_isOnWall)
            {
                _particles.Spawn("Jump");
                _allowDoubleJump = false;
                return _jumpSpeed;
            }

            return base.CalculateJumpVelocity(yVelocity);
        }

        public override void TakeDamage()
        {
            base.TakeDamage();
            if (CoinsCount > 0)
            {
                SpawnCoins();
            }
        }


        private void SpawnCoins()
        {

            if (ScoreCounter._score > 0)
            {
                var numCoinsToDispose = Mathf.Min(ScoreCounter._score, 5);
                ScoreCounter._score -= numCoinsToDispose;

                var burst = _hitParticles.emission.GetBurst(0);
                burst.count = numCoinsToDispose;
                _hitParticles.emission.SetBurst(0, burst);

                _hitParticles.gameObject.SetActive(true);
                _hitParticles.Play();
                print(ScoreCounter._score);
            }
        }

        public void Interact()
        {
            _interactionCheck.Check();
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.IsInLayer(_groundLayer))
            {
                var contact = other.contacts[0];
                if (contact.relativeVelocity.y >= _slamDownVelocity)
                {
                    _particles.Spawn("SlamDown");
                }
            }
        }


        public override void Attack()
        {
            if (SwordCount <= 0) return;

            base.Attack();

        }


        

        private void UpdateHeroWeapon()
        {
            Animator.runtimeAnimatorController = SwordCount > 0 ? _armed: _disarmed;
        }

        public void OnDoThrow()
        {
            _particles.Spawn("Throw");
        }

        public IEnumerator WaitSomeTime()
        {
            yield return new WaitForSeconds(1f);
        }

        internal void Throw(float number)
        {
            if (number == 1)
            {
                if (_throwCooldown.IsReady && (amount._numberOfSwords > 1)) // && (amount._numberOfSwords > 1)
                {
                    amount._numberOfSwords -= 1;    //
                    print($"У вас {amount._numberOfSwords} зарядов меча");  //
                    Animator.SetTrigger(ThrowKey);
                    _throwCooldown.Reset();
                }
            }
           // else if (amount._numberOfSwords > 1)        // При нажатии на ctrl должен кидать несколько мечей друг за другом
          //  {
           //     amount._numberOfSwords -= 1;    //
           //     print($"У вас {amount._numberOfSwords} зарядов меча");
          //      Animator.SetTrigger(ThrowKey);          // Разобраться как работает этот бросок
           //     WaitSomeTime();
            //    Animator.SetTrigger(ThrowKey);


            //}
        }

        public void AddInInventory(string id, int value)
        {
            _session.Data.Inventory.Add(id, value);
        }
    }
}