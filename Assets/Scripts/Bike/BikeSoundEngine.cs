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

    private bool isPlaying = false;
    private float thrustDiff = 0;

    public override void SetEngineState ( float thrust, bool reverse ) {

        if (this.bodyAudioSource == null )
        {
            this.thrustDiff = thrust;
            this.playIdle( );
        }

        if ( this.thrustDiff != thrust )
        {
            this.thrustDiff = thrust;
            this.isPlaying = false;
        }

        if (thrust == 0)
        {
            this.playIdle( );
            return;
        }

        this.bodyAudioSource.volume = thrust;
        this.bodyAudioSource.pitch = thrust;

        if ( thrust > .5 )
            this.playLoop( );
        else
            this.playAcc( thrust );
    }

    private void playIdle ( )
    {
        playAudio(idle, true, 1, 1);
    }

    private void playAcc ( float thrust )
    {
        playAudio(acc, false, null, null);
    }

    private void playLoop ( )
    {
        playAudio(loop, true, null, 1);
    }

    private void playAudio(AudioClip clip, bool loop, float? volume = null, float? pitch = null)
    {
        if ( this.isPlaying )
            return;

        this.bodyAudioSource.clip = clip;
        this.bodyAudioSource.loop = loop;
        
        if (pitch != null)
            this.bodyAudioSource.pitch = pitch ?? 1;
        if (volume != null)
            this.bodyAudioSource.volume = volume ?? 1;

        this.bodyAudioSource.Play( );
        this.isPlaying = true;
    }

}
