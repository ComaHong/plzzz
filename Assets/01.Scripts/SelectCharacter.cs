using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SelectCharacter : MonoBehaviour
{
    public GameObject selectCharacter;
    public GameObject mainMenu;
    //public GameObject player1prefab;
    //public GameObject player2prefab;
    //public GameObject player3prefab;
    //test12 testMod;
    //void Start()
    //{
    //    // SceneManager.sceneLoaded �̺�Ʈ�� OnSceneLoaded �Լ��� �߰��Ͽ� ���� �ε�� �� ȣ��ǵ��� ��
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //    testMod = FindObjectOfType<test12>();
    //}

    //void OnDestroy()
    //{
    //    // �� ��ũ��Ʈ�� �ı��� �� �̺�Ʈ �����ʸ� ����
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}
    // ���� �ε�� �� ȣ��Ǵ� �Լ�
    //void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    // �� �̸��� ZombieScene�� ��쿡�� ����
    //    if (scene.name == "ZombieLand")
    //    {
    //        if (testMod.a == 0)
    //        {
    //            GameObject player1 = GameObject.Find("Player1");
    //            if (player1 != null)
    //            {
    //                player1.SetActive(true);
    //            }
               
    //        }
    //        else if (testMod.a == 1)
    //        {
    //            GameObject player2 = GameObject.Find("Player2");
    //            if (player2 != null)
    //            {
    //                player2.SetActive(true);
    //            }
    //        }
    //    }
    //}
    public void OnBackButton()
    {
        selectCharacter.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OnCharacter1()
    {
        //testMod.a = 0;
        SceneManager.LoadScene("ZombieLand");
        // ù ��° ��ư Ŭ�� �� ù ��° ������ �ν��Ͻ�ȭ
        //Instantiate(player1prefab, new Vector3(501, 20, 379), Quaternion.identity);
    }
    public void OnCharacter2()
    {
        //testMod.a = 1;
        SceneManager.LoadScene("ZombieLand 2");
        // �� ��° ��ư Ŭ�� �� �� ��° ������ �ν��Ͻ�ȭ
        //Instantiate(player2prefab, new Vector3(501, 20, 379), Quaternion.identity);
    }
    //public void OnCharacter3()
    //{
    //    SceneManager.LoadScene("ZombieLand");
        // �� ��° ��ư Ŭ�� �� �� ��° ������ �ν��Ͻ�ȭ
       /* Instantiate(player3prefab, new Vector3(501, 20, 379), Quaternion.identity)*/
    //}

}
