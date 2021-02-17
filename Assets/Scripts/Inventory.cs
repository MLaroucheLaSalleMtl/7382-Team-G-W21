using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private bool inventoryEnabled;
    public GameObject inventory;

    private int allSlots;
    private int enabledSlots;
    private GameObject[] slot;
    public Sprite woodImage;


    public GameObject slotPanel;

    void Start()
    { // to check empty
        allSlots = 20;
        slot = new GameObject[allSlots];
        for(int i = 0; i < allSlots; i++)
        {
            slot[i] = slotPanel.transform.GetChild(i).gameObject;
            if (slot[i].GetComponent<Slot>().item = null)
                slot[i].GetComponent<Slot>().empty = true;
        }
    }

    void Update()
    {
        // Open/Close the inventory window
        if (Input.GetKeyDown(KeyCode.Tab))
            inventoryEnabled = !inventoryEnabled;

        if(inventoryEnabled == true)
        {
            inventory.SetActive(true);
        }
        else
        {
            inventory.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "wood")
        {
            GameObject itemPickedUp = other.gameObject;
            Item item = itemPickedUp.GetComponent<Item>();
            //   woodImage.SetActive(true);

            AddItem(itemPickedUp, item.icon);
        }
    }

    void AddItem(GameObject itemObject, Sprite itemIcon)
    {
        for (int i = 0; i < allSlots; i++)
        {
            if(slot[i].GetComponent<Slot>().empty)
            {
                //add item to slot
               itemObject.GetComponent<Item>().pickup = true;

                slot[i].GetComponent<Slot>().item = itemObject;
                slot[i].GetComponent<Slot>().icon = woodImage;
                

                
               // itemObject.transform.parent = slot[i].transform;
               // itemObject.SetActive(false);

                slot[i].GetComponent<Slot>().UpdateSlot();
                slot[i].GetComponent<Slot>().empty = false;
            }
            return;
        }
    }
}
