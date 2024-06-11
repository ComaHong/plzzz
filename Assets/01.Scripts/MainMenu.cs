using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("���ξ��� �޴�")]
    public GameObject option;
    public GameObject mainMenu;
    public InventoryObject inventory;

   
    public GameObject loadingScreen; // �ε� ȭ�� UI
    public Slider progressBar; // ����� ǥ�� �����̴�

    // ĳ���� ���� ��ư�� Ŭ������ �� ȣ��Ǵ� �޼���
    public void OnOption()
    {
        //  �޴��� Ȱ��ȭ�ϰ� ���� �޴��� ��Ȱ��ȭ�մϴ�.
        option.SetActive(true);
        mainMenu.SetActive(false);
    }
    // �÷��� ��ư�� Ŭ������ �� ȣ��Ǵ� �޼���
    public void OnPlayButton()
    {
        // �ε� ȭ���� ���� Ȱ��ȭ�մϴ�.
        loadingScreen.SetActive(true);
       
        // �񵿱� �� �ε� �ڷ�ƾ�� �����մϴ�.
        StartCoroutine(LoadAsyncScene());
       
    }
    // �����ư�� Ŭ������ �� ȣ��Ǵ� �޼���
    public void OnQuitButton()
    {
        // ������ �����մϴ�.
        Debug.Log("���� ������. . . ");
        Application.Quit();
        inventory.Container.Clear();
    }
    IEnumerator LoadAsyncScene()
    {
        // �񵿱�� �� �ε带 �����մϴ�.
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("ZombieLand");
        asyncOperation.allowSceneActivation = false; // �� �ε� �Ϸ� �� �ڵ����� ���� Ȱ��ȭ���� �ʵ��� ����

        // ���� �ε�Ǵ� ���� ������� ������Ʈ�մϴ�.
        while (!asyncOperation.isDone)
        {
            // ������� 0���� 0.9���� ���������� ������Ʈ�˴ϴ�.
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            progressBar.value = progress;
           

            // ���� 90% �ε�Ǹ� ���� Ȱ��ȭ�� �غ� �Ϸ�� ���Դϴ�.
            if (asyncOperation.progress >= 0.9f)
            {
                // ������� 100%�� �����մϴ�.
                progressBar.value = 1f;
             
                // �ε� ȭ���� ��Ȱ��ȭ�ϰ� ���� Ȱ��ȭ�մϴ�.
               
                asyncOperation.allowSceneActivation = true;
                // ���θ޴� ��Ȱ��ȭ
                mainMenu.SetActive(false);
            }

            yield return null;
        }
    }
}
