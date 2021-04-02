using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class hpdisplay : MonoBehaviour
{
    private Text HP;
    public GameObject Monsters;
    private attack Monsters_property;
    public GameObject slider;
    private Slider myslider;
    void Start()
    {
        HP = GetComponent<Text>();
        Monsters_property = Monsters.GetComponent<attack>();
        myslider = slider.GetComponent<Slider>();
    }
    public void set_hpslider()
    {
        myslider.value = Monsters_property.Hp;
    }
    // Update is called once per frame
    void Update()
    {
        HP.text = Monsters_property.Hp + "/" + Monsters_property.Max_hp;
        set_hpslider();
    }
}
