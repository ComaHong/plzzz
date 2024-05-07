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
    public GameObject akmObject; // �� ������Ʈ

    [Header("Rifle Ammunition and shooting")]// �Ѿ˰� ����
    private int maximumammunition = 32; // �ִ� �Ѿ� ��
    public int mag = 10; // źâ�� ����ִ� �Ѿ� ��
    private int presentAmmunition; // ���� �Ѿ� ��
    public float reloadingTime = 2.0f; // ���� �ð�
    private bool setReloading = false; // ���� ������ ���θ� ��Ÿ���� �÷���

    [Header("Rifle Effects")] //�ѱ� ����Ʈ
    public ParticleSystem muzzleSpark; // ����� �ѱ�ȭ�� ��ƼŬ
    public GameObject WoodedEffect; // �Ѿ��� ������ �˷��ִ� ���� ����Ʈ ��ƼŬ
    public GameObject goreEffect; // ���� �ǰݴ����� �� �� �ִ� �� ����Ʈ ��ƼŬ

    [Header("Rifle Sound and UI")]
    public GameObject AmmoOutUI; // �Ѿ�ǥ�� UI
    public AudioClip shootingSound; // �� �߻� �Ҹ�
    public AudioClip reloadingSound; // �� ���� �Ҹ�
    public AudioSource audioSource; // ������ҽ�
    private float h;
    private float v;

    // �� ��ũ��Ʈ�� ���������� ����Ǵ� �޼���
    private void OnEnable()
    {
        // AmmoCount ��ũ��Ʈ�� UpdateAmmoText �޼��忡 presentAmmunition �Ű������� �Ҵ�
        AmmoCount.Instance.UpdateAmmoText(presentAmmunition);
        // AmmoCount ��ũ��Ʈ�� UpdateMagText �޼��忡 mag �Ű������� �Ҵ�
        AmmoCount.Instance.UpdateMagText(mag);
        // ��UI ��
        rifleUI.SetActive(true);
    }
    // play �ɶ� ���� ����� �޼���
    private void Awake()
    {
        Debug.Log("��ȹ��");
        // ���� ��ġ���� �� ������Ʈ�� �θ�� ���
        transform.SetParent(hand);
        // ���� �Ѿ��� �ִ� �Ѿ˷� ����
        presentAmmunition = maximumammunition;

    }

    void Update()
    { // ���� ���� ��� �ڵ� ���� ����
        if (setReloading)
        {
            return;
        }
        // �Ѿ��� 0 �����̰ų� R Ű�� ������ ������ ����
        if (presentAmmunition <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());

        }

        // ���콺 ���ʹ�ư�� ������ ���ӳ������� �ð��� ���� �߻�ð��̻��϶� ����� �ڵ�
        if (Input.GetButton("Fire1")/* && Time.time >= nextTimeToShoot*/)
        {
            // �ѱ�ȭ�� ��ƼŬ ���
            //muzzleSpark.Play();
            // FIre �ִϸ��̼� ���
            anim.SetBool("Fire", true);
            // Idle �ִϸ��̼� ����
            anim.SetBool("Idle", false);
            // ���� �� �߻� �ð� ����
            nextTimeToShoot = Time.time + 1f / fireCharge;
            Shoot();
        }
        // ���콺 ���ʹ�ư�� ��������
        else if (Input.GetButtonUp("Fire1"))
        {
            // �ѱ�ȭ����ƼŬ ����
            //muzzleSpark.Stop();
            // Idle �ִϸ��̼� ����
            anim.SetBool("Idle", false);
            // FireWalk �ִϸ��̼� ����
            anim.SetBool("FireWalk", true);
        }
        // ���콺 �����ʹ�ư�� ���ʹ�ư�� ���ÿ� ������ ����� �ڵ�
        else if (Input.GetButton("Fire2") && Input.GetButton("Fire1"))
        {
            // Idle �ִϸ��̼� ����
            anim.SetBool("Idle", false);
            // IdleAim �ִϸ��̼� ����
            anim.SetBool("IdleAim", true);

            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(h, 0f, v).normalized;
            // �Էµ� ���⿡ ���� �ִϸ��̼��� �����մϴ�.
            if (direction.magnitude > 0.1f)
            {
                // FireWalk �ִϸ��̼� ����
                anim.SetBool("RifleWalk", true);
            }
               
            // Walk �ִϸ��̼� ����
            //anim.SetBool("Walk", true);
           
        }
        else
        {
            // Fire �ִϸ��̼� ����
            anim.SetBool("Fire", false);
            // Idle �ִϸ��̼� ����
            anim.SetBool("Idle", true);
            // FireWalk �ִϸ��̼� ����
            anim.SetBool("FireWalk", false);
        }
        // AKM ������Ʈ�� Ȱ��ȭ ���ο� ���� �ִϸ��̼� ����
        //if (akmObject.activeSelf)
        //{
        //    // AKM�� Ȱ��ȭ�Ǿ� ������ RifleWalk �ִϸ��̼� ����
        //    anim.SetBool("RIfleWalk", true);
        //}
        //else
        //{
        //    // AKM�� ��Ȱ��ȭ�Ǿ� ������ RifleWalk �ִϸ��̼� ����
        //    anim.SetBool("RifleWalk", false);
        //}

    }
    private void Shoot()
    {

        // �Ѿ� Ȯ��
        if (mag == 0)
        {
            // �Ѿ��� ���� ��� �Ѿ� ���� UI Ȱ��ȭ
            StartCoroutine(showAmmoOut());
            // �ѱ�ȭ�� ��ƼŬ ����
            //muzzleSpark.Stop();
            // Fire�ִϸ��̼� ����
            anim.SetBool("Fire", false);
            // �Ѿ� �ؽ�Ʈ
            return;
        }
        muzzleSpark.Play();
        // ���� �Ѿ� �� ����
        presentAmmunition--;
        // ���� �Ѿ��� 0�� �Ǹ� ����� �ڵ�
        if (presentAmmunition == 0)
        {
            // źâ �� ����
            mag--;
        }
        // AmmoCount ��ũ��Ʈ�� UpdateAmmoText �޼��忡 presentAmmunition �Ű������� �Ҵ�
        AmmoCount.Instance.UpdateAmmoText(presentAmmunition);
        // AmmoCount ��ũ��Ʈ�� UpdateMagText �޼��忡 mag �Ű������� �Ҵ�
        AmmoCount.Instance.UpdateMagText(mag);

        //muzzleSpark.Play();
        // �� �߻�Ҹ� �ѹ� ���
        audioSource.PlayOneShot(shootingSound);
        // ����� Raycast
        RaycastHit hitinfo;
        // �Ѿ��� ���� ��� ���� ȿ�� �� ������ ����
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
   
    // ������ �޼���
    IEnumerator Reload()
    {
        // �÷��̾� �̵� �ӵ� ����
        playerController.walkSpeed = 0f;
        // �÷��̾� �ٴ� �ӵ� ����
        playerController.runSpeed = 0f;
        // ���������� true
        setReloading = true;
        Debug.Log("������...");
        // �ѱ�ȭ����ƼŬ ����
        muzzleSpark.Stop();
        // Reload�ִϸ��̼� ����
        anim.SetTrigger("Reload");
        // ���� �Ҹ� �ѹ� ����
        audioSource.PlayOneShot(reloadingSound);
        // ������ �ð��� ��ٸ�
        yield return new WaitForSeconds(reloadingTime);
        // ���� �Ѿ˿� ��ü �Ѿ��� �Ҵ�
        presentAmmunition = maximumammunition;
        // �÷��̾� �̵� �ӵ� ������� �ǵ���
        playerController.walkSpeed = 1.9f;
        // �÷��̾� �ٴ� �ӵ� ������� �ǵ���
        playerController.runSpeed = 3f;
        // ���������� false
        setReloading = false;


    }
    // �Ѿ� ���� UI ǥ��
    IEnumerator showAmmoOut()
    {
        // AmmoOutUI ��
        AmmoOutUI.SetActive(true);
        // 5�� ��ٸ�
        yield return new WaitForSeconds(5f);
        // AmmoOutUI ��
        AmmoOutUI.SetActive(false);
    }

}
