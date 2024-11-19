using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _turnSpeed;

    private Vector3 _relative;
    private Vector3 _direction;
    private Matrix4x4 _matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

    private void FixedUpdate()
    {
        InputGet();
        Look();

        if (_direction != Vector3.zero)
        {
            Move();
        }
    }

    private Vector3 ToIso(Vector3 input) => _matrix.MultiplyPoint3x4(input);

    private void InputGet()
    {
        _direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    private void Move()
    {
        _rigidbody.MovePosition(transform.position + (transform.forward * _direction.magnitude) * _moveSpeed * Time.deltaTime);
    }

    private void Look()
    {
        if (_direction != Vector3.zero)
        {
            _relative = (transform.position + ToIso(_direction)) - transform.position;

            var rotation = Quaternion.LookRotation(_relative, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _turnSpeed * Time.deltaTime);
        }
    }
}
