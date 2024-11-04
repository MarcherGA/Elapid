using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlaySoundBehaviour : StateMachineBehaviour
{
    [SerializeField] private AudioClip _soundToPlay;
    [SerializeField] private float _volume = 1f;
    [SerializeField] private bool _playOnEnter = true, _playOnExit = false, _playAfterDelay = false;
    [SerializeField] private float _delay = 0.25f;

    private float _timeElapsed = 0f;
    private bool _hadDelayedSoundPlayed = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(_playOnEnter)
       {
            PlaySound(animator);
       }

       if(_playAfterDelay)
       {
            Task.Delay((int)(_delay * 1000)).ContinueWith(
                t => PlaySound(animator));
       }

       _timeElapsed = 0f;
       _hadDelayedSoundPlayed = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(_playAfterDelay && !_hadDelayedSoundPlayed)
       {
            if(_timeElapsed < _delay)
            {
                _timeElapsed += Time.deltaTime;

            }
            else
            {
                PlaySound(animator);
                _hadDelayedSoundPlayed = true;
            }
       }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(_playOnExit)
       {
            PlaySound(animator);
       }
    }

    private void PlaySound(Animator animator)
    {
        AudioSource.PlayClipAtPoint(_soundToPlay, animator.transform.position, _volume);
    }
}
