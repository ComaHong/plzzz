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
    //    // SceneManager.sceneLoaded 이벤트에 OnSceneLoaded 함수를 추가하여 씬이 로드될 때 호출되도록 함
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //    testMod = FindObjectOfType<test12>();
    //}

    //void OnDestroy()
    //{
    //    // 이 스크립트가 파괴될 때 이벤트 리스너를 제거
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}
    // 씬이 로드될 때 호출되는 함수
    //void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    // 씬 이름이 ZombieScene인 경우에만 실행
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
        // 첫 번째 버튼 클릭 시 첫 번째 프리팹 인스턴스화
        //Instantiate(player1prefab, new Vector3(501, 20, 379), Quaternion.identity);
    }
    public void OnCharacter2()
    {
        //testMod.a = 1;
        SceneManager.LoadScene("ZombieLand 2");
        // 두 번째 버튼 클릭 시 두 번째 프리팹 인스턴스화
        //Instantiate(player2prefab, new Vector3(501, 20, 379), Quaternion.identity);
    }
    //public void OnCharacter3()
    //{
    //    SceneManager.LoadScene("ZombieLand");
        // 세 번째 버튼 클릭 시 세 번째 프리팹 인스턴스화
       /* Instantiate(player3prefab, new Vector3(501, 20, 379), Quaternion.identity)*/
    //}

}
