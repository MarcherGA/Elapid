using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDirections : MonoBehaviour
{
    public bool IsGrounded { get
        {
            return _isGrounded;
        } private set
        {
            _isGrounded = value;
            _animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }
    public bool IsOnWall { get
        {
            return _isOnWall;
        } private set
        {
            _isOnWall = value;
            _animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }
    public bool IsOnCeiling { get
        {
            return _isOnCeiling;
        } private set
        {
            _isOnCeiling = value;
            _animator.SetBool(AnimationStrings.isOnCeiling, value);
        }
    }

    [SerializeField] bool _isGrounded;
    [SerializeField] bool _isOnWall;

    [SerializeField] bool _isOnCeiling;
    [SerializeField] float _groundDistance = 0.05f;
    [SerializeField] float _wallDistance = 0.2f;
    [SerializeField] float _ceilingDistance = 0.05f;



    CapsuleCollider2D _collider;
    Animator _animator;

    RaycastHit2D[] _groundHits = new RaycastHit2D[5];
    RaycastHit2D[] _wallHits = new RaycastHit2D[5];

    RaycastHit2D[] _ceilingHits = new RaycastHit2D[5];

    ContactFilter2D _castFilter;
    private Vector2 wallCheckDirection => transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    void Awake()
    {
        _collider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        IsGrounded = _collider.Cast(Vector2.down, _castFilter, _groundHits, _groundDistance) > 0;
        IsOnWall = _collider.Cast(wallCheckDirection, _castFilter, _wallHits, _wallDistance) > 0;
        IsOnCeiling = _collider.Cast(Vector2.up, _castFilter, _ceilingHits, _ceilingDistance) > 0;
    }
}
