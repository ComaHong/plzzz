using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SelectCharacter : MonoBehaviour
{
    public GameObject selectCharacter;
    public GameObject mainMenu;
  
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
