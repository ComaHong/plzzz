using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective2 : MonoBehaviour
{
    // �÷��̾�� ������� ����� �޼���
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // �̼� �Ϸ�
            ObjectivesComplete.occurrence.GetobjectivesDone(true, true, false, false);

          Destroy(gameObject, 2f);
        }
    }
}