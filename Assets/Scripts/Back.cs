using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back : MonoBehaviour
{
    public GameObject panel;
    public void closesetting()
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }
}
