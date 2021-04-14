using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class change_quilty : MonoBehaviour
{
    [SerializeField] private Dropdown dorpdown;
    [SerializeField] private string[] quiltyname;
    [SerializeField] private TMP_Dropdown dropDownTMP;
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
        if (dorpdown != null)
        {
            dorpdown.AddOptions(DropOption);
            dorpdown.value = QualitySettings.GetQualityLevel();
        }

        if (dropDownTMP != null)
        {
            dropDownTMP.AddOptions(DropOption);
            dropDownTMP.value = QualitySettings.GetQualityLevel();
        }

    }
    public void Setquilty()
    {
        if (dorpdown != null)
            QualitySettings.SetQualityLevel(dorpdown.value, true);

        if (dropDownTMP != null)
            QualitySettings.SetQualityLevel(dropDownTMP.value, true);
    }
}
