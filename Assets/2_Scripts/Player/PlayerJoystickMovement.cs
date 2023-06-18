using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoystickMovement : MonoBehaviour
{
    [SerializeField] private FloatingJoystick floatingJoystick;
    [SerializeField] private float moveSpeed;

    private bool _canMove;
    private float _heading;
    
    private Animator _animator;
    private Rigidbody _rigidbody;

    private static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");
    private void Start()
    {
        _animator = Player.Instance.Animator;
        _rigidbody = Player.Instance.Rigidbody;
        _canMove = true;
        _animator.SetFloat(MoveSpeed, 0);
    }

    private void FixedUpdate()
    {
        if (!_canMove) return;

        var direction = Vector3.right * floatingJoystick.Horizontal +
                        Vector3.forward * floatingJoystick.Vertical;
        
        if (!Input.GetMouseButton(0))
            direction = direction.normalized / 1000;
        
        _animator.SetFloat(MoveSpeed, direction.magnitude);
        
        if (Mathf.Abs(floatingJoystick.Vertical) > 0.001f ||
            Mathf.Abs(floatingJoystick.Horizontal) > 0.001f)
        {
            _heading = Mathf.Atan2(direction.x, direction.z);
            transform.rotation = Quaternion.Euler(0f, _heading * Mathf.Rad2Deg, 0);
            _rigidbody.velocity = direction * moveSpeed;
        }
        else
        {
            //transform.rotation = Quaternion.Euler(0f, _heading * Mathf.Rad2Deg, 0);
            _rigidbody.velocity = Vector3.down;
        }
    }
}