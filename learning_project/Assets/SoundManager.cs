using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    // members //
    public AudioSource loopAudio;
    // end - members //

    // Start is called before the first frame update
    void Start()
    {
        loopAudio = GetComponent<AudioSource>();
        loopAudio.Play(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
