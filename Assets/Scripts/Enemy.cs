using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool HasTarget
    {
        get => _hasTarget;
        protected set 
        {
            _hasTarget = value; 
            _animator.SetBool(AnimationStrings.hasTarget, value);
        } 
    }

    public bool CanMove
    { 
        get
        {
            return _animator.GetBool(AnimationStrings.canMove);
        }
    }

    public float AttackCooldown 
    { 
        get => _animator.GetFloat(AnimationStrings.attackCooldown); 
        protected set
        {
            _animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
        } 
    }

    [SerializeField] protected DetectionZone _attackZone;

    protected Rigidbody2D _rigidBody;
    protected Animator _animator;
    protected Damageable _damageable;

    private bool _hasTarget = false;

    protected void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _damageable = GetComponent<Damageable>();

    }

    protected void OnEnable()
    {
        _attackZone.onDetected.AddListener ((target) => HasTarget = true);
        _attackZone.onLost.AddListener((target) => HasTarget = false);
    }

    protected void OnDisable()
    {
        _attackZone.onDetected.RemoveListener((target) => HasTarget = true);
        _attackZone.onLost.RemoveListener((target) => HasTarget = false);
    }

    protected void Update()
    {
        if(AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
    }
}
