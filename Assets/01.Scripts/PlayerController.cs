using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{/// <summary>
/// public �ʵ�
/// </summary>
    [Range(1f, 10f)]
    [SerializeField] public float walkSpeed = 1.9f; // �÷��̾��� �ȴ� �ӵ�
    [Range(2f, 15f)]
    [SerializeField] public float runSpeed = 3f; // �÷��̾��� �޸��� �ӵ�
    [Header("�÷��̾� ü�� ����")]
    private float playerHealth = 100f; // �÷��̾��� ü��
    public float presentHealth; // �÷��̾��� ����ü��
    public GameObject playerDamage; // �÷��̾ �ǰݴ����� ǥ������ UI
    public HealthBar hpSlider; //hp�����̴� �������ִ� ��ũ��Ʈ
    public GameObject EndgameMenu; // ������� �޴��г�


    public Transform centerTr; // �÷��̾��� center Transform
    public Transform player; // �÷��̾��� Transform
    public Animator anim; // �÷��̾��� �ִϸ�����
    public GameObject cam; // 3��Ī����ī�޶�
    public GameObject akmObject; // �� ������Ʈ
    /* public Text cartext;*/ // ���� ��ȣ�ۿ��� �޽���

    /// <summary>
    /// private�ʵ�
    /// </summary>
    private float h; // �÷��̾��� ���� �Է��� ���� horizontal����
    private float v; // �÷��̾��� �¿� �Է��� ���� vertical����
    private float mouseX; // ī�޶� �Է¹��� ���콺 X�ప
    private float mouseY; // ī�޶� �Է¹��� ���콺 Y�ప
    private bool spbar; // �÷��̾ ���� �ߴ��� Ȯ���� boolŸ�� ����

    private Rigidbody rb; // �÷��̾��� ������ٵ� ������Ʈ
    public bool Crouch = false; //v �÷��̾ �ɾ��ִ��� Ȯ���� boolŸ�� ����
    public bool isGround = false; // ���� ��� �ִ��� Ȯ���� bool ����
    public bool isAKMActive;

    // �����ϸ� ���� �޼���
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // �����ϸ� �÷��̾��� ������ٵ� ������Ʈ�� ã�Ƽ� �Ҵ�
        Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ�� ���ɱ�
        presentHealth = playerHealth; // �÷��̾��� ����ü���� �ʱ⿡ �����Ȱ����� �Ҵ�
        hpSlider.GiveFullHealth(playerHealth); // �÷��̾��� Silder Ui�� MaxValue�� Value�� �÷��̾��� ü���� �Ҵ�


    }

    // ��� ������Ʈ üŷ�� �޼���
    void Update()
    {
        GetPlayerInputs();
        MoveAndRotate();
        Run();
        Jump();


        // CŰ�� ������ �÷��̾ �ɾ����� �ʴٸ� ������ �ڵ�
        if (Input.GetKeyDown(KeyCode.C) && !Crouch)
        {
            // IsCrouch�ִϸ��̼� ���
            anim.SetBool("IsCrouch", true);
            // �������·� ����
            Crouch = true;
            // �÷��̾��� �̵��ӵ��� ������
            walkSpeed = 2f;

        }
        // CŰ�� ������ �÷��̾ �ɾ��ִٸ� ������ �ڵ�
        else if (Input.GetKeyDown(KeyCode.C) && Crouch)
        {
            // IsCrouch�ִϸ��̼� ����
            anim.SetBool("IsCrouch", false);
            // �÷��̾ �Ͼ ���·� ����
            Crouch = false;
            // Idle�ִϸ��̼� ���
            anim.SetBool("Idle", true);
            // �̵��ӵ� ���� ���·� �ǵ���
            walkSpeed = 5f;
        }

    }
    // �÷��̾�� �ٸ� �ݶ��̴��� �´��� ������ ����� �޼���
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }

    }
    // �÷��̾�� �ٸ� �ݶ��̴��� �´�������� ����� �޼���
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }

    }
    // �÷��̾�� �ݶ��̴��� ����ִٰ� �������� ����� �޼���
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }

    }
    // �÷��̾��� �̵��� ����ϴ� �޼���
    private void GetPlayerInputs()
    {
        // �÷��̾��� ���� �� ���� �Է��� �����ɴϴ�.
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        // ���ο� Vector3 ���Ⱚ 
        Vector3 direction = new Vector3(h, 0f, v).normalized;
        
        // �Էµ� ���⿡ ���� �ִϸ��̼��� �����մϴ�.
        if (direction.magnitude > 0.1f)
        {
            // �̵� ���� ���� �ִϸ��̼��� �����մϴ�.
            anim.SetBool("Idle", false);
            anim.SetBool("Walk", true);
            anim.SetBool("Run", false);
            
            // AKM�� Ȱ��ȭ�Ǿ� ���� ��
            if (akmObject.activeSelf)
            {
                Debug.Log("akȰ��ȭ");
                // RifleWalk �ִϸ��̼��� Ȱ��ȭ�մϴ�.
                anim.SetBool("RifleWalk", true);
                Debug.Log("�ѵ�� ������");
            }
            else
            {
                // AKM�� ��Ȱ��ȭ�Ǿ� ���� �� RifleWalk �ִϸ��̼��� ��Ȱ��ȭ�ϰ� Walk �ִϸ��̼��� Ȱ��ȭ�մϴ�.
                //anim.SetBool("RifleWalk", false);
                anim.SetBool("RifleIdle", false);
                anim.SetBool("RifleRun", false);
                anim.SetBool("Walk", true);
            }
        }
        else
        {
            // ���� ������ ���� �ִϸ��̼��� �����մϴ�.
            anim.SetBool("Idle", true);
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);

            // AKM�� ��Ȱ��ȭ�Ǿ� ���� �� RifleWalk �ִϸ��̼��� ��Ȱ��ȭ�մϴ�.
            if (!akmObject.activeSelf)
            {
                Debug.Log("ak��Ȱ��ȭ");
                //anim.SetBool("RifleWalk", false);
                anim.SetBool("RIfleIdle", false);
                anim.SetBool("RIfleRun", false);
                anim.SetBool("Idle", true);
            }
            else
            {
                anim.SetBool("RifleWalk", false);
            }
        }
        if (akmObject.activeSelf)
        {
            anim.SetBool("RifleIdle", true);
        }
        else
        {
            anim.SetBool("RifleIdle", false);
        }

        // �ڷ� �̵��� ���� �̵� �ӵ��� ���Դϴ�.
        if (v < 0) v *= 0.6F;
        // ���콺 �Է� ���� �����ɴϴ�.
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        // ��Ʈ Ʈ�� �ִϸ��̼� �������� �ڿ��������� ���� �����Է°��� �����մϴ�.
        float smoothedValueX = Mathf.Lerp(anim.GetFloat("MoveH"), h, 6f * Time.deltaTime);
        float smoothedValueY = Mathf.Lerp(anim.GetFloat("MoveV"), v, 6f * Time.deltaTime);
        anim.SetFloat("MoveV", smoothedValueY);
        anim.SetFloat("MoveH", smoothedValueX);


        //}
        //if (gunToggleMode)
        //{
        //    if (Input.GetMouseButtonDown(1)) zoom = !zoom;
        //    guncamera.depth = zoom ? 3 : -1;
        //}
        //else
        //{
        //    if (Input.GetMouseButton(1))
        //    {
        //        guncamera.depth = 4;

        //    }
        //    else guncamera.depth = -1;
        //}
    }

    // �÷��̾��� �̵� �� ȸ���� ó���� �޼���
    private void MoveAndRotate()
    {
        // �÷��̾��� ȸ���� ó���մϴ�.
        transform.Rotate(Vector3.up * mouseX);
        centerTr.Rotate(Vector3.right * -mouseY);
        // �̵� ���͸� ����Ͽ� Rigidbody�� �̿��� �̵��մϴ�.
        Vector3 move = Time.deltaTime * walkSpeed * ((transform.forward * v) + (transform.right * h));
        rb.MovePosition(rb.position + move);
        #region comment
        #endregion
    }
    // �÷��̾��� �޸��� ������ ó���� �޼���
    private void Run()
    {
        isAKMActive = akmObject.activeSelf;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(h, 0f, v).normalized;

            if (direction.magnitude > 0.1f)
            {
                // �޸��� ���� ���� �ִϸ��̼��� �����մϴ�.
                anim.SetBool("Walk", false);
                anim.SetBool("Run", true);
                //anim.SetBool("Attack", true);
                //anim.SetBool("Fire", false);
                Debug.Log("�ٱ�");
                // �޸��� ���� �̵� ���͸� ����Ͽ� Rigidbody�� �̿��� �̵��մϴ�.
                Vector3 run = ((transform.forward * v) + (transform.right * h)) * runSpeed * Time.deltaTime;
                rb.MovePosition(rb.position + run);
                if (akmObject.activeSelf)
                {
                    // AKM�� Ȱ��ȭ�Ǿ� ������ RifleWalk �ִϸ��̼� ����

                    anim.SetBool("RifleRun", true);
                }
                else
                {
                    // AKM�� ��Ȱ��ȭ�Ǿ� ������ RifleWalk �ִϸ��̼� ����
                    // anim.SetBool("RifleWalk", false);

                }
            }
            // AKM ������Ʈ�� Ȱ��ȭ ���ο� ���� �ִϸ��̼� ����

        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            // �޸��⸦ ���� ���� �ִϸ��̼��� �����մϴ�.
            anim.SetBool("Run", false);
            anim.SetBool("RifleRun", false);
            
            if (akmObject.activeSelf)
            {
                // AKM�� Ȱ��ȭ�Ǿ� ������ RifleWalk �ִϸ��̼� ����

                anim.SetBool("RifleIdle", true);
            }
            else
            {
                // AKM�� ��Ȱ��ȭ�Ǿ� ������ RifleWalk �ִϸ��̼� ����
                 anim.SetBool("Idle", true);

            }


        }
        if (v < 0) v *= 0.6F;
    }
    // �÷��̾��� ���� ������ ó���� �޼���
    void Jump()
    {
        spbar = Input.GetKeyDown(KeyCode.Space);
        if (spbar) Debug.Log("����Ű ����");
     
        if (spbar && isGround)
        {
            Debug.Log("JUMP");
            // ���� �ִϸ��̼��� ����մϴ�.
            anim.SetTrigger("Jump");
            // ���� ������ false�� �����մϴ�.
            isGround = false;
            // �÷��̾ �������� �̵����� ���� ȿ���� �ݴϴ�.
            var force = Vector3.up * 5;
            rb.velocity = force;
            rb.velocity = Vector3.up * rb.velocity.y; ;

        }
    }
    // �÷��̾ �������� ������ ���� Ȱ��ȭ �Ǵ� �޼���
    public void playerHitDamage(float takeDamage)
    {
        // �÷��̾��� ü���� ���ҽ�ŵ�ϴ�.
        presentHealth -= takeDamage;
        // �������� �޾����� UI�� ǥ���մϴ�.
        StartCoroutine(PlayerDamage());
        // ü�� �����̴��� ������Ʈ�մϴ�.
        hpSlider.SetHealth(presentHealth);

        // ü���� 0 �����̸� �÷��̾ ����մϴ�.
        if (presentHealth <= 0)
        {
            PlayerDie();
        }
    }
    // �÷��̾��� ����� ó���� �޼���
    private void PlayerDie()
    {
        // ������� �޴��� Ȱ��ȭ�մϴ�.
        EndgameMenu.SetActive(true);
        // ���콺 Ŀ���� �����մϴ�.
        Cursor.lockState = CursorLockMode.None;
        // �÷��̾� ���� ������Ʈ�� ���� �ð� �Ŀ� �ı��մϴ�.
        Destroy(gameObject, 1.0f);
    }

    // �÷��̾ �������� ���������� UI�� Ȱ��ȭ ��ų IEnumerator
    IEnumerator PlayerDamage()
    {
        // �÷��̾� ������ UI�� Ȱ��ȭ�մϴ�.
        playerDamage.SetActive(true);
        // ���� �ð� �Ŀ� UI�� ��Ȱ��ȭ�մϴ�.
        yield return new WaitForSeconds(0.2f);
        playerDamage.SetActive(false);
    }


}