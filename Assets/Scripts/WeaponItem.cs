using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public enum WeaponType
{
    Sword,
    Axe,
    Bow,
    Shield,
    Null
}

public class WeaponItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public WeaponType weaponType;
    private void Start()
    {
        //点击展示所需material
        GetComponent<Button>().onClick.AddListener(ShowMaterials);
    }

   
    public void ShowMaterials()
    {
        List<GameObject> templist = WeaponManager.GetInstance().templist; //储存列表

        if (templist.Count > 0)
        {
            for (int i = 0; i < templist.Count; i++)
            {
                Destroy(templist[i]);
            }
            templist.Clear();
        }

        WeaponManager.GetInstance().RequiredMaterials.SetActive(true);
        var materials = WeaponManager.GetInstance().RequiredMaterials.transform.Find("Materials");
        LayoutRebuilder.ForceRebuildLayoutImmediate(materials as RectTransform);
        string materialsData = WeaponManager.GetInstance().GetMaterialsData(weaponType);

        for (int i = 0; i < materialsData.Split('/').Length; i++)
        {
            var material = Instantiate(Resources.Load<GameObject>("Materialitem"));
            templist.Add(material);
            material.transform.parent = materials;
            material.transform.localScale = Vector3.one;
            LayoutRebuilder.ForceRebuildLayoutImmediate(materials.GetComponent<RectTransform>());
            LayoutRebuilder.ForceRebuildLayoutImmediate(materials as RectTransform);
            string spriteName = GetmaterialName(materialsData.Split('/')[i].Split('_')[0]);
            string materialNum = materialsData.Split('/')[i].Split('_')[1];
            material.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + spriteName);
            material.GetComponentInChildren<Text>().text = "*" + materialNum;
        }
    }
    public string GetmaterialName(string a)
    {
        string name="";
        if (a=="0")
        {
            name = "rock";
        }
        if (a == "1")
        {
            name = "wood";
        }
        return name;
    }

    //添加武器提示 在锻造面板
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (weaponType == WeaponType.Sword)
        {
          WeaponManager.GetInstance(). infoText.text = "This is a sword!";
        }
        if (weaponType == WeaponType.Axe)
        {
            WeaponManager.GetInstance().infoText.text = "This is an Axe!";
        }
        if (weaponType == WeaponType.Bow)
        {
            WeaponManager.GetInstance().infoText.text = "This is a Bow!";
        }
        if (weaponType == WeaponType.Shield)
        {
            WeaponManager.GetInstance().infoText.text = "This is a shield!";
        }
        WeaponManager.GetInstance().info.SetActive(true);
        if (transform.localPosition.x<0)
        {
            WeaponManager.GetInstance().info.transform.position = transform.position + new Vector3(100,0,0);
        }
        else
        {
            WeaponManager.GetInstance().info.transform.position = transform.position + new Vector3(-100, 0, 0);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        WeaponManager.GetInstance().info.SetActive(false);

    }



}
