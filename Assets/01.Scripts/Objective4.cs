using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Objective4 : MonoBehaviour
{
    public Helicopter helicopterController; // �︮���� ��Ʈ�ѷ��� ����

    
    private void Update()
    {
        helicopterController.StartHelicopter();
        helicopterController.ActivateHelicopter();
    }

    // ������ ������� ����� �޼���
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Vehicle")
        {
            // �̼� �Ϸ�
            ObjectivesComplete.occurrence.GetobjectivesDone(true, true, true, true);
            helicopterController.ActivateHelicopter();

            // MainMenu���� �ε��մϴ�
            //SceneManager.LoadScene("MainMenu");
        }
    }
}
