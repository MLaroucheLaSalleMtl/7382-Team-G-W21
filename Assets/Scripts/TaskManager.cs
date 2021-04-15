using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    private static TaskManager instance;
    public static TaskManager GetInstance()
    {
        return instance;
    }

    public GameObject taskView;
    public GameObject succesView;
    public Text taskName;
    public Text taskText;
    public int curtaskId = 0;
    public Dictionary<string,string> taskOkList ;
    public bool isEat;
    public bool isKill;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        taskOkList = new Dictionary<string, string>();
        taskOkList.Add("Stone Collector","collect stone *2");
        taskOkList.Add("Weapon Master","Make sword *1");
        taskOkList.Add("Fruit lover","pick apple *1");
        taskOkList.Add("Hunter","kill wolf *1");

        taskView.SetActive(true);
        succesView.SetActive(false);
        taskName.text = "Stone collecter";
        taskText.text = "You need to collect stone *2";
    }

    // Update is called once per frame
    void Update()
    {
        if (curtaskId==0)
        {
            var item = InventoryManager.GetInstance().itemBagList.FirstOrDefault(t => t.ID == 0);
            if (item != null&& item.Num>=2)
            {
                taskView.SetActive(true);
                succesView.SetActive(true);

                taskName.text = "Weapon Master";
                taskText.text = "Make sword *1";
                curtaskId++;
            }
        }
        if (curtaskId==1)
        {
            var item = InventoryManager.GetInstance().itemBagList.FirstOrDefault(t => t.ID == 3);
            if (item != null && item.Num >= 1)
            {
                taskView.SetActive(true);
                succesView.SetActive(true);

                taskName.text = "Fruit lover";
                taskText.text = "Eat apple *1";
                curtaskId++;
            }
        }
        if (curtaskId==2)
        {
            Debug.Log(curtaskId);
            if (isEat)
            {
                taskView.SetActive(true);
                succesView.SetActive(true);
                taskName.text = "Hunter";
                taskText.text = "Kill wolf *1";
                curtaskId++;
                Debug.Log(curtaskId);
            }
        }
        if (curtaskId == 3)
        {
            Debug.Log(curtaskId);
            if (isKill)
            {
                taskView.SetActive(true);
                succesView.SetActive(true);
                curtaskId++;
                Debug.Log(curtaskId);
            }
        }
    }
    public void Overtask()
    {
        if (curtaskId ==4)
        {
            taskView.SetActive(false);
        }
    }
}
