using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextpageandbeforepage : MonoBehaviour
{
    public GameObject PanelNext;
    public GameObject PanelBefore;
    public void NextPanel()
    {
        if (PanelNext != null && PanelBefore != null)
        {
            PanelNext.SetActive(true);
            PanelBefore.SetActive(false);
        }
    }
}
