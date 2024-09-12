﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrew.Utils;
using UnityEngine.Events;
using static PixelCrew.Components.EnterCollisionComponent;

namespace PixelCrew.Components
{
    public class EnterTrigger : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private LayerMask _layer = ~0;
        [SerializeField] private EnterEvents _action;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.IsInLayer(_layer)) return;
            if (!string.IsNullOrEmpty(_tag) && !other.gameObject.CompareTag(_tag)) return;
            _action?.Invoke(other.gameObject);

        }

    }
}