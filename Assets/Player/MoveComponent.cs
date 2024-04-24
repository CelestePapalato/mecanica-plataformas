using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveComponent : MonoBehaviour
{
    [Header("Speed related")]
    [SerializeField] float _maxSpeed;
    private float _currentMaxSpeed;
    public float InputFactor
    {
        get { return _currentMaxSpeed; }
        set { _currentMaxSpeed = _maxSpeed * value; }
    }
    [SerializeField] float _acceleration;
    [SerializeField] float _deceleration;

    private Rigidbody _rigidbody;
    private float _inputMagnitude = 0f;

    private void Awake()
    {
        _currentMaxSpeed = _maxSpeed;
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
        Vector3 _desiredVelocity = transform.forward;
        Vector3 targetSpeed = _desiredVelocity * _currentMaxSpeed;
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

    private void OnMove(InputValue inputValue)
    {
        Vector2 input_vector = inputValue.Get<Vector2>();
        input_vector = Vector2.ClampMagnitude(input_vector, 1);
        _inputMagnitude = input_vector.magnitude;
    }
}
