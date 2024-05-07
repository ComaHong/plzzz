using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("������� ��� �޴�")]
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
    // ��ǥ������Ʈ���� �����ִ� UI�� Ȱ��ȭ��Ű�� �޼���
    public void showObjectives()
    {
        ObjectiveMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsStopped = true;
    }
    // ��ǥ������Ʈ���� �����ִ� UI�� ��Ȱ��ȭ��Ű�� �޼���
    public void removeObjective()
    {
        ObjectiveMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsStopped = false;
    }

    // ������ �ٽ� �簳�ϴ� �޼��� ��ư
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsStopped = false;
    }
    public void Restart()
    {
        // ���� �޴��� ���ư��� ���� �����
        SceneManager.LoadScene("MainMenu");

    }
    // ���� �޴����� �ε��ϴ� �޼��� ��ư
    public void LoadMenu()
    {
        // ���θ޴� ���� �ε��մϴ�
        SceneManager.LoadScene("MainMenu");
    }
    // ������ �����ϴ� �޼��� ��ư
    public void QuitGame()
    {
        Debug.Log("���� ������. . . ");
        Application.Quit();
    }
    // ������ ������Ű�� �޼��� ��ư
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsStopped = true;
    }


}
