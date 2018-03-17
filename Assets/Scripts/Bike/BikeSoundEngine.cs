using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeSoundEngine : BikeEngine 
{
	[LabelOverride("Audio Source")]
    public AudioSource bodyAudioSource = null;


	[Header("Sound Files")]

	[LabelOverride("Idle Audio")]
    public AudioClip idle;

	[LabelOverride("Loop Audio")]
    public AudioClip loop;


	[Header("Audio settings")]

	[LabelOverride("Pitch Curve")]
    public AnimationCurve pitchCurve = AnimationCurve.Linear(0, 0.5f, 1, 1);


    public override void SetEngineState ( float thrust, bool reverse ) 
    {
        if (thrust == 0)
            playAudio(idle, true, 1, 1);

        if ( thrust > 0)
            playAudio(loop, true, 1, pitchCurve.Evaluate(thrust));
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
