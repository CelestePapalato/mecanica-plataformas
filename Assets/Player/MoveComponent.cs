using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveComponent : MonoBehaviour
{
    [SerializeField] float _maxSpeed;
    [SerializeField] float _acceleration;
    [SerializeField] float _deceleration;

    private Rigidbody _rigidbody;
    private Vector3 _desiredVelocity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _move();
    }

    private void _move()
    {
        Vector3 currentVelocity = _rigidbody.velocity;
        currentVelocity.y = 0;
        Vector3 movement = Vector3.zero;
        Vector3 targetSpeed = _desiredVelocity * _maxSpeed;
        float difference;
        difference = targetSpeed.magnitude - currentVelocity.magnitude;
        if(!_mathApproximately(difference, 0, 0.01f))
        {
            float currentAcceleration;
            if (difference > 0)
            {
                currentAcceleration = Mathf.Min(_acceleration * Time.fixedDeltaTime, difference);
            }
            else
            {
                currentAcceleration = Mathf.Max(_deceleration * Time.fixedDeltaTime * -1, difference);
            }
            difference = 1f / difference;
            movement = targetSpeed - currentVelocity;
            movement *= difference * currentAcceleration;
        }
        _rigidbody.velocity += movement;
    }

    private bool _mathApproximately(float n1, float n2, float tolerance)
    {
        return (Mathf.Abs(n2 - n1) < tolerance);
    }
    
    public void OnMove(InputValue inputValue)
    {
        Vector2 input_vector = inputValue.Get<Vector2>();
        Vector3 vector3 = new Vector3(input_vector.x, 0, input_vector.y);
        _desiredVelocity = Vector3.ClampMagnitude(vector3, 1);
    }
}
