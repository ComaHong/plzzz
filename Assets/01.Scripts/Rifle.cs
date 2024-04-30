using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Rifle : MonoBehaviour
{
    [Header("Rifle Things")]
    public GameObject cam;
    public float giveDamegeOf = 10f; // 총으로 가할 데미지
    public float shootingRange = 100f; // 총의 사정거리
    public float fireCharge = 15f;
    private float nextTimeToShoot = 0f; //다음 총쏘기까지의 쿨타임
    public PlayerController playerController; // 플레이어컨트롤러 스크립트
    public Transform hand; // 핸드 위치
    public Animator anim; // 플레이어 애니메이터
    public GameObject rifleUI; // 총알 UI

    [Header("Rifle Ammunition and shooting")]// 총알과 슈팅
    private int maximumammunition = 32;
    public int mag = 10;
    private int presentAmmunition;
    public float reloadingTime = 2.0f;
    private bool setReloading = false;

    [Header("Rifle Effects")] //총기 이펙트
    public ParticleSystem muzzleSpark; // 사용할 총기화염 파티클
    public GameObject WoodedEffect; // 총알이 박힘을 알려주는 나무 이펙트 파티클
    public GameObject goreEffect; // 좀비가 피격당함을 알 수 있는 피 이펙트 파티클

    // 이 스크립트가 켜질때마다 실행되는 메서드
    private void OnEnable()
    {
        // 총UI 켬
        rifleUI.SetActive(true);
    }
    // play 될때 먼저 실행될 메서드
    private void Awake()
    {
        Debug.Log("총획득");
        // 총의 위치값을 손 오브젝트의 부모로 등록
        transform.SetParent(hand);
        // 현재 총알을 최대 총알로 변경
        presentAmmunition = maximumammunition;





    }

    void Update()
    {// 리로딩이 진행되면 더이상 코드가 실행되지못하고 if문을 빠져나감
        if (setReloading)
        {
            return;
        }
        // 총알이 0보다 작거나 같거나 키보드 R키를 눌렀을떄 실행될 코드
        if (presentAmmunition <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            
        }

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    StartCoroutine(Reload());
        //    return;
        //}

        // 마우스 왼쪽버튼을 누르고 게임내에서의 시간이 다음 슈팅시간과 같더나 더 클때 실행될 코드
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToShoot)
        {
            muzzleSpark.Play();
            anim.SetBool("Fire", true);
            anim.SetBool("Idle", false);
            nextTimeToShoot = Time.time + 1f / fireCharge;
            Shoot();
        }
        // 마우스 왼쪽버튼이 떼졌을떄
        else if (Input.GetButtonUp("Fire1"))
        {
            // 총기화염파티클 멈춤
            muzzleSpark.Stop();
            // Idle 애니메이션 중지
            anim.SetBool("Idle", false);
            // FireWalk 애니메이션 실행
            anim.SetBool("FireWalk", true);
        }
        // 마우스 오른쪽버튼과 왼쪽버튼이 동시에 눌릴떄 실행될 코드
        else if (Input.GetButton("Fire2") && Input.GetButton("Fire1"))
        {
            // Idle 애니메이션 중지
            anim.SetBool("Idle", false);
            // IdleAim 애니메이션 실행
            anim.SetBool("IdleAim", true);
            // FireWalk 애니메이션 실행
            anim.SetBool("FireWalk", true);
            // Walk 애니메이션 실행
            anim.SetBool("Walk", true);
            //// Reloading 애니메이션 중지
            //anim.SetBool("Reloading", false);
        }
        else
        {
            // Fire 애니메이션 중지
            anim.SetBool("Fire", false);
            // Idle 애니메이션 실행
            anim.SetBool("Idle", true);
            // FireWalk 애니메이션 중지
            anim.SetBool("FireWalk", false);
        }

    }
    private void Shoot()
    {

        // 총알 확인
        if (mag == 0)
        {
            // 총알 텍스트
            return;
        }
        presentAmmunition--;
        if (presentAmmunition == 0)
        {
            mag--;
        }
        // AmmoCount 스크립트의 UpdateAmmoText 메서드에 presentAmmunition 매개변수를 할당
        AmmoCount.Instance.UpdateAmmoText(presentAmmunition);
        // AmmoCount 스크립트의 UpdateMagText 메서드에 mag 매개변수를 할당
        AmmoCount.Instance.UpdateMagText(mag);

        // 총을 쏘고 있음을 표시
        //Invoke("StopShooting", muzzleSpark.main.duration); // 파티클의 재생 시간 이후에 발사를 중단하도록 설정
        RaycastHit hitinfo;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitinfo, shootingRange))
        {
            Debug.Log(hitinfo.transform.name);

            ObjectToHit objectToHit = hitinfo.transform.GetComponent<ObjectToHit>();
            Zombie1 zombie1 = hitinfo.transform.GetComponent<Zombie1>();
            Zombie2 zombie2 = hitinfo.transform.GetComponent<Zombie2>();

            if (objectToHit != null)
            {
                objectToHit.ObjectHitDamage(giveDamegeOf);
                GameObject Woodgo = Instantiate(WoodedEffect, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));
                Destroy(Woodgo, 1f);
            }
            else if (zombie1 != null)
            {
                zombie1.zombieHitDamage(giveDamegeOf);
                GameObject goreEffectGo = Instantiate(goreEffect, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));
                Destroy(goreEffectGo, 0.5f);
            }
            else if (zombie2 != null)
            {
                zombie2.zombieHitDamage(giveDamegeOf);
                GameObject goreEffectGo = Instantiate(goreEffect, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));
                Destroy(goreEffectGo, 0.5f);
            }
        }
    }
    private void OnDrawGizmos()
    {
        // Ray 시작점을 카메라 위치로 설정
        Vector3 rayOrigin = cam.transform.position;
        // Ray 방향은 카메라의 전방 방향
        Vector3 rayDirection = cam.transform.forward;

        // Ray를 씬 뷰에 그림
        Gizmos.color = Color.red;
        Gizmos.DrawRay(rayOrigin, rayDirection * shootingRange);
    }

    IEnumerator Reload()
    {
        playerController.walkSpeed = 0f;
        playerController.runSpeed = 0f;
        setReloading = true;
        Debug.Log("장전중...");
        // 애니메이션 실행
        anim.SetTrigger("Reload");
        // 장전 소리 실행
        yield return new WaitForSeconds(reloadingTime);
        // 애니메이션 실행
        //anim.SetBool("Reloading", false);
        presentAmmunition = maximumammunition;
        playerController.walkSpeed = 1.9f;
        playerController.runSpeed = 3;
        setReloading = false;
        muzzleSpark.Stop();

    }

}
