using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [Header("Rifle's")]
    public GameObject playerRifle; // 플레이어가 지니고 있는 라이플
    public GameObject PickupRifle; // 미리 생성 되있는 픽업 할 수 있는 라이플
    public PlayerPunch playerPunch;
    public GameObject rifleUI;

    [Header("Rifle Assign Things")]
    public PlayerController playerController; // 가져다 쓸 플레이어 컨트롤러 스크립트
    public Animator anim; // 플레이어 애니메이터
    private float radius = 2.5f; // 플레이어의 주변을 체크한 반지름 변수
    private float nextTimeToPunch = 0f; // 다음펀치까지 시간
    public float punchCharge = 15f; // 펀치모으는 시간

    private void Awake()
    {
        playerRifle.SetActive(false);
        rifleUI.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToPunch)
        {
            anim.SetBool("Punch", true);
            anim.SetBool("Idle", false);
            nextTimeToPunch = Time.time + 1f / punchCharge;

            playerPunch.Punch();
        }
        else
        {
            anim.SetBool("Punch", false);
            //anim.SetBool("Idle", true);
        }
        if (Vector3.Distance(transform.position, playerController.transform.position) < radius)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                playerRifle.SetActive(true);
                PickupRifle.SetActive(false);
                // 소리

                // 오브젝트 
            }
        }
    }
}
