using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Objective4 : MonoBehaviour
{
    // 차량과 닿았을때 사용할 메서드
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Vehicle")
        {
            // 미션 완료
            ObjectivesComplete.occurrence.GetobjectivesDone(true, true, true, true);
            // MainMenu씬을 로드합니다
            SceneManager.LoadScene("MainMenu");
        }
    }
}
