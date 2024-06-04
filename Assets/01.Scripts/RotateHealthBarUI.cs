using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHealthBarUI : MonoBehaviour
{
    // 카메라의 위치값을 받아올 변수
    public Transform MainCamera;

    public void Awake()
    {
     
    }

    // LateUpdate 메서드는 모든 업데이트가 끝난 후 호출됩니다.
    private void LateUpdate()
    {
        // HealthBar UI를 항상 메인 카메라를 향하도록 설정합니다.
        // 즉, HealthBar가 항상 플레이어 캐릭터를 따라 회전합니다.
        // transform.position은 HealthBar의 현재 위치를 나타내고,
        // MainCamera.forward는 메인 카메라의 정면 방향을 나타냅니다.
        // 따라서 LookAt 메서드를 사용하여 HealthBar를 메인 카메라의 정면 방향으로 회전시킵니다.
        transform.LookAt(transform.position + MainCamera.forward);
    }
}
