using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType {apple, wood, rock,stone}

public class Item : MonoBehaviour
{
    public Sprite icon;
    public ItemType type;
    //   public int itemID;
    public static string typeInfo;

    public void ItemUsage()
    {
       
    }
}
