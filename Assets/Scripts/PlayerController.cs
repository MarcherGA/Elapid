using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
    
[RequireComponent(typeof(Rigidbody2D), typeof(CollisionDirections))]
public class PlayerController : MonoBehaviour
{
    public bool IsMoving { get 
        { 
            return _isMoving;
        } 
        private set
        {
            _isMoving = value;
            _animator?.SetBool(AnimationStrings.isMoving, value);
        }
    }

    public bool IsRunning { get 
        { 
            return _isRunning;
        } 
        private set
        {
            _isRunning = value;
            _animator?.SetBool(AnimationStrings.isRunning, value);
        }
    }

    public float CurrentMoveSpeed{ get
        {

            if(IsMoving && !_collisionDirections.IsOnWall)
            {
                
                return _collisionDirections.IsGrounded ? (IsRunning ? _runSpeed : _walkSpeed) : _airWalkSpeed;
            }
            return 0;
        }
    }

    public bool IsFacingRight { get
        {
            return _isFacingRight;
        }
        private set
        {
            if(_isFacingRight != value)
            {
                _isFacingRight = value;
                transform.localScale *= _facingDirectionScaler;

            }
        }
    }

    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _runSpeed = 8f;
    [SerializeField] private float _airWalkSpeed = 3f;

    [SerializeField] private float _jumpImpulse = 10f;

    [SerializeField] private bool _isMoving = false;
    [SerializeField] private bool _isRunning = false;


    private CollisionDirections _collisionDirections;
    private bool _isFacingRight = true;
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private Vector2 _moveInput;
    private Vector2 _currentVelocity;
    private Vector2 _facingDirectionScaler = new Vector2(-1,1);

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collisionDirections = GetComponent<CollisionDirections>();
        _currentVelocity = new Vector2();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        SetVelocity(_moveInput.x * CurrentMoveSpeed, _rigidBody.velocity.y);
        _animator.SetFloat(AnimationStrings.yVelocity, _rigidBody.velocity.y);

    }

    private void SetVelocity(float xVelocity, float yVelocity)
    {
        _currentVelocity.Set(xVelocity, yVelocity);
        if (_rigidBody.velocity != _currentVelocity)
        {
            _rigidBody.velocity = _currentVelocity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();

        IsMoving = _moveInput != Vector2.zero;

        SetFacingDirection(_moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if(moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        //TODO check if alive
        if(context.started && _collisionDirections.IsGrounded)
        {
            _animator.SetTrigger(AnimationStrings.jump);
            SetVelocity(_rigidBody.velocity.x, _jumpImpulse);
        }
    }
}
