using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int _healthRestored = 20;
    [SerializeField] private Vector3 _spinRotationSpeed = new Vector3(0, 180, 0);
    [SerializeField] private AudioClip _pickupSound;
    [SerializeField] private float _volume = 0.5f;
    


    private void OnTriggerEnter2D(Collider2D other)
    {
        Damageable damageable = other.GetComponent<Damageable>();
        if(damageable != null)
        {
            if(damageable.Heal(_healthRestored))
            {
                AudioSource.PlayClipAtPoint(_pickupSound, transform.position, _volume);
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        transform.Rotate(_spinRotationSpeed * Time.deltaTime);
    }
}
