using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("메인씬의 메뉴")]
    public GameObject selectCharacter;
    public GameObject mainMenu;
    public InventoryObject inventory;

    // 캐릭터 선택 버튼을 클릭했을 때 호출되는 메서드
    public void OnSelectCharater()
    {
        // 캐릭터 선택 메뉴를 활성화하고 메인 메뉴를 비활성화합니다.
        selectCharacter.SetActive(true);
        mainMenu.SetActive(false);
    }
    // 플레이 버튼을 클릭했을 때 호출되는 메서드
    public void OnPlayButton()
    {
        // "ZombieLand" 씬으로 이동합니다.
        SceneManager.LoadScene("ZombieLand");
    }
    // 종료버튼을 클릭했을 때 호출되는 메서드
    public void OnQuitButton()
    {
        // 게임을 종료합니다.
        Debug.Log("게임 끄는중. . . ");
        Application.Quit();
        inventory.Container.Clear();    
    }
}
