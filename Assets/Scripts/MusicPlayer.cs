using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    [SerializeField] AudioSource _introSource, _loopSource;

    // Start is called before the first frame update
    void Start()
    {
        _introSource.Play();
        _loopSource.PlayDelayed(_introSource.clip.length);
    }

}
