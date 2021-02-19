using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject Material;
    public bool canBePickedUp;
    public int wood;
    public int rock;
    public GameObject pickUpText;

    private bool inventoryEnabled;
    public GameObject inventory;

    private int allSlots;
    private int enabledSlots;
    private GameObject[] slot;
    Item item;
    public GameObject slotPanel;

    // Start is called before the first frame update
    void Start()
    {
        wood = 0;
        rock = 0;

        // to check empty
        allSlots = 20;
        slot = new GameObject[allSlots];
        for (int i = 0; i < allSlots; i++)
        {
            slot[i] = slotPanel.transform.GetChild(i).gameObject;
            slot[i].GetComponent<Slot>().item = null;
            slot[i].GetComponent<Slot>().empty = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (canBePickedUp == true)
        {
            // The  pickup action
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Material.tag == "rock" && item != null)
                {
                    rock += 1;
                    AddItem(Material, item.icon);
                }
                if (Material.tag == "wood" && item != null)
                {
                    wood += 1;
                    AddItem(Material, item.icon);
                }
                //  Destroy(Material);
                Material.SetActive(false);
                Material = null;


                canBePickedUp = false;
            }
        }

        if (canBePickedUp == true)
        {
            pickUpText.SetActive(true);
        }
        else
        {
            pickUpText.SetActive(false);
        }

        // Open/Close the inventory window
        if (Input.GetKeyDown(KeyCode.Tab))
            inventoryEnabled = !inventoryEnabled;

        if (inventoryEnabled == true)
        {
            inventory.SetActive(true);
        }
        else
        {
            inventory.SetActive(false);
        }

    }

    void OnTriggerEnter(Collider collider)
    {
        Material = collider.gameObject;
        item = Material.GetComponent<Item>();

        if (collider.tag == "rock")
        {
            canBePickedUp = true;
        }

        if (collider.tag == "wood")
        {
            canBePickedUp = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "rock")
        {
            Material = null;
            canBePickedUp = false;
            item = null;
        }

        if (collider.tag == "wood")
        {
            Material = null;
            canBePickedUp = false;
            item = null;
        }
    }

    void AddItem(GameObject itemObject, Sprite itemIcon)
    {

        for (int i = 0; i < allSlots; i++)
        {
            Debug.Log(slot[1].GetComponent<Slot>().empty);
            //  Debug.Log(slot[2].GetComponent<Slot>().empty);
            if (slot[i].GetComponent<Slot>().empty)
            {
                Debug.Log("In the if loop");
                //add item to slot
                itemObject.GetComponent<Item>().pickup = true;

                slot[i].GetComponent<Slot>().item = itemObject;
                slot[i].GetComponent<Slot>().icon = itemIcon;
                // itemObject.transform.parent = slot[i].transform;
                // itemObject.SetActive(false);

                slot[i].GetComponent<Slot>().UpdateSlot();
                slot[i].GetComponent<Slot>().empty = false;
                return;
            }
        }
    }
}

