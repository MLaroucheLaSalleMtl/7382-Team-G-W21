using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class setBGMvol : MonoBehaviour
{
    public AudioMixer mixer;

    public void setvol(float sliderval)
    {
        mixer.SetFloat("BGM", Mathf.Log10(sliderval) * 20);
    }
}
