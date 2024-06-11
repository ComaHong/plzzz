using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("메인씬의 메뉴")]
    public GameObject option;
    public GameObject mainMenu;
    public InventoryObject inventory;

   
    public GameObject loadingScreen; // 로딩 화면 UI
    public Slider progressBar; // 진행률 표시 슬라이더

    // 캐릭터 선택 버튼을 클릭했을 때 호출되는 메서드
    public void OnOption()
    {
        //  메뉴를 활성화하고 메인 메뉴를 비활성화합니다.
        option.SetActive(true);
        mainMenu.SetActive(false);
    }
    // 플레이 버튼을 클릭했을 때 호출되는 메서드
    public void OnPlayButton()
    {
        // 로딩 화면을 먼저 활성화합니다.
        loadingScreen.SetActive(true);
       
        // 비동기 씬 로딩 코루틴을 시작합니다.
        StartCoroutine(LoadAsyncScene());
       
    }
    // 종료버튼을 클릭했을 때 호출되는 메서드
    public void OnQuitButton()
    {
        // 게임을 종료합니다.
        Debug.Log("게임 끄는중. . . ");
        Application.Quit();
        inventory.Container.Clear();
    }
    IEnumerator LoadAsyncScene()
    {
        // 비동기로 씬 로드를 시작합니다.
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("ZombieLand");
        asyncOperation.allowSceneActivation = false; // 씬 로드 완료 시 자동으로 씬을 활성화하지 않도록 설정

        // 씬이 로드되는 동안 진행률을 업데이트합니다.
        while (!asyncOperation.isDone)
        {
            // 진행률은 0에서 0.9까지 정상적으로 업데이트됩니다.
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            progressBar.value = progress;
           

            // 씬이 90% 로드되면 씬을 활성화할 준비가 완료된 것입니다.
            if (asyncOperation.progress >= 0.9f)
            {
                // 진행률을 100%로 설정합니다.
                progressBar.value = 1f;
             
                // 로딩 화면을 비활성화하고 씬을 활성화합니다.
               
                asyncOperation.allowSceneActivation = true;
                // 메인메뉴 비활성화
                mainMenu.SetActive(false);
            }

            yield return null;
        }
    }
}
