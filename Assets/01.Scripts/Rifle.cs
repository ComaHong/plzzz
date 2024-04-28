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
    public float reloadingTime = 1.3f;
    private bool setReloading = false;

    [Header("Rifle Effects")] //총기 이펙트
    public ParticleSystem muzzleSpark;
    public GameObject WoodedEffect;
    public GameObject goreEffect;


    private void OnEnable()
    {
        rifleUI.SetActive(true);
    }
    private void Awake()
    {
        Debug.Log("총획득");
        transform.SetParent(hand);
        presentAmmunition = maximumammunition;





    }

    void Update()
    {
        if (setReloading)
        {
            return;
        }
        if (presentAmmunition <= 0)
        {
            StartCoroutine(Reload());
            return;
        }


        if (Input.GetButton("Fire1") && Time.time >= nextTimeToShoot)
        {
            muzzleSpark.Play();
            anim.SetBool("Fire", true);
            anim.SetBool("Idle", false);
            nextTimeToShoot = Time.time + 1f / fireCharge;
            Shoot();
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            //muzzleSpark.Stop();
            anim.SetBool("Idle", false);
            anim.SetBool("FireWalk", true);
        }
        else if (Input.GetButton("Fire2") && Input.GetButton("Fire1"))
        {
            anim.SetBool("Idle", false);
            anim.SetBool("IdleAim", true);
            anim.SetBool("FireWalk", true);
            anim.SetBool("Walk", true);
            anim.SetBool("Reloading", false);
        }
        else
        {
            anim.SetBool("Fire", false);
            anim.SetBool("Idle", true);
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

        AmmoCount.Instance.UpdateAmmoText(presentAmmunition);
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

    //public void Onparticleplaying()
    //{
    //    if (Input.GetButton("Fire1") && Time.time >= nextTimeToShoot)
    //    {

    //        nextTimeToShoot = Time.time + 1f / fireCharge;
    //        Shoot();

    //    }
    //    else if (Input.GetButtonUp("Fire1"))
    //    {
    //        muzzleSpark.Stop();
    //    }
    //}
    IEnumerator Reload()
    {
        playerController.walkSpeed = 0f;
        playerController.runSpeed = 0f;
        setReloading = true;
        Debug.Log("장전중...");
        // 애니메이션 실행
        anim.SetBool("Reloading", true);
        // 장전 소리 실행
        yield return new WaitForSeconds(reloadingTime);
        // 애니메이션 실행
        anim.SetBool("Reloading", false);
        presentAmmunition = maximumammunition;
        playerController.walkSpeed = 1.9f;
        playerController.runSpeed = 3;
        setReloading = false;
        muzzleSpark.Stop();

    }

}
