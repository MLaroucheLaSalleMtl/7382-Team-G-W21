using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagSlot : MonoBehaviour ,IDropHandler,IPointerEnterHandler, IPointerExitHandler
{
	public int slotID;
	InventoryManager inv;
	// Use this for initialization
	void Start()
	{
		inv = InventoryManager.GetInstance();
	}

   
	public void OnDrop(PointerEventData eventData){
		BagItem droppenItem = eventData.pointerDrag.GetComponent<BagItem> ();
		if (droppenItem==null)
		{
			Debug.Log("当前拖拽无效");
			return;
		}
		if (transform.childCount == 0 || inv.itemBagList[slotID].ID == -1)
		{
			//把拖拽的item对应的槽位赋值一个新的item
			inv.itemBagList[droppenItem.slotIndex] = new ItemData();
			droppenItem.slotIndex = slotID;
			//把拖拽的item赋值给当前落下的槽位
			inv.itemBagList[slotID] = droppenItem.itemData;
		}
		//交换对象，位置
		else if (droppenItem.slotIndex != slotID)
		{
			Transform item = this.transform.GetChild(0);

			item.GetComponent<GoodItem>().slotIndex = droppenItem.slotIndex;

			item.transform.SetParent(inv.slotBagList[droppenItem.slotIndex].transform);

			item.transform.position = item.transform.parent.position;
			inv.itemBagList[droppenItem.slotIndex] = item.GetComponent<BagItem>().itemData;
			droppenItem.slotIndex = slotID;
			inv.itemBagList[slotID] = droppenItem.itemData;
		}
	}
	float temp =1f;
	public  bool isEnter;
	private void Update()
	{
		if (isEnter)
		{
			//鼠标悬停1f秒钟显示描述界面
			temp -= Time.deltaTime;
			if (temp <= 0)
			{
				InventoryManager.GetInstance().descripPanel.GetComponent<DescripPanel>().ShowPanel(inv.itemBagList[slotID].Name, inv.itemBagList[slotID].Desp);
				temp = 1f;
			}
		}
        //点击右键使用道具
        if (Input.GetMouseButtonDown(1)&& isEnter&& transform.childCount>0)
        {
			isEnter = false;
			InventoryManager.GetInstance().descripPanel.GetComponent<DescripPanel>().HidePanel();
			InventoryManager.GetInstance().UseItem(slotID);

		}
        //刷新num数
        if (GetComponentInChildren<Text>()!=null)
        {
            GetComponentInChildren<Text>().text = inv.itemBagList[slotID].Num+"";
        }
        //当前数量为0后 重置此格子
        if (transform.childCount > 0&&inv.itemBagList[slotID].Num <= 0)
        {
            inv.itemBagList[slotID].Reset();
            Destroy( GetComponentInChildren<GoodItem>().gameObject);
        }
    }
	public void OnPointerEnter(PointerEventData eventData)
	{
		////Debug.Log("鼠标进入物品槽:"+ slotID);
		if (this.transform.childCount > 0)
		{
			isEnter = true; temp = 1f;
			//获得描述文本

		}
	}

	void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
	{
		if (this.transform.childCount > 0)
		{
			isEnter = false;
			InventoryManager.GetInstance().descripPanel.GetComponent<DescripPanel>().HidePanel();
		}
	}
}
