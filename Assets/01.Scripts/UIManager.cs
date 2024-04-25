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
    public void showObjectives()
    {
        ObjectiveMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsStopped = true;
    }

    public void removeObjective()
    {
        ObjectiveMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsStopped = false;
    }


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

    public void LoadMenu()
    {
        // 메뉴 로드
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("게임 끄는중. . . ");
        Application.Quit();
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsStopped = true;
    }


}
