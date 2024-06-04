using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Objective4 : MonoBehaviour
{
    public Helicopter helicopterController; // �︮���� ��Ʈ�ѷ��� ����
    private bool ishelicopter = false;


    private void Update()
    {
        
    }

    // ������ ������� ����� �޼���
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Vehicle")
        {
            // �̼� �Ϸ�
            ObjectivesComplete.occurrence.GetobjectivesDone(true, true, true, true);
            ishelicopter = true;
            StartHelicoptertoggle();
            helicopterController.ActivateHelicopter();

            // MainMenu���� �ε��մϴ�
            //SceneManager.LoadScene("MainMenu");
        }
    }
    void StartHelicoptertoggle()
    {
        if (ishelicopter)
        {
            helicopterController.StartHelicopter();
        }
    }
}
