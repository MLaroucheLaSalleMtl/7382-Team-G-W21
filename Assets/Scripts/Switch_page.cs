using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_page : MonoBehaviour
{
    public GameObject PanelNext;
    public GameObject PanelBefore;
    public void NextPanel()
    {
        Debug.Log("NextPanel");
        if (PanelNext != null && PanelBefore != null)
        {
            PanelNext.SetActive(true);
            PanelBefore.SetActive(false);
        }
    }
}
