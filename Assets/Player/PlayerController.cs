using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    enum STATE
    {
        IDLE,
        JUMP
    }

    [Header("Input related")]
    [SerializeField] bool _inputMovementRelatedToCamera;
    [SerializeField]
    [Range(0f, 0.3f)] float _smoothRotationValue;
    [Header("Movement related")]
    [SerializeField] float _jumpImpulse;
    [SerializeField] LayerMask _floorLayerMask;
    [SerializeField][Range(0f, 0.1f)] float _floorRaycastLength;

    private Vector3 _movement_input;
    private float _playerRotationcurrentVelocity;
    private Rigidbody _rb;
    private MoveComponent _moveComponent;
    private Camera _mainCamera;
    private CapsuleCollider _col;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _mainCamera = Camera.main;
        _moveComponent = GetComponent<MoveComponent>();
        _col = GetComponent<CapsuleCollider>();
    }

    private void FixedUpdate()
    {
        rotatePlayer();
        maxSpeedRelatedToInput();
    }

    private void rotatePlayer()
    {
        if(_movement_input == Vector3.zero)
        {
            return;
        }
        float currentRotation = transform.eulerAngles.y;
        float targetRotation = Mathf.Atan2(_movement_input.x, _movement_input.z) * Mathf.Rad2Deg;
        if (_inputMovementRelatedToCamera)
        {
            targetRotation += _mainCamera.transform.eulerAngles.y;
        }
        float updatedRotation = Mathf.SmoothDampAngle(currentRotation, targetRotation, ref _playerRotationcurrentVelocity, _smoothRotationValue);
        _rb.MoveRotation(Quaternion.Euler(0, updatedRotation, 0));
    }

    private void maxSpeedRelatedToInput()
    {
        _moveComponent.InputFactor = _movement_input.magnitude;
    }

    private void OnMove(InputValue inputValue)
    {
        Vector2 input_vector = inputValue.Get<Vector2>();
        Vector3 vector3 = new Vector3(input_vector.x, 0, input_vector.y);
        _movement_input = Vector3.ClampMagnitude(vector3, 1);
    }

    private void OnJump()
    {
        if (!isOnFloor())
        {
            return;
        }
        _rb.AddForce(transform.up * _jumpImpulse);
    }

    private bool isOnFloor()
    {
        float altura = _col.height * transform.lossyScale.y;
        return Physics.Raycast(transform.position, -transform.up, altura / 2 + _floorRaycastLength, _floorLayerMask);
    }
}