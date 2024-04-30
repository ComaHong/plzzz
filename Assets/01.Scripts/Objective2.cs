using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective2 : MonoBehaviour
{
    // 플레이어와 닿았을때 사용할 메서드
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // 미션 완료
            ObjectivesComplete.occurrence.GetobjectivesDone(true, true, false, false);

          Destroy(gameObject, 2f);
        }
    }
}
