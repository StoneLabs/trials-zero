using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Engine for simulating sound of a simple 2-Gear bike
 */
public class BikeSimpleSoundEngine : BikeEngine 
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
    // Volume for neutral sound
    public float volume0 = 1.0f;

    [Space(10)]

	[LabelOverride("Volume")]
    [Range(0, 1)]
    // Volume for bike sound
    public float volume1 = 1.0f;

	[LabelOverride("Pitch Curve")]
    // Pitch curve simulating Engine RPM
    public AnimationCurve pitchCurve1 = AnimationCurve.Linear(0, 0.5f, 1, 1);
    
    [Space(10)]

	[LabelOverride("Volume Reverse")]
    [Range(0, 1)]
    // Volume for reverse sound
    public float volumeR = 1.0f;

	[LabelOverride("Pitch Curve Reverse")]
    // Pitch curve simulating Engine RPM in reverse
    public AnimationCurve pitchCurveR = AnimationCurve.Linear(0, 0.5f, 1, 1);


    public override void SetEngineState ( float thrust, bool reverse ) 
    {
        //If no "thrust" is given play the idle sound
        if (thrust == 0)
            playAudio(idle, true, volume0, 1);

        //Play reverse or non-reverse sound
        if ( thrust > 0)
            if (reverse)
                playAudio(loop, true, volumeR, pitchCurveR.Evaluate(thrust));
            else
                playAudio(loop, true, volume1, pitchCurve1.Evaluate(thrust));
    }

    //Function to change current clip, pitch and volume
    private void playAudio(AudioClip clip, bool loop, float volume = 1, float pitch = 1)
    {
        this.bodyAudioSource.pitch = pitch;
        this.bodyAudioSource.volume = volume;
        
        //Only set (and therefor restart) clip if it has changed
        if (this.bodyAudioSource.clip != clip)
        {
            this.bodyAudioSource.clip = clip;
            this.bodyAudioSource.loop = loop;
            this.bodyAudioSource.Play();
        }
    }

}
