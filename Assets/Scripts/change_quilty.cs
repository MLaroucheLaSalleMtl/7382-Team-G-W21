using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class change_quilty : MonoBehaviour
{
    [SerializeField]private Dropdown dorpdown;
    [SerializeField]private string[] quiltyname;
    // Start is called before the first frame update
    void Start()
    {
        dorpdown = GetComponent<Dropdown>();
        quiltyname = QualitySettings.names;
        List<string> DropOption = new List<string>();
        foreach (string str in quiltyname)
        {
            DropOption.Add(str);
        }
        dorpdown.AddOptions(DropOption);
        dorpdown.value = QualitySettings.GetQualityLevel();

    }
    public void Setquilty()
    {
        QualitySettings.SetQualityLevel(dorpdown.value, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
