﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Creatures.Weapons
{
    public class BaseProjectile : MonoBehaviour
    {
        [SerializeField] protected float _speed;
        [SerializeField] private bool _inverX;

        protected Rigidbody2D Rigidbody;
        protected int Direction;

       protected virtual void Start()
        {
            var mod = _inverX ? -1 : 1;
            Direction = mod * transform.lossyScale.x > 0 ? 1 : -1;
            Rigidbody = GetComponent<Rigidbody2D>();
             var force = new Vector2(Direction * _speed, 0); 
             Rigidbody.AddForce(force, ForceMode2D.Impulse);
        }


    }
}