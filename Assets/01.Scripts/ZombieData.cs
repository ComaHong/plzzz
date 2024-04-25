using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/ZombieData", fileName = "Zombie Data")]

public class ZombieData
{
     [Header("���� ü�°� ������")]
    private float zombieHealth = 100f;
    private float presentHealth;
    public float giveDamage = 5f;

    public float zombieSpeed; // ���� �̵��ӵ�
    public float walkingpointRadius = 2f; // ���� �̵��� ������

}
