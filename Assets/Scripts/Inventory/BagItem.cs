using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BagItem : GoodItem
{
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        if (itemData != null)
        {
            transform.SetParent(transform.parent.parent);
            transform.position = eventData.position;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            //如果描述界面显示 就把他关闭

            InventoryManager.GetInstance().descripPanel.GetComponent<DescripPanel>().HidePanel();
            InventoryManager.GetInstance().slotBagList[slotIndex].GetComponent<BagSlot>().isEnter = false;
        }
    }
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        //slotIndex已经在drop逻辑内重新赋值 
        transform.SetParent(InventoryManager.GetInstance().slotBagList[slotIndex].transform);
        transform.position = transform.parent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
