using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class monstersound : MonoBehaviour
{
    public AudioMixer mixer;

    public void setvol(float sliderval)
    {
        mixer.SetFloat("Monsterssound", Mathf.Log10(sliderval) * 20);
    }
}
