using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("메인씬의 메뉴")]
    public GameObject selectCharacter;
    public GameObject mainMenu;
   

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnSelectCharater()
    {
        selectCharacter.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene("ZombieLand");
    }

    public void OnQuitButton()
    {
        Debug.Log("게임 끄는중. . . ");
        Application.Quit();
    }
}
