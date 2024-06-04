using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivesComplete : MonoBehaviour
{
    // 각각의 목표를 표시할 텍스트 UI 요소들
    [Header("ObjectivesComplete")]
    public Text objective1;
    public Text objective2;
    public Text objective3;
    public Text objective4;

    

    // 이 스크립트의 인스턴스를 전역적으로 접근할 수 있도록 정적 변수로 설정
    public static ObjectivesComplete occurrence;

    // 스크립트가 활성화될 때 호출되는 메서드
    private void Awake()
    {
        if(occurrence == null ) 
        {
            // occurrence 변수에 이 스크립트의 인스턴스를 할당하여 전역적으로 접근 가능하게 함
            occurrence = this;
        }
       
    }

    // 외부에서 호출하여 목표 상태를 업데이트하는 메서드
    public void GetobjectivesDone(bool obj1, bool obj2, bool obj3, bool obj4)
    {
        if(obj1 == true)
        {
            // 목표가 완료된 경우 텍스트를 변경하고 색상을 초록색으로 설정
            objective1.text = "1. Completed";
            objective1.color = Color.green;
        }
        else
        {
            // 목표가 완료되지 않은 경우 텍스트를 변경하고 색상을 흰색으로 설정
            objective1.text = "1. Find the Rifle";
            objective1.color = Color.white;
        }
        // 두 번째 목표 상태 업데이트 (위와 동일한 방식으로 처리)
        if (obj2 == true)
        {
            objective2.text = "2. Completed";
            objective2.color = Color.green;
        }
        else
        {
            objective2.text = "2. Locate the villagers";
            objective2.color = Color.white;
        }
        // 세 번째 목표 상태 업데이트 (위와 동일한 방식으로 처리)
        if (obj3 == true)
        {
            objective3.text = "3. Completed";
            objective3.color = Color.green;
        }
        else
        {
            objective3.text = "3. Find Vehicle";
            objective3.color = Color.white;
        }
        // 네 번째 목표 상태 업데이트 (위와 동일한 방식으로 처리)
        if (obj4 == true)
        {
            objective4.text = "4. Mission Completed";
            objective4.color = Color.green;
            
          
        }
        else
        {
            objective4.text = "4. Get all villagers into vehicle";
            objective4.color = Color.white;
        }
    }

}
