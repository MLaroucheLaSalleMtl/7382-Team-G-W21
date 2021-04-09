using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penalloader : MonoBehaviour
{
    public GameObject panel;
    public void getsetting()
    {
        if (panel != null)
        {
            panel.SetActive(true);
        }
    }
}
