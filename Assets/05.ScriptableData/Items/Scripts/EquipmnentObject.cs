using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = " New Equipment object", menuName = "Inventory System/Items/Equipment")]


public class EquipmnentObject : ItemObject
{
    public float atkBonus;
    public float defenceBonus;
    public void Awake()
    {
        type = ItemType.Equipment;
    }

}
