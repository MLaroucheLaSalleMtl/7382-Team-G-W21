using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class setseavolume : MonoBehaviour
{
    public AudioMixer mixer;

    public void setvol(float sliderval)
    {
        mixer.SetFloat("sea sound", Mathf.Log10(sliderval) * 20);
    }
}
