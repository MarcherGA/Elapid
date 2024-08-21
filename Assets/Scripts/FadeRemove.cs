using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeRemoveText : MonoBehaviour
{
    [SerializeField] private float _fadeTime = 0.5f;

    TextMeshProUGUI _textMeshPro;
    Color _newColor;
    float _startColorAlpha;
    float _timeElapsed = 0;
    float newAlpha;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _startColorAlpha = _textMeshPro.color.a;
        _newColor = _textMeshPro.color;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    private void Update()
    {
       _timeElapsed += Time.deltaTime;


       if (_timeElapsed < _fadeTime)
       {
            newAlpha = _startColorAlpha * (1 - (_timeElapsed / _fadeTime));
            _newColor.a = newAlpha;
            _textMeshPro.color = _newColor;
       }
       else
       {
            Destroy(gameObject);
       }
    }
}
