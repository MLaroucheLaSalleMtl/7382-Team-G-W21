using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
	private static InventoryManager instance;
	public static InventoryManager GetInstance()
	{
	   return instance;
	}
	public GameObject item;
    public Character character;
    //当前背包的所有物品槽
    public List<GameObject> slotBagList = new List<GameObject>();
	//当前背包内的所有物品Item,包括空的槽内的
	public List<ItemData> itemBagList = new List<ItemData>();

	//游戏运行中获取的道具
	public List<ItemData> tempItemList = new List<ItemData>();

	//配置文件读取到的数据
	public List<ItemData> itemDataList = new List<ItemData>();
	//背包父节点
	GameObject slotBagParent;
	public  DescripPanel descripPanel;

	void Start()
	{
		instance = this;
		descripPanel = GameObject.Find("DescripPanel").GetComponent<DescripPanel>();
		slotBagParent = GameObject.Find("Canvas/Inventory/SlotParent");
        slotBagParent.SetActive(false);
        //初始化道具
        Rock stone = new Rock();
		Wood wood = new Wood();
        Apple apple = new Apple();
        Sword sword = new Sword();
        Axe axe = new Axe();

        //添加的物品种类
        itemDataList.Add(new ItemData(stone.iD, stone.Name, stone.Desp, "rock"));
		itemDataList.Add(new ItemData(wood.iD, wood.Name, wood.Desp, "wood"));
        itemDataList.Add(new ItemData(apple.iD, apple.Name, apple.Desp, "apple"));
        itemDataList.Add(new ItemData(sword.iD, sword.Name, sword.Desp, "sword"));
        itemDataList.Add(new ItemData(axe.iD, axe.Name, axe.Desp, "axe"));

        //InitItemPrefab();
        InitBag();
	}

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (slotBagParent.activeSelf)
            {
                slotBagParent.SetActive(false);
            }
            else
            {
                slotBagParent.gameObject.SetActive(true);
                UpdataBag();
            }
        }
       
	}
  
    private void InitBag()
	{
		//初始化背包槽位
		for (int i = 0; i < slotBagList.Count; i++)
		{
			slotBagList[i].AddComponent<BagSlot>();
			slotBagList[i].transform.SetParent(slotBagParent.transform);
			slotBagList[i].GetComponent<BagSlot>().slotID = i;
			itemBagList.Add(new ItemData());
		}
	}
	public void UpdataBag()
    {
        if (tempItemList.Count>0)
        {
            foreach (var item in tempItemList)
            {
				Additem(item.ID);
			}
        }
		tempItemList.Clear();
	}

	public void Additem(int itemId)
	{
		ItemData tempItem =null;
		var item = itemDataList.FirstOrDefault(a => a.ID == itemId);
        if (item != null)
        {
			tempItem = new ItemData(item.ID,item.Name,item.Desp,item.Sprite.name);
		}
		bool isExist = itemBagList.FirstOrDefault(t => t.ID == itemId) == null ? false : true;
		//当前背包内是否已经存在同类型
		if (isExist)
		{
			for (int i = 0; i < itemBagList.Count; i++)
			{
				if (itemBagList[i].ID == itemId)
				{
                    itemBagList[i].Num++;
                    Debug.Log(itemBagList[i].Num);
                    GoodItem data = slotBagList[i].transform.GetChild(0).GetComponent<GoodItem>();
					data.transform.GetChild(0).GetComponent<Text>().text = itemBagList[i].Num.ToString();
				}
			}
		}
		else
		{
			CreatNewItem(tempItem);
		}
	}

	void CreatNewItem(ItemData itemToAdd)
	{
		if (itemBagList.FirstOrDefault(t => t.ID == -1) == null)
		{
			Debug.LogError("存储已满");
			return;
		}
		Debug.Log("获取物品："+ itemToAdd.Name);
		for (int i = 0; i < itemBagList.Count; i++)
		{
			if (itemBagList[i].ID == -1)
			{
				itemBagList[i] = itemToAdd;
                itemBagList[i].Num++;

                GameObject itemObj = Instantiate(item);
                itemObj.transform.localScale = Vector3.one;
                itemObj.AddComponent<BagItem>();
				itemObj.transform.SetParent(slotBagList[i].transform);
				itemObj.transform.localPosition = Vector2.zero;
				itemObj.name = itemBagList[i].Name;
				itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
				itemObj.GetComponentInChildren<Text>().text = "1";
				itemObj.GetComponent<BagItem>().itemData = itemToAdd;
				itemObj.GetComponent<BagItem>().slotIndex = i;
				break;
			}
		}
	}

	public void UseItem(int slotID)
    {
        itemBagList[slotID].Num--;
        GoodItem goodItem =   slotBagList[slotID].GetComponentInChildren<GoodItem>();

        goodItem.transform.GetChild(0).GetComponent<Text>().text = itemBagList[slotID].Num.ToString();
		if (itemBagList[slotID].Num <= 0)
        {
			itemBagList[goodItem.slotIndex].Reset();
			Destroy(goodItem.gameObject);
			InventoryManager.GetInstance().descripPanel.GetComponent<DescripPanel>().HidePanel();
		}

        //道具使用
		Debug.Log("使用道具" + goodItem.name);
        if (goodItem.name=="Stone")
        {

        }
        if (goodItem.name == "Apple")
        {
            //character.Recover();
            Invoke("Late", 0.5f);
        }
        if (goodItem.name == "Sword")
        {
            WeaponManager.GetInstance().curEquipWeapon = WeaponType.Sword;
        }
	}
    private void Late()
    {
        TaskManager.GetInstance().isEat = true;
    }
}
