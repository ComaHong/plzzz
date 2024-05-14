using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("���ξ��� �޴�")]
    public GameObject selectCharacter;
    public GameObject mainMenu;
    public InventoryObject inventory;

    // ĳ���� ���� ��ư�� Ŭ������ �� ȣ��Ǵ� �޼���
    public void OnSelectCharater()
    {
        // ĳ���� ���� �޴��� Ȱ��ȭ�ϰ� ���� �޴��� ��Ȱ��ȭ�մϴ�.
        selectCharacter.SetActive(true);
        mainMenu.SetActive(false);
    }
    // �÷��� ��ư�� Ŭ������ �� ȣ��Ǵ� �޼���
    public void OnPlayButton()
    {
        // "ZombieLand" ������ �̵��մϴ�.
        SceneManager.LoadScene("ZombieLand");
    }
    // �����ư�� Ŭ������ �� ȣ��Ǵ� �޼���
    public void OnQuitButton()
    {
        // ������ �����մϴ�.
        Debug.Log("���� ������. . . ");
        Application.Quit();
        inventory.Container.Clear();    
    }
}
