using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlyingEye : Enemy
{   
    [SerializeField] float _flightSpeed = 2f;
    [SerializeField] List<Transform> _waypoints;
    [SerializeField] Collider2D _deathCollider;

    Transform _currentWaypoint;
    int _currentWaypointIndex = 0;

    private new void OnEnable()
    {
        base.OnEnable();
        _damageable.onDeath.AddListener(OnDeath);
    }

    private new void OnDisable()
    {
        base.OnDisable();
        _damageable.onDeath.RemoveListener(OnDeath);
    }

    private void Start()
    {
        _currentWaypoint = _waypoints[_currentWaypointIndex];
    }

    private void FixedUpdate()
    {
        if(_damageable.IsAlive)
        {   
            if(CanMove)
            {
                Flight();
            }
            else if(_rigidBody.velocity != Vector2.zero)
            {
                _rigidBody.velocity = Vector2.zero;
            }
        }
    }

    private void Flight()
    {

        if(Vector2.Distance(transform.position, _currentWaypoint.position) <= 0.1f)
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Count;
            _currentWaypoint = _waypoints[_currentWaypointIndex];
            
        }

        Vector2 directionToWaypoint = (_currentWaypoint.position - transform.position).normalized;

        _rigidBody.velocity = directionToWaypoint * _flightSpeed;

        UpdateDirection();

        
    }

    private void UpdateDirection()
    {
        if(transform.localScale.x > 0 && _rigidBody.velocity.x < 0 || transform.localScale.x < 0 && _rigidBody.velocity.x > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }

    void OnDeath()
    {
        _rigidBody.gravityScale = 1f;
        _rigidBody.velocity = new Vector2(0, _rigidBody.velocity.y);
        _deathCollider.enabled = true;
    }
}
