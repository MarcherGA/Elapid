using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> onTakeDamage;
    public int MaxHealth
    {
        get => _maxHealth;
        set
        {
            if(_maxHealth != value)
            {
                _maxHealth = value;
            }
        }
    }

    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            if(_currentHealth != value)
            {
                _currentHealth = value;
            }

            if(_currentHealth <= 0)
            {
                IsAlive = false;
            }
        }
    }

    public bool IsAlive
    {
        get => _isAlive;
        set
        {
            if(_isAlive != value)
            {
                _isAlive = value;
                _animator.SetBool(AnimationStrings.isAlive, value);
                Debug.Log("Is Alive: " + value);
            }
        }
    }

    public bool LockVelocity
    { 
        get => _animator.GetBool(AnimationStrings.lockVelocity);
        private set
        {
            _animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }


    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _currentHealth = 100;
    [SerializeField] private bool _isAlive = true;
    [SerializeField] private bool _isInvincible = false;

    [SerializeField] private float _invincibilityTimer = 0.5f;

    private Animator _animator;
    

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public bool TakeDamage(int damage, Vector2 knockback)
    {
        if(IsAlive && !_isInvincible)
        {
            CurrentHealth -= damage;
            _isInvincible = true;
            LockVelocity = true;

            _animator.SetTrigger(AnimationStrings.hitTrigger);

            StartCoroutine(SetInvincibleTimer());

            onTakeDamage?.Invoke(damage, knockback);

            CharacterEvents.characterTookDamage?.Invoke(gameObject, damage);

            return true;
        }

        return false;
    }

    public bool Heal(int healthRestored)
    {
        if(IsAlive && CurrentHealth < MaxHealth)
        {
            int actualHealthRestored = CurrentHealth + healthRestored > MaxHealth ? MaxHealth - CurrentHealth : healthRestored;

            CurrentHealth += actualHealthRestored;

            CharacterEvents.characterHealed?.Invoke(gameObject, actualHealthRestored);

            return true;
        }

        return false;
    }


    IEnumerator SetInvincibleTimer()
    {
        yield return new WaitForSeconds(_invincibilityTimer);
        _isInvincible = false;
    }


}
