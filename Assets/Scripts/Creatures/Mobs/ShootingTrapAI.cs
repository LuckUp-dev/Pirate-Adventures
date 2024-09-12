using System.Collections;
using System.Collections.Generic;
using PixelCrew.Components;
using PixelCrew.Utils;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs
{
    public class ShootingTrapAI : MonoBehaviour
    {
           
        [SerializeField] private LayerCheck _vision;

        [Header("Melee")]
        [SerializeField] private Cooldown _meleeColdown;
        [SerializeField] private CheckCircleOverlap _meleeAttack;
        [SerializeField] private LayerCheck _meleeCanAttack;

        [Header("Range")]
        [SerializeField] private SpawnComponent _rangeAttack;
        [SerializeField] private Cooldown _rangeColdown;
        [SerializeField] private bool _canOnlyShoot;

        private static readonly int Melee = Animator.StringToHash("melee");
        private static readonly int Range = Animator.StringToHash("range");


        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_canOnlyShoot)
            {
                if (_rangeColdown.IsReady)
                {
                    RangeAttack();
                }
            }

               else if (_vision.IsTouchingLayer)
                {
                    if (_meleeCanAttack.IsTouchingLayer)
                    {
                        if (_meleeColdown.IsReady)
                            MeleeAttack();
                        return;
                    }
                    if (_rangeColdown.IsReady)
                    {
                        RangeAttack();
                    }
                }
            
        }

        private void RangeAttack()
        {
            _rangeColdown.Reset();
            _animator.SetTrigger(Range);

        }

        private void MeleeAttack()
        {
            _meleeColdown.Reset();
            _animator.SetTrigger(Melee);
        }

        public void OnMeleeAttack()
        {
            _meleeAttack.Check();
        }

        public void OnRangeAttack()
        {
            _rangeAttack.Spawn();
        }
    }
}