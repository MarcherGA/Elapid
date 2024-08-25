using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRemoveBehaviour : StateMachineBehaviour
{

    [SerializeField] private float _fadeTime = 0.5f;
    [SerializeField] private float _fadeDelay = 0.5f;

    SpriteRenderer _spriteRenderer;
    GameObject _objToRemove;
    Color _startColor;
    float _timeElapsed = 0;
    float _timeElapsedDelay = 0;
    float newAlpha;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       _spriteRenderer = animator.GetComponent<SpriteRenderer>();
       _objToRemove = animator.gameObject;
       _startColor = _spriteRenderer.color;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(_timeElapsedDelay < _fadeDelay)
        {
            _timeElapsedDelay += Time.deltaTime;
            return;
        }
        
       _timeElapsed += Time.deltaTime;

       newAlpha = _startColor.a * (1 - (_timeElapsed / _fadeTime));
       _spriteRenderer.color = new Color(_startColor.r, _startColor.g, _startColor.b, newAlpha);


       if (_timeElapsed >= _fadeTime)
       {
           Destroy(_objToRemove);
       }
    }
}
