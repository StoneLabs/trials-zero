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

	[LabelOverride("Volume Neutral")]
    [Range(0, 1)]
    public float volume0 = 1.0f;

    [Space(10)]

	[LabelOverride("Volume")]
    [Range(0, 1)]
    public float volume1 = 1.0f;

	[LabelOverride("Pitch Curve")]
    public AnimationCurve pitchCurve1 = AnimationCurve.Linear(0, 0.5f, 1, 1);
    
    [Space(10)]

	[LabelOverride("Volume Reverse")]
    [Range(0, 1)]
    public float volumeR = 1.0f;

	[LabelOverride("Pitch Curve Reverse")]
    public AnimationCurve pitchCurveR = AnimationCurve.Linear(0, 0.5f, 1, 1);


    public override void SetEngineState ( float thrust, bool reverse ) 
    {
        reverse = reverse ^ inverse;

        if (thrust == 0)
            playAudio(idle, true, volume0, 1);

        if ( thrust > 0)
            if (reverse)
                playAudio(loop, true, volumeR, pitchCurveR.Evaluate(thrust));
            else
                playAudio(loop, true, volume1, pitchCurve1.Evaluate(thrust));
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
