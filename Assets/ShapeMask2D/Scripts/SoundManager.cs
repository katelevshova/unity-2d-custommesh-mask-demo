using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private static string CLASS_NAME = typeof(SoundManager).ToString();
    public const int MUTE = 1;
    public const int UN_MUTE = 2;
    public AudioMixerSnapshot snapshotMute;
    public AudioMixerSnapshot snapShotUnMute;
    public float transitionTime = 2f;

    public AudioMixer masterMixer;
    public AudioMixer soundEffects;

    public const string EXPOSED_PARAM_BG_MUSIC = "sliderBgMusic";
    

    public void TransitionToSnapshot(int triggerNumber)
    {
        switch (triggerNumber)
        {
            case MUTE:
                snapshotMute.TransitionTo(transitionTime);
                break;
            case UN_MUTE:
                snapShotUnMute.TransitionTo(transitionTime);
                break;
        }
    }

    public void SetVolumeLevel(string exposedParamName, float sliderValue)
    {
        float convertedVolume = 0f;

        // Formulas have been generated with a help of https://www.wolframalpha.com
        // NOTE: Slider min value must be 1 and not 0 because lg(0)=1 which gets us to wrong result - instead of expected -80db we get -45 db

        switch (exposedParamName)
        {
            // Default volume level -10db
            case EXPOSED_PARAM_BG_MUSIC:
                //range from -80 to -10db for BgMusic, plot lg(x)*35 - 80, x=1 to 100, because Slider minValue=1 and maxValue=100
                //How do we get 35? the range from -80 to -10db is 70, so divide by 2 = 35
                convertedVolume = Mathf.Log10(sliderValue) * 35 - 80;
                masterMixer.SetFloat(exposedParamName, convertedVolume);
                break;
        }
        //NOTE: here you can add more sliders 
    }
}
