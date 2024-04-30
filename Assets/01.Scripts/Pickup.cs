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
            anim.SetBool("Punch", true);
            anim.SetBool("Idle", false);
            nextTimeToPunch = Time.time + 1f / punchCharge;
            playerPunch.Punch();
        }
        // 위의 코드 이외의 모든 경우에 실행될 코드
        else
        {
            anim.SetBool("Punch", false);
            anim.SetBool("Idle", true);
        }

        if (Vector3.Distance(transform.position, playerController.transform.position) < radius)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                playerRifle.SetActive(true);
                PickupRifle.SetActive(false);
                // 소리

                // 오브젝트 
                ObjectivesComplete.occurrence.GetobjectivesDone(true, false, false, false);
            }
        }
    }
}
