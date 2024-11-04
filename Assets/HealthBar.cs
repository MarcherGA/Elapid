using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private TMP_Text _healthBarText;
    Damageable _playerDamageable;

    void Awake()
    {
        _playerDamageable = GameObject.FindWithTag("Player").GetComponent<Damageable>();
    }

    void Start()
    {
        UpdateHealthBar(_playerDamageable.CurrentHealth, _playerDamageable.MaxHealth);
    }

    void OnEnable()
    {
        _playerDamageable.onHealthChanged.AddListener(UpdateHealthBar);
    }


    void OnDisable()
    {
        _playerDamageable.onHealthChanged.RemoveListener(UpdateHealthBar);
    }

    private float CalculateSliderPercentage(int currentHealth, int maxHealth)
    {
        return (float) currentHealth / maxHealth;
    }

    private void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        _healthSlider.value = CalculateSliderPercentage(currentHealth, maxHealth);
        _healthBarText.text = "HP: " + currentHealth.ToString() + "/" + maxHealth.ToString();
    }
}
