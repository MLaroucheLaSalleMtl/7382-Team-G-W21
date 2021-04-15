using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class setanimalsvol : MonoBehaviour
{
    public AudioMixer mixer;

    public void setvol(float sliderval)
    {
        mixer.SetFloat("Animals sound", Mathf.Log10(sliderval) * 20);
    }
}
