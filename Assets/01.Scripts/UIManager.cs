using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("좀비씬의 모든 메뉴")]
    public GameObject pauseMenuUI;
    public GameObject EndGameMenuUI;
    public GameObject ObjectiveMenuUI;

    public static bool GameIsStopped = false;

    private void Start()
    {
        showObjectives();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsStopped)
            {
                Resume();
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Pause();
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (GameIsStopped)
            {
                removeObjective();
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                showObjectives();
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
    // 목표오브젝트들을 보여주는 UI를 활성화시키는 메서드
    public void showObjectives()
    {
        ObjectiveMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsStopped = true;
    }
    // 목표오브젝트들을 보여주는 UI를 비활성화시키는 메서드
    public void removeObjective()
    {
        ObjectiveMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsStopped = false;
    }

    // 게임을 다시 재개하는 메서드 버튼
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsStopped = false;
    }
    public void Restart()
    {
        // 메인 메뉴로 돌아가서 게임 재시작
        SceneManager.LoadScene("MainMenu");

    }
    // 메인 메뉴씬을 로드하는 메서드 버튼
    public void LoadMenu()
    {
        // 메인메뉴 씬을 로드합니다
        SceneManager.LoadScene("MainMenu");
    }
    // 게임을 종료하는 메서드 버튼
    public void QuitGame()
    {
        Debug.Log("게임 끄는중. . . ");
        Application.Quit();
    }
    // 게임을 중지시키는 메서드 버튼
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsStopped = true;
    }


}
