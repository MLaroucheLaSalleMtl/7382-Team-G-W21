using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class WeaponManager : MonoBehaviour
{
    private static WeaponManager instance;

    public static WeaponManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    public WeaponType curEquipWeapon = WeaponType.Null;
    public List<GameObject> templist = new List<GameObject>();
    public GameObject info;
    public Text infoText;

    public GameObject RequiredMaterials;
    public GameObject tips;

    // Start is called before the first frame update
    void Start()
    {
        info.SetActive(false);
        RequiredMaterials.SetActive(false);

    }

    // Update is called once per frame
 void Update()
    {

    }
   
    public string GetMaterialsData(WeaponType weaponType)
    {
        string dataStr = "";
        switch (weaponType)
        {
            case WeaponType.Sword:
                //两个木头三个石头
                dataStr = "1_2/0_3";
                materialsdata = dataStr;
                weaponItemId = 3;

                break;
            case WeaponType.Axe:
                //一个木头五个石头
                dataStr = "1_1/0_5";
                materialsdata = dataStr;
                weaponItemId = 4;

                break;

            case WeaponType.Bow:
                //一个木头五个石头
                dataStr = "1_1/0_1";
                materialsdata = dataStr;
                weaponItemId = 6;
                break;

            case WeaponType.Shield:
                dataStr = "1_3/0_5";
                materialsdata = dataStr;
                weaponItemId = 7;
                break;

            default:
                dataStr = "";
                break;
        }
        return dataStr;
    }

    string materialsdata = "";
    int weaponItemId =-1; //default id

    public void Craft()
    {
        var materialstype = materialsdata.Split('/');
        List<ItemData> itemDatas = InventoryManager.GetInstance().itemBagList;
        List<GameObject> slotBagList = InventoryManager.GetInstance().slotBagList;

        //First required material
        int mId1 = int.Parse(materialstype[0].Split('_')[0]);
        int mNum1 = int.Parse(materialstype[0].Split('_')[1]);
        //second required material
        int mId2 = int.Parse(materialstype[1].Split('_')[0]);
        int mNum2 = int.Parse(materialstype[1].Split('_')[1]);

        Debug.Log(mId1+ mId2);
        var itemdata1= itemDatas.FirstOrDefault(t=>t.ID == mId1);

        var itemdata2= itemDatas.FirstOrDefault(t=>t.ID == mId2);

        if (itemdata1 != null && itemdata2 != null && itemdata1.Num >= mNum1 && itemdata2.Num >= mNum2)
        {
            Debug.Log("Enough material");
            itemdata1.Num -= mNum1;
            itemdata2.Num -= mNum2;
            tips.SetActive(true);
            tips.GetComponentInChildren<Text>().text ="The weapon is made, please check your inventory！";
            InventoryManager.GetInstance().Additem(weaponItemId);
            Debug.Log(InventoryManager.GetInstance().itemDataList[weaponItemId]);
        }
        else
        {
            tips.SetActive(true);
            tips.GetComponentInChildren<Text>().text = "Not enough material!";
        }
    }
}
