using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{/// <summary>
/// public �ʵ�
/// </summary>
    [Range(1f, 10f)]
    [SerializeField] public float walkSpeed = 1.9f; //�̵��ӵ�
    [Range(2f, 15f)]
    [SerializeField] public float runSpeed = 3f;
    [Header("�÷��̾� ü�� ����")]
    private float playerHealth = 100f;
    public float presentHealth;
    public GameObject playerDamage;
    public HealthBar hpSlider; //hp�����̴� �������ִ� ��ũ��Ʈ
    public GameObject EndgameMenu; // ������� �޴��г�


    public Transform centerTr;
    public Transform player; // �÷��̾��� Transform
    public Animator anim; // �÷��̾��� �ִϸ�����
    public GameObject cam; // ī�޶�
    public Text cartext; // ���� ��ȣ�ۿ��� �޽���

    /// <summary>
    /// private�ʵ�
    /// </summary>
    private float h; // �÷��̾��� ���� �Է��� ���� horizontal����
    private float v; // �÷��̾��� �¿� �Է��� ���� vertical����
    private float mouseX;
    private float mouseY;
    private bool spbar; // �÷��̾ ���� �ߴ��� Ȯ���� bool ����
    private Rigidbody rb; // �÷��̾��� ������ٵ�
    public bool Crouch = false;
    public bool isGround = false; // ���� ��� �ִ��� Ȯ���� bool ����


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ�� ���ɱ�
        presentHealth = playerHealth;
        hpSlider.GiveFullHealth(playerHealth);


    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInputs();
        MoveAndRotate();
        Run();
        Jump();

        // Ű���� �Է��� ���� ���� �����մϴ�.
        if (Input.GetKeyDown(KeyCode.C) && !Crouch)
        {
            anim.SetBool("IsCrouch", true);
            Crouch = true;
            walkSpeed = 2f;

        }
        else if (Input.GetKeyDown(KeyCode.C) && Crouch)
        {
            anim.SetBool("IsCrouch", false);
            Crouch = false;
            anim.SetBool("Idle", true);
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
    static List<Vector3> dir = new();
    private bool isShot;


    static PlayerController()
    {
        for (int i = 0; i < 8; i++)
        {
            dir.Add(Quaternion.Euler(0, 0, i / 8f * -360) * Vector3.up);
        }
    }

    private void GetPlayerInputs()
    {

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(h, 0f, v).normalized;
        if (direction.magnitude > 0.1f)
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Walk", true);
            //anim.SetBool("Run", false);
            anim.SetBool("RifleWalk", false);
            anim.SetBool("IdleAim", false);

        }
        else
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Walk", false);
            //anim.SetBool("Run", false);
        }
        if (v < 0) v *= 0.6F;
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");


        // ��Ʈ Ʈ�� �ִϸ��̼� �������� �ڿ��������� ����
        float smoothedValueX = Mathf.Lerp(anim.GetFloat("MoveH"), h, 6f * Time.deltaTime);
        float smoothedValueY = Mathf.Lerp(anim.GetFloat("MoveV"), v, 6f * Time.deltaTime);
        anim.SetFloat("MoveV", smoothedValueY);
        anim.SetFloat("MoveH", smoothedValueX);



        if (Input.GetMouseButtonDown(0))
        {
            isShot = true;
            anim.SetBool("Fire", true);


        }
        else if (Input.GetMouseButtonUp(0))
        {

            isShot = false;
            anim.SetBool("Fire", false);

        }
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
    /// <summary> -Y ������ �ӷ��� ����ϰ� isGrounded �ʱ�ȭ </summary>

    private void MoveAndRotate()
    {

        // Rotate
        transform.Rotate(Vector3.up * mouseX);
        centerTr.Rotate(Vector3.right * -mouseY);
        Vector3 move = Time.deltaTime * walkSpeed * ((transform.forward * v) + (transform.right * h));
        rb.MovePosition(rb.position + move);
        #region comment
        #endregion
    }
    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(h, 0f, v).normalized;

            if (direction.magnitude > 0.1f)
            {
                anim.SetBool("Walk", false);
                anim.SetBool("Run", true);
                anim.SetBool("Fire", false);
                Debug.Log("�ٱ�");
                Vector3 run = ((transform.forward * v) + (transform.right * h)) * runSpeed * Time.deltaTime;
                rb.MovePosition(rb.position + run);
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetBool("Run", false);

        }
    }
    void Jump()
    {
        spbar = Input.GetKeyDown(KeyCode.Space);
        if (spbar) Debug.Log("����Ű ����");
        Debug.Log(isGround);
        if (spbar && isGround)
        {
            Debug.Log("JUMP");
            isGround = false;
            var force = Vector3.up * 5;
            rb.velocity = force;
            rb.velocity = Vector3.up * rb.velocity.y; ;

        }
    }
    // �÷��̾ �������� ������ ���� Ȱ��ȭ �Ǵ� �޼���
    public void playerHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;

        StartCoroutine(PlayerDamage());

        hpSlider.SetHealth(presentHealth);

        if (presentHealth <= 0)
        {
            PlayerDie();
        }
    }
    // �÷��̾��� ����� ó���� �޼���
    private void PlayerDie()
    {
        EndgameMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Destroy(gameObject, 1.0f);
    }

    // �÷��̾ �������� ���������� UI�� Ȱ��ȭ ��ų IEnumerator
    IEnumerator PlayerDamage()
    {
        playerDamage.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        playerDamage.SetActive(false);
    }

}