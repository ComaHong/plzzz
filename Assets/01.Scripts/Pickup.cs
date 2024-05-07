using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [Header("Rifle's")]
    public GameObject playerRifle; // 플레이어가 지니고 있는 라이플
    public GameObject PickupRifle; // 미리 생성 되있는 픽업 할 수 있는 라이플
    public PlayerPunch playerPunch; // 플레이어의 펀치
    public GameObject rifleUI; // 총기UI

    [Header("Rifle Assign Things")]
    public PlayerController playerController; // 가져다 쓸 플레이어 컨트롤러 스크립트
    public Animator anim; // 플레이어 애니메이터
    private float radius = 2.5f; // 플레이어의 주변을 체크한 반지름 변수
    private float nextTimeToPunch = 0f; // 다음펀치까지 시간
    public float punchCharge = 15f; // 펀치모으는 시간

    private void Awake()
    {
        // 게임이 시작되면 player의 Rifle을 끔
        playerRifle.SetActive(false);
        // rifleUI도 끔
        rifleUI.SetActive(false);
    }
    private void Update()
    {
        // 마우스 왼쪽버튼을 누름과 게임안의 시간이 다음펀치시간보다 같거나 큰 조건이 둘다 충족될 경우 실행될 코드
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToPunch)
        {
            // 펀치 애니메이션 재생
            anim.SetBool("Punch", true);
            // 펀치 애니메이션 멈춤
            anim.SetBool("Idle", false);
            // 다음 펀치시간 설정
            nextTimeToPunch = Time.time + 1f / punchCharge;
            // playerPunch스크립트의 Punch메서드 호출
            playerPunch.Punch();
        }
        // 위의 코드 이외의 모든 경우에 실행될 코드
        else
        {
            // 펀치 애니메이션 중지
            anim.SetBool("Punch", false);
            // Idle애니메이션 실행
            anim.SetBool("Idle", true);
        }
        // 플레이어와 이 스크립트가 부착된 게임 오브젝트 간의 거리가 특정 반경 이내인지 확인합니다.
        if (Vector3.Distance(transform.position, playerController.transform.position) < radius)
        {
            // 플레이어가 총을 주울 수 있는 범위 내에 있고 F 키를 누르면 아래 코드가 실행됩니다.
            if (Input.GetKeyDown(KeyCode.F))
            {
                // 플레이어의 총 오브젝트를 활성화
                playerRifle.SetActive(true);
                // 픽업한 총의 오브젝트를 비활성화
                PickupRifle.SetActive(false);
                // 소리

                // 정적처리 된 objectiveComplete스크립트의 GetobjectivesDone메서드를 실행 첫번째 목표를 true로 변경 나머지 목표들은 그대로 false
                ObjectivesComplete.occurrence.GetobjectivesDone(true, false, false, false);
            }
        }
    }
}
