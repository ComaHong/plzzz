using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DIsplayInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public int x_Start;
    public int y_Start;
    public int x_Space_Between_Item;
    public int y_Space_Between_Item;
    public int Number_Of_Column;
    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
    }

    
    void Update()
    {
        UpdateDisplay();
    }
    public  void UpdateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (itemsDisplayed.ContainsKey(inventory.Container[i]))
            {
                itemsDisplayed[inventory.Container[i]].GetComponentInChildren<Text>().text = inventory.Container[i].amount.ToString("nO");

            }
            else
            {
                var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<Text>().text = inventory.Container[i].amount.ToString("nO");
                itemsDisplayed.Add(inventory.Container[i], obj);
                Debug.Log("I++");
            }
        }
    }

    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<Text>().text = inventory.Container[i].amount.ToString("nO");
            itemsDisplayed.Add(inventory.Container[i], obj);
            Debug.Log("I+++++++++");
        }
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(x_Start + (x_Space_Between_Item * (i % Number_Of_Column)), y_Start + (-y_Space_Between_Item * (i / Number_Of_Column)), 0f);
    }
   
}


