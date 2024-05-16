using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoost : MonoBehaviour
{
    // 가져와서 사용할 라이플  스크립트
    public Rifle rifle;
    // 아이템을 먹으면 재생될 오디오클립
    public AudioClip ammoBoostSound;
    // 오디오 컴포넌트
    public AudioSource audioSource;
    // 플레이어 애니메이터
    public Animator anim;

    // 아이템을 먹으면 할당해줄 체력
    private int magToGive = 30;
    // 아이템의 닿음을 알려 줄 반경 변수
    private float radius = 2.5f;

    private void Update()
    {
        // 플레이어의 위치가 아이템의 반지름 변숫값 안에 들어왔음을 판별할 if문
        if (Vector3.Distance(transform.position, rifle.transform.position) < radius)
        {
            // 반지름안에 들어왔고 F키를 누르면 
            if (Input.GetKeyDown(KeyCode.F))
            {
                // Open 애니메이션 재생
                anim.SetBool("Open", true);
                // 플레이어 컨트롤러 스크립트의 presentHealth변수값을 아이템을 먹으면 얻게될 체력 healthGive값에 할당.
                rifle.mag = magToGive;
                // healthBoostSound클립을 한번재생
                audioSource.PlayOneShot(ammoBoostSound);
                // 아이템 1.5초뒤에 파괴
                Destroy(gameObject, 1.3f);
            }
        }
    }
}
