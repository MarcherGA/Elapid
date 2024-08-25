using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CollisionDirections), typeof(Damageable))]
public class Skeleton : Enemy
{
    
    public enum WalkableDirection
    {
        Right,
        Left
    }

    public WalkableDirection WalkDirection
    {
        get => _walkDirection;
        set
        {
            if (_walkDirection != value)
            {
                transform.localScale *= _facingDirectionScaler;

                _walkDirectionVector = value == WalkableDirection.Right ? Vector2.right : Vector2.left;

                _walkDirection = value;
            }
        }
    }



    [SerializeField] private float _maxSpeed = 3f;
    [SerializeField] private float _walkAcceleration = 30f;
    [SerializeField] private float _walkStopRate = 0.05f;
    [SerializeField] private DetectionZone _cliffDetectionZone;

    private static Vector2 _facingDirectionScaler = new Vector2(-1,1);
    private Vector2 _walkDirectionVector = Vector2.right;
    CollisionDirections _collisionDirections;
    WalkableDirection _walkDirection;

    private new void Awake()
    {
        base.Awake();
        _collisionDirections = GetComponent<CollisionDirections>();
    }

    void FixedUpdate()
    {
        if (_collisionDirections.IsOnWall && _collisionDirections.IsGrounded)
        {
            FlipDirection();
        }

        if(!_damageable.LockVelocity)
        {
            UpdateVelocity();
        }
    }

    private void UpdateVelocity()
    {
        if (CanMove && _collisionDirections.IsGrounded)
        {
            _rigidBody.velocity = new Vector2(
                Mathf.Clamp(_rigidBody.velocity.x + (_walkAcceleration * _walkDirectionVector.x * Time.fixedDeltaTime), -_maxSpeed, _maxSpeed)
                , _rigidBody.velocity.y);
        }
        else
        {
            _rigidBody.velocity = new Vector2(Mathf.Lerp(_rigidBody.velocity.x, 0f, _walkStopRate), _rigidBody.velocity.y);
        }
    }

    private void FlipDirection()
    {
        WalkDirection = WalkDirection == WalkableDirection.Right ? WalkableDirection.Left : WalkableDirection.Right;
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        _rigidBody.velocity = new Vector2(knockback.x, _rigidBody.velocity.y + knockback.y);
    }

    public void OnCliffDetected()
    {
        if(_collisionDirections.IsGrounded)
        {
            FlipDirection();
        }
    }
}
