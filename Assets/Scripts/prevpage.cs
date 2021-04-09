using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prevpage : MonoBehaviour
{
    public GameObject Panelprev;
    public GameObject Panelnow;
    public void NextPanel()
    {
        if (Panelprev != null && Panelnow != null)
        {
            Panelprev.SetActive(true);
            Panelnow.SetActive(false);
        }
    }
}
