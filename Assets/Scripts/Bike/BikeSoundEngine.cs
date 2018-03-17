using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeSoundEngine : BikeEngine 
{
    public AudioSource bodyAudioSource = null;

    public AudioClip idle;
    public AudioClip acc;
    public AudioClip shift1;
    public AudioClip shift2;
    public AudioClip unshift;
    public AudioClip loop;

    public override void SetEngineState ( float thrust, bool reverse ) 
    {
        if (thrust == 0)
            playAudio(idle, true, 1, 1);

        if ( thrust > 0)
            playAudio(loop, true, 1, thrust);
    }

    private void playAudio(AudioClip clip, bool loop, float volume = 1, float pitch = 1)
    {
        this.bodyAudioSource.pitch = pitch;
        this.bodyAudioSource.volume = volume;
        
        if (this.bodyAudioSource.clip != clip)
        {
            this.bodyAudioSource.clip = clip;
            this.bodyAudioSource.loop = loop;
            this.bodyAudioSource.Play();
        }
    }

}
