using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{/// <summary>
/// public 필드
/// </summary>
    [Range(1f, 10f)]
    [SerializeField] public float walkSpeed = 1.9f; //이동속도
    [Range(2f, 15f)]
    [SerializeField] public float runSpeed = 3f;
    [Header("플레이어 체력 관련")]
    private float playerHealth = 100f;
    public float presentHealth;
    public GameObject playerDamage;
    public HealthBar hpSlider; //hp슬라이더 가지고있는 스크립트
    public GameObject EndgameMenu; // 엔드게임 메뉴패널


    public Transform centerTr;
    public Transform player; // 플레이어의 Transform
    public Animator anim; // 플레이어의 애니메이터
    public GameObject cam; // 카메라
    public Text cartext; // 차량 상호작용할 메시지

    /// <summary>
    /// private필드
    /// </summary>
    private float h; // 플레이어의 상하 입력을 받을 horizontal변수
    private float v; // 플레이어의 좌우 입력을 받을 vertical변수
    private float mouseX;
    private float mouseY;
    private bool spbar; // 플레이어가 점프 했는지 확인할 bool 변수
    private Rigidbody rb; // 플레이어의 리지드바디
    public bool Crouch = false;
    public bool isGround = false; // 땅을 밟고 있는지 확인할 bool 변수


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 락걸기
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

        // 키보드 입력을 통해 값을 변경합니다.
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
    // 플레이어와 다른 콜라이더가 맞닿은 순간만 사용할 메서드
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }

    }
    // 플레이어와 다른 콜라이더와 맞닿아있을때 사용할 메서드
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }

    }
    // 플레이어와 콜라이더와 닿아있다가 떨어질때 사용할 메서드
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


        // 블렌트 트리 애니메이션 움직임의 자연스러움을 위해
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
    /// <summary> -Y 방향의 속력을 계산하고 isGrounded 초기화 </summary>

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
                Debug.Log("뛰기");
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
        if (spbar) Debug.Log("점프키 누름");
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
    // 플레이어가 데미지를 입을때 마다 활성화 되는 메서드
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
    // 플레이어의 사망을 처리할 메서드
    private void PlayerDie()
    {
        EndgameMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Destroy(gameObject, 1.0f);
    }

    // 플레이어가 데미지를 입을때마다 UI를 활성화 시킬 IEnumerator
    IEnumerator PlayerDamage()
    {
        playerDamage.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        playerDamage.SetActive(false);
    }

}