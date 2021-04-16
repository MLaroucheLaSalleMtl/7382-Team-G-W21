using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    int id = -1;
    GameObject TempGo;
    bool canBePickedUp;
    public GameObject pickUpText;
    GameObject WeaponWindowParent;

    // Start is called before the first frame update
    void Start()
    {
        // Type = item.type;
        WeaponWindowParent = GameObject.Find("Canvas/WeaponWindow");
        if (WeaponWindowParent != null)
            WeaponWindowParent.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))

        {
            Debug.Log("B key was pressed.");
            if (WeaponWindowParent.activeSelf)
            {
                WeaponWindowParent.SetActive(false);
            }
            else
            {
                WeaponWindowParent.gameObject.SetActive(true);
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
        //捡物品
        if (Input.GetKeyDown(KeyCode.E) && id != -1)
        {
            if (id == 0)
            {
                Debug.Log("get a stone!!");
                InventoryManager.GetInstance().Additem(0);

                Destroy(TempGo);
                canBePickedUp = false;
            }
            if (id == 1)
            {
                Debug.Log("get a wood!!");
                InventoryManager.GetInstance().Additem(1);

                Destroy(TempGo);
                canBePickedUp = false;
            }
            if (id == 2)
            {
                Debug.Log("get an apple!!");
                InventoryManager.GetInstance().Additem(2);

                Destroy(TempGo);
                canBePickedUp = false;
            }
            if (id == 5)
            {
                Debug.Log("get a pear!!");
                InventoryManager.GetInstance().Additem(5);

                Destroy(TempGo);
                canBePickedUp = false;
            }
            if (id == 8)
            {
                Debug.Log("get a deer meat!!");
                InventoryManager.GetInstance().Additem(8);

                Destroy(TempGo);
                canBePickedUp = false;
            }
            if (id == 9)
            {
                Debug.Log("get a wolf meat!!");
                InventoryManager.GetInstance().Additem(9);
                Destroy(TempGo);
                canBePickedUp = false;
            }
            if (id == 10)
            {
                Debug.Log("get a deer meat!!");
                InventoryManager.GetInstance().Additem(10);
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
        if (other.tag == "pear")
        {
            canBePickedUp = true;
            id = 5;
            TempGo = other.gameObject;
        }
        if (other.tag == "deerMeat")
        {
            canBePickedUp = true;
            id = 8;
            TempGo = other.gameObject;
        }
        if (other.tag == "wolfMeat")
        {
            canBePickedUp = true;
            id = 9;
            TempGo = other.gameObject;
        }
        if (other.tag == "bearMeat")
        {
            canBePickedUp = true;
            id = 10;
            TempGo = other.gameObject;
        }

        if (other.name == "enemy1")
        {
            //模拟敌人1死亡
            var item = Instantiate(Resources.Load<GameObject>("Item Prefab/apple"));
            item.transform.position = other.transform.position;
            Destroy(other.gameObject);
        }
        if (other.name == "enemy2")
        {

        }
    }
    private void OnTriggerExit(Collider other)
    {
        canBePickedUp = false;
    }
}
