using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
       int id = -1;
    GameObject TempGo;
    public Item itemType;
    bool canBePickedUp;
    public GameObject pickUpText;

    // Start is called before the first frame update
    void Start()
    {
       // Type = item.type;

    }

    // Update is called once per frame
    void Update()
    {
        if( canBePickedUp == true)
        {
            pickUpText.SetActive(true);
        }
        else
        {
            pickUpText.SetActive(false);
        }
        //捡物品
        if (Input.GetKeyDown(KeyCode.E) && id != -1)
        {
            if (id == 0)
            {
                Debug.Log("get a stone!!");
                InventoryManager.GetInstance().tempItemList.Add(InventoryManager.GetInstance().itemDataList[id]);
                Destroy(TempGo);
                canBePickedUp = false;
            }
            if (id == 1)
            {
                Debug.Log("get a wood!!");
                InventoryManager.GetInstance().tempItemList.Add(InventoryManager.GetInstance().itemDataList[id]);
                Destroy(TempGo);
                canBePickedUp = false;
            }
            if (id == 2)
            {
                Debug.Log("get an apple!!");
                InventoryManager.GetInstance().tempItemList.Add(InventoryManager.GetInstance().itemDataList[id]);
                Destroy(TempGo);
                canBePickedUp = false;
            }

            id = -1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "rock")
        {
            canBePickedUp = true;
            id = 0;        
            TempGo = other.gameObject;
        }
        if (other.tag == "wood")
        {
            canBePickedUp = true;
            id = 1;
            TempGo = other.gameObject;
        }
        if (other.tag == "apple")
        {
            canBePickedUp = true;
            id = 2;
            TempGo = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canBePickedUp = false;
    }
}
