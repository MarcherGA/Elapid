
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int _damage = 10;
    [SerializeField] Vector2 _moveSpeed = new Vector2(3f, 0);
    [SerializeField] Vector2 _knockback = Vector2.zero;

    private Rigidbody2D _rigidBody;
    
    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _rigidBody.velocity = new Vector2(_moveSpeed.x * transform.localScale.x, _moveSpeed.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {        
        if(other.gameObject.GetComponent<Damageable>() != null)
        {
            _knockback.x = transform.localScale.x > 0 ?  Mathf.Abs(_knockback.x) : Mathf.Abs(_knockback.x) * -1;

            if(other.gameObject.GetComponent<Damageable>().TakeDamage(_damage, _knockback))
            {
                Debug.Log(other.name + " took " + _damage + " damage");
            }
        }
        
        Destroy(this.gameObject);

    }
}
