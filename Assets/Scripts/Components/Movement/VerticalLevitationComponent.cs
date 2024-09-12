using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Components.Movement
{

    public class VerticalLevitationComponent : MonoBehaviour
    {
        [SerializeField] private float _frequency = 1f;
        [SerializeField] private float _amplitude = 1f;

        private float _originalY;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _originalY = _rigidbody.position.y;
        }


        private void Update()
        {
            var pos = _rigidbody.position;
            pos.y = _originalY + Mathf.Sin(Time.time * _frequency) * _amplitude;
            _rigidbody.MovePosition(pos);
        }

    }
}
