﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//wrote by iris
public class GoodItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public ItemData itemData;
	public int slotIndex;

	public virtual void OnBeginDrag(PointerEventData eventData){
		
	}
	public virtual void OnDrag(PointerEventData eventData){
		if(itemData != null){
			transform.position = eventData.position;
		}
	}
	public virtual void OnEndDrag(PointerEventData eventData){
	
	}
}
