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
    public GameObject akmObject; // 총 오브젝트

    [Header("Rifle Ammunition and shooting")]// 총알과 슈팅
    private int maximumammunition = 32; // 최대 총알 수
    public int mag = 10; // 탄창에 들어있는 총알 수
    private int presentAmmunition; // 현재 총알 수
    public float reloadingTime = 2.0f; // 장전 시간
    private bool setReloading = false; // 장전 중인지 여부를 나타내는 플래그

    [Header("Rifle Effects")] //총기 이펙트
    public ParticleSystem muzzleSpark; // 사용할 총기화염 파티클
    public GameObject WoodedEffect; // 총알이 박힘을 알려주는 나무 이펙트 파티클
    public GameObject goreEffect; // 좀비가 피격당함을 알 수 있는 피 이펙트 파티클

    [Header("Rifle Sound and UI")]
    public GameObject AmmoOutUI; // 총알표시 UI
    public AudioClip shootingSound; // 총 발사 소리
    public AudioClip reloadingSound; // 총 장전 소리
    public AudioSource audioSource; // 오디오소스
    private float h;
    private float v;

    // 이 스크립트가 켜질때마다 실행되는 메서드
    private void OnEnable()
    {
        // AmmoCount 스크립트의 UpdateAmmoText 메서드에 presentAmmunition 매개변수를 할당
        AmmoCount.Instance.UpdateAmmoText(presentAmmunition);
        // AmmoCount 스크립트의 UpdateMagText 메서드에 mag 매개변수를 할당
        AmmoCount.Instance.UpdateMagText(mag);
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
    { // 장전 중인 경우 코드 실행 중지
        if (setReloading)
        {
            return;
        }
        // 총알이 0 이하이거나 R 키를 누르면 재장전 시작
        if (presentAmmunition <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());

        }

        // 마우스 왼쪽버튼을 누르고 게임내에서의 시간이 다음 발사시간이상일때 실행될 코드
        if (Input.GetButton("Fire1")/* && Time.time >= nextTimeToShoot*/)
        {
            // 총구화염 파티클 재생
            //muzzleSpark.Play();
            // FIre 애니메이션 재생
            anim.SetBool("Fire", true);
            // Idle 애니메이션 정지
            anim.SetBool("Idle", false);
            // 다음 총 발사 시간 설정
            nextTimeToShoot = Time.time + 1f / fireCharge;
            Shoot();
        }
        // 마우스 왼쪽버튼이 떼졌을떄
        else if (Input.GetButtonUp("Fire1"))
        {
            // 총구화염파티클 멈춤
            //muzzleSpark.Stop();
            // Idle 애니메이션 정지
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

            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(h, 0f, v).normalized;
            // 입력된 방향에 따라 애니메이션을 변경합니다.
            if (direction.magnitude > 0.1f)
            {
                // FireWalk 애니메이션 실행
                anim.SetBool("RifleWalk", true);
            }
               
            // Walk 애니메이션 실행
            //anim.SetBool("Walk", true);
           
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
        // AKM 오브젝트의 활성화 여부에 따라 애니메이션 실행
        //if (akmObject.activeSelf)
        //{
        //    // AKM이 활성화되어 있으면 RifleWalk 애니메이션 실행
        //    anim.SetBool("RIfleWalk", true);
        //}
        //else
        //{
        //    // AKM이 비활성화되어 있으면 RifleWalk 애니메이션 정지
        //    anim.SetBool("RifleWalk", false);
        //}

    }
    private void Shoot()
    {

        // 총알 확인
        if (mag == 0)
        {
            // 총알이 없는 경우 총알 부족 UI 활성화
            StartCoroutine(showAmmoOut());
            // 총구화염 파티클 멈춤
            //muzzleSpark.Stop();
            // Fire애니메이션 중지
            anim.SetBool("Fire", false);
            // 총알 텍스트
            return;
        }
        muzzleSpark.Play();
        // 현재 총알 수 감소
        presentAmmunition--;
        // 현재 총알이 0이 되면 실행될 코드
        if (presentAmmunition == 0)
        {
            // 탄창 수 감소
            mag--;
        }
        // AmmoCount 스크립트의 UpdateAmmoText 메서드에 presentAmmunition 매개변수를 할당
        AmmoCount.Instance.UpdateAmmoText(presentAmmunition);
        // AmmoCount 스크립트의 UpdateMagText 메서드에 mag 매개변수를 할당
        AmmoCount.Instance.UpdateMagText(mag);

        //muzzleSpark.Play();
        // 총 발사소리 한번 재생
        audioSource.PlayOneShot(shootingSound);
        // 사용할 Raycast
        RaycastHit hitinfo;
        // 총알이 맞은 대상에 따라 효과 및 데미지 적용
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
                GameObject Woodgo = Instantiate(WoodedEffect, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));
                Destroy(Woodgo, 1f);
                Destroy(goreEffectGo, 0.5f);
            }
            else if (zombie2 != null)
            {
                zombie2.zombieHitDamage(giveDamegeOf);
                GameObject goreEffectGo = Instantiate(goreEffect, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));
                GameObject Woodgo = Instantiate(WoodedEffect, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));
                Destroy(Woodgo, 1f);
                Destroy(goreEffectGo, 0.5f);
            }
        }
    }
   
    // 재장전 메서드
    IEnumerator Reload()
    {
        // 플레이어 이동 속도 감소
        playerController.walkSpeed = 0f;
        // 플레이어 뛰는 속도 감소
        playerController.runSpeed = 0f;
        // 재장전상태 true
        setReloading = true;
        Debug.Log("장전중...");
        // 총구화염파티클 멈춤
        muzzleSpark.Stop();
        // Reload애니메이션 실행
        anim.SetTrigger("Reload");
        // 장전 소리 한번 실행
        audioSource.PlayOneShot(reloadingSound);
        // 재장전 시간을 기다림
        yield return new WaitForSeconds(reloadingTime);
        // 현재 총알에 전체 총알을 할당
        presentAmmunition = maximumammunition;
        // 플레이어 이동 속도 원래대로 되돌림
        playerController.walkSpeed = 1.9f;
        // 플레이어 뛰는 속도 원래대로 되돌림
        playerController.runSpeed = 3f;
        // 재장전상태 false
        setReloading = false;


    }
    // 총알 부족 UI 표시
    IEnumerator showAmmoOut()
    {
        // AmmoOutUI 켬
        AmmoOutUI.SetActive(true);
        // 5초 기다림
        yield return new WaitForSeconds(5f);
        // AmmoOutUI 끔
        AmmoOutUI.SetActive(false);
    }

}
