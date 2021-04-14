using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    [SerializeField] private GameObject[] magic_list;
    [SerializeField] private Transform Target;
    private ParticleSystem[] Magic_particle1;
    private ParticleSystem[] Magic_particle2;
    private bool stop;
    float timer;
    [SerializeField] float Set_RepeatingPertime;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject element in magic_list)
        {
            element.SetActive(false);
        }
        Magic_particle1 = magic_list[0].GetComponentsInChildren<ParticleSystem>();
        Magic_particle2 = magic_list[1].GetComponentsInChildren<ParticleSystem>();
        stop = true;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Magic_UseByDistance();
        StopFire();
        Timer();
    }
    void Magic_UseByDistance()
    {
        float distance = Vector3.Distance(transform.position, Target.position);
        //Debug.Log(distance);
            if (distance > 20.0f && distance < 30.0f)
            {
                magic_list[0].SetActive(true);
                magic_list[1].SetActive(false);
            }
            if (distance >10.0f && distance < 20.0f)
            {
                magic_list[1].SetActive(true);
                magic_list[0].SetActive(false);
            }
            if (distance > 30.0f)
            {
            magic_list[0].SetActive(false);
            magic_list[1].SetActive(false);
            }
    }
    void StopFire()
    {
        if (stop)
        {
            foreach (var system in Magic_particle1)
            {
                var emission = system.emission;
                emission.enabled = false;
            }
            foreach (var system in Magic_particle2)
            {
                var emission = system.emission;
                emission.enabled = false;
            }
        }
        if (!stop)
        {
            foreach (var system in Magic_particle1)
            {
                var emission = system.emission;
                emission.enabled = true;
            }
            foreach (var system in Magic_particle2)
            {
                var emission = system.emission;
                emission.enabled = true;
            }
        }

    }
    void Timer()
    {
        timer += 1f * Time.deltaTime;
        if (timer >= Set_RepeatingPertime)
        {
            stop = !stop;


            timer = 0f;
        }

    }


}
