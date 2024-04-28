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
    public float giveDamegeOf = 10f; // ������ ���� ������
    public float shootingRange = 100f; // ���� �����Ÿ�
    public float fireCharge = 15f;
    private float nextTimeToShoot = 0f; //���� �ѽ������� ��Ÿ��
    public PlayerController playerController; // �÷��̾���Ʈ�ѷ� ��ũ��Ʈ
    public Transform hand; // �ڵ� ��ġ
    public Animator anim; // �÷��̾� �ִϸ�����
    public GameObject rifleUI; // �Ѿ� UI

    [Header("Rifle Ammunition and shooting")]// �Ѿ˰� ����
    private int maximumammunition = 32;
    public int mag = 10;
    private int presentAmmunition;
    public float reloadingTime = 1.3f;
    private bool setReloading = false;

    [Header("Rifle Effects")] //�ѱ� ����Ʈ
    public ParticleSystem muzzleSpark;
    public GameObject WoodedEffect;
    public GameObject goreEffect;


    private void OnEnable()
    {
        rifleUI.SetActive(true);
    }
    private void Awake()
    {
        Debug.Log("��ȹ��");
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

        // �Ѿ� Ȯ��
        if (mag == 0)
        {
            // �Ѿ� �ؽ�Ʈ
            return;
        }
        presentAmmunition--;
        if (presentAmmunition == 0)
        {
            mag--;
        }

        AmmoCount.Instance.UpdateAmmoText(presentAmmunition);
        AmmoCount.Instance.UpdateMagText(mag);

        // ���� ��� ������ ǥ��
        //Invoke("StopShooting", muzzleSpark.main.duration); // ��ƼŬ�� ��� �ð� ���Ŀ� �߻縦 �ߴ��ϵ��� ����
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
        // Ray �������� ī�޶� ��ġ�� ����
        Vector3 rayOrigin = cam.transform.position;
        // Ray ������ ī�޶��� ���� ����
        Vector3 rayDirection = cam.transform.forward;

        // Ray�� �� �信 �׸�
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
        Debug.Log("������...");
        // �ִϸ��̼� ����
        anim.SetBool("Reloading", true);
        // ���� �Ҹ� ����
        yield return new WaitForSeconds(reloadingTime);
        // �ִϸ��̼� ����
        anim.SetBool("Reloading", false);
        presentAmmunition = maximumammunition;
        playerController.walkSpeed = 1.9f;
        playerController.runSpeed = 3;
        setReloading = false;
        muzzleSpark.Stop();

    }

}
