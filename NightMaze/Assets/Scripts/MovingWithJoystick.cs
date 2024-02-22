using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class MovingWithJoystick : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Animator _animator;


    [SerializeField] private float _moveSpeed;

    private void Update()
    {
        _animator.SetFloat("Speed", Mathf.Abs(_joystick.Horizontal) + Mathf.Abs(_joystick.Vertical));


        if (_joystick.Horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            //transform.Rotate(0, 0, 0);
        }
        if (_joystick.Horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            //transform.Rotate(0, 180, 0);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_joystick.Horizontal * _moveSpeed, _joystick.Vertical * _moveSpeed);
    }
}
