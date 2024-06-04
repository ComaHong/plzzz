using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivesComplete : MonoBehaviour
{
    // ������ ��ǥ�� ǥ���� �ؽ�Ʈ UI ��ҵ�
    [Header("ObjectivesComplete")]
    public Text objective1;
    public Text objective2;
    public Text objective3;
    public Text objective4;

    

    // �� ��ũ��Ʈ�� �ν��Ͻ��� ���������� ������ �� �ֵ��� ���� ������ ����
    public static ObjectivesComplete occurrence;

    // ��ũ��Ʈ�� Ȱ��ȭ�� �� ȣ��Ǵ� �޼���
    private void Awake()
    {
        if(occurrence == null ) 
        {
            // occurrence ������ �� ��ũ��Ʈ�� �ν��Ͻ��� �Ҵ��Ͽ� ���������� ���� �����ϰ� ��
            occurrence = this;
        }
       
    }

    // �ܺο��� ȣ���Ͽ� ��ǥ ���¸� ������Ʈ�ϴ� �޼���
    public void GetobjectivesDone(bool obj1, bool obj2, bool obj3, bool obj4)
    {
        if(obj1 == true)
        {
            // ��ǥ�� �Ϸ�� ��� �ؽ�Ʈ�� �����ϰ� ������ �ʷϻ����� ����
            objective1.text = "1. Completed";
            objective1.color = Color.green;
        }
        else
        {
            // ��ǥ�� �Ϸ���� ���� ��� �ؽ�Ʈ�� �����ϰ� ������ ������� ����
            objective1.text = "1. Find the Rifle";
            objective1.color = Color.white;
        }
        // �� ��° ��ǥ ���� ������Ʈ (���� ������ ������� ó��)
        if (obj2 == true)
        {
            objective2.text = "2. Completed";
            objective2.color = Color.green;
        }
        else
        {
            objective2.text = "2. Locate the villagers";
            objective2.color = Color.white;
        }
        // �� ��° ��ǥ ���� ������Ʈ (���� ������ ������� ó��)
        if (obj3 == true)
        {
            objective3.text = "3. Completed";
            objective3.color = Color.green;
        }
        else
        {
            objective3.text = "3. Find Vehicle";
            objective3.color = Color.white;
        }
        // �� ��° ��ǥ ���� ������Ʈ (���� ������ ������� ó��)
        if (obj4 == true)
        {
            objective4.text = "4. Mission Completed";
            objective4.color = Color.green;
            
          
        }
        else
        {
            objective4.text = "4. Get all villagers into vehicle";
            objective4.color = Color.white;
        }
    }

}
