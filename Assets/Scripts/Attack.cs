using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Attack : MonoBehaviour
{
    [SerializeField] private int _attackDamage = 10;
    [SerializeField] private Vector2 _knockback = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Damageable damageable = other.GetComponent<Damageable>();

        if(damageable != null)
        {
            _knockback.x = transform.parent.localScale.x > 0 ?  MathF.Abs(_knockback.x) : MathF.Abs(_knockback.x) * -1;
            if(damageable.TakeDamage(_attackDamage, _knockback))
            {
                Debug.Log(other.name + " took " + _attackDamage + " damage");
            }
        }
    }
}
