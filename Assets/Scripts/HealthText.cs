using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    [SerializeField] private Vector3 _moveSpeed = new Vector3(0, 125, 0);

    RectTransform _textTransform;

    private void Awake()
    {
        _textTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        _textTransform.position += _moveSpeed * Time.deltaTime;
    }
}
