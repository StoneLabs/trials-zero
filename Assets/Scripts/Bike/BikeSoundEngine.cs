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
        {

            this.playLoop( );

        }else
        {

            this.playAcc( thrust );

        }
    }

    private void playIdle ( )
    {
        if ( this.isPlaying )
        {

            return;

        }

        this.bodyAudioSource.volume = 1;

        this.bodyAudioSource.clip = this.idle;

        this.bodyAudioSource.pitch = 1;

        this.bodyAudioSource.loop = true;

        this.bodyAudioSource.Play( );

        this.isPlaying = true;

    }

    private void playAcc ( float thrust )
    {
        if ( this.isPlaying )
        {

            return;

        }

        this.bodyAudioSource.clip = this.acc;

        this.bodyAudioSource.loop = true;

        this.bodyAudioSource.Play( );

        this.isPlaying = true;

    }

    private void playLoop ( )
    {

        if ( this.isPlaying )
        {

            return;

        }
        
        this.bodyAudioSource.clip = this.loop;

        this.bodyAudioSource.loop = true;

        this.bodyAudioSource.pitch = 1;

        this.bodyAudioSource.Play( );

        this.isPlaying = true;

    }

}
