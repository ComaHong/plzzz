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
/// public 필드
/// </summary>
    [Range(1f, 10f)]
    [SerializeField] public float walkSpeed = 1.9f; // 플레이어의 걷는 속도
    [Range(2f, 15f)]
    [SerializeField] public float runSpeed = 3f; // 플레이어의 달리기 속도
    [Header("플레이어 체력 관련")]
    private float playerHealth = 100f; // 플레이어의 체력
    public float presentHealth; // 플레이어의 현재체력
    public GameObject playerDamage; // 플레이어가 피격당함을 표시해줄 UI
    public HealthBar hpSlider; //hp슬라이더 가지고있는 스크립트
    public GameObject EndgameMenu; // 엔드게임 메뉴패널


    public Transform centerTr; // 플레이어의 center Transform
    public Transform player; // 플레이어의 Transform
    public Animator anim; // 플레이어의 애니메이터
    public GameObject cam; // 3인칭시점카메라
    public GameObject akmObject; // 총 오브젝트
    /* public Text cartext;*/ // 차량 상호작용할 메시지

    /// <summary>
    /// private필드
    /// </summary>
    private float h; // 플레이어의 상하 입력을 받을 horizontal변수
    private float v; // 플레이어의 좌우 입력을 받을 vertical변수
    private float mouseX; // 카메라가 입력받을 마우스 X축값
    private float mouseY; // 카메라가 입력받을 마우스 Y축값
    private bool spbar; // 플레이어가 점프 했는지 확인할 bool타입 변수

    private Rigidbody rb; // 플레이어의 리지드바디 컴포넌트
    public bool Crouch = false; //v 플레이어가 앉아있는지 확인할 bool타입 변수
    public bool isGround = false; // 땅을 밟고 있는지 확인할 bool 변수
    public bool isAKMActive;

    // 시작하면 사용될 메서드
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // 시작하면 플레이어의 리지드바디 컴포넌트를 찾아서 할당
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 락걸기
        presentHealth = playerHealth; // 플레이어의 현재체력을 초기에 설정된값으로 할당
        hpSlider.GiveFullHealth(playerHealth); // 플레이어의 Silder Ui의 MaxValue와 Value에 플레이어의 체력을 할당


    }

    // 계속 업데이트 체킹할 메서드
    void Update()
    {
        GetPlayerInputs();
        MoveAndRotate();
        Run();
        Jump();


        // C키를 눌렀고 플레이어가 앉아있지 않다면 실행할 코드
        if (Input.GetKeyDown(KeyCode.C) && !Crouch)
        {
            // IsCrouch애니메이션 재생
            anim.SetBool("IsCrouch", true);
            // 앉음상태로 변경
            Crouch = true;
            // 플레이어의 이동속도도 느려짐
            walkSpeed = 2f;

        }
        // C키를 눌렀고 플레이어가 앉아있다면 실행할 코드
        else if (Input.GetKeyDown(KeyCode.C) && Crouch)
        {
            // IsCrouch애니메이션 스톱
            anim.SetBool("IsCrouch", false);
            // 플레이어가 일어난 상태로 변경
            Crouch = false;
            // Idle애니메이션 재생
            anim.SetBool("Idle", true);
            // 이동속도 원래 상태로 되돌림
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
    // 플레이어의 이동을 담당하는 메서드
    private void GetPlayerInputs()
    {
        // 플레이어의 수평 및 수직 입력을 가져옵니다.
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        // 새로운 Vector3 방향값 
        Vector3 direction = new Vector3(h, 0f, v).normalized;
        
        // 입력된 방향에 따라 애니메이션을 변경합니다.
        if (direction.magnitude > 0.1f)
        {
            // 이동 중일 때의 애니메이션을 설정합니다.
            anim.SetBool("Idle", false);
            anim.SetBool("Walk", true);
            anim.SetBool("Run", false);
            
            // AKM이 활성화되어 있을 때
            if (akmObject.activeSelf)
            {
                Debug.Log("ak활성화");
                // RifleWalk 애니메이션을 활성화합니다.
                anim.SetBool("RifleWalk", true);
                Debug.Log("총들고 움직임");
            }
            else
            {
                // AKM이 비활성화되어 있을 때 RifleWalk 애니메이션은 비활성화하고 Walk 애니메이션을 활성화합니다.
                //anim.SetBool("RifleWalk", false);
                anim.SetBool("RifleIdle", false);
                anim.SetBool("RifleRun", false);
                anim.SetBool("Walk", true);
            }
        }
        else
        {
            // 정지 상태일 때의 애니메이션을 설정합니다.
            anim.SetBool("Idle", true);
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);

            // AKM이 비활성화되어 있을 때 RifleWalk 애니메이션은 비활성화합니다.
            if (!akmObject.activeSelf)
            {
                Debug.Log("ak비활성화");
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

        // 뒤로 이동할 때의 이동 속도를 줄입니다.
        if (v < 0) v *= 0.6F;
        // 마우스 입력 값을 가져옵니다.
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        // 블렌트 트리 애니메이션 움직임의 자연스러움을 위해 현재입력값을 보간합니다.
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

    // 플레이어의 이동 및 회전을 처리할 메서드
    private void MoveAndRotate()
    {
        // 플레이어의 회전을 처리합니다.
        transform.Rotate(Vector3.up * mouseX);
        centerTr.Rotate(Vector3.right * -mouseY);
        // 이동 벡터를 계산하여 Rigidbody를 이용해 이동합니다.
        Vector3 move = Time.deltaTime * walkSpeed * ((transform.forward * v) + (transform.right * h));
        rb.MovePosition(rb.position + move);
        #region comment
        #endregion
    }
    // 플레이어의 달리기 동작을 처리할 메서드
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
                // 달리기 중일 때의 애니메이션을 설정합니다.
                anim.SetBool("Walk", false);
                anim.SetBool("Run", true);
                //anim.SetBool("Attack", true);
                //anim.SetBool("Fire", false);
                Debug.Log("뛰기");
                // 달리는 동안 이동 벡터를 계산하여 Rigidbody를 이용해 이동합니다.
                Vector3 run = ((transform.forward * v) + (transform.right * h)) * runSpeed * Time.deltaTime;
                rb.MovePosition(rb.position + run);
                if (akmObject.activeSelf)
                {
                    // AKM이 활성화되어 있으면 RifleWalk 애니메이션 실행

                    anim.SetBool("RifleRun", true);
                }
                else
                {
                    // AKM이 비활성화되어 있으면 RifleWalk 애니메이션 정지
                    // anim.SetBool("RifleWalk", false);

                }
            }
            // AKM 오브젝트의 활성화 여부에 따라 애니메이션 실행

        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            // 달리기를 멈출 때의 애니메이션을 설정합니다.
            anim.SetBool("Run", false);
            anim.SetBool("RifleRun", false);
            
            if (akmObject.activeSelf)
            {
                // AKM이 활성화되어 있으면 RifleWalk 애니메이션 실행

                anim.SetBool("RifleIdle", true);
            }
            else
            {
                // AKM이 비활성화되어 있으면 RifleWalk 애니메이션 정지
                 anim.SetBool("Idle", true);

            }


        }
        if (v < 0) v *= 0.6F;
    }
    // 플레이어의 점프 동작을 처리할 메서드
    void Jump()
    {
        spbar = Input.GetKeyDown(KeyCode.Space);
        if (spbar) Debug.Log("점프키 누름");
     
        if (spbar && isGround)
        {
            Debug.Log("JUMP");
            // 점프 애니메이션을 재생합니다.
            anim.SetTrigger("Jump");
            // 땅에 닿음을 false로 변경합니다.
            isGround = false;
            // 플레이어를 위쪽으로 이동시켜 점프 효과를 줍니다.
            var force = Vector3.up * 5;
            rb.velocity = force;
            rb.velocity = Vector3.up * rb.velocity.y; ;

        }
    }
    // 플레이어가 데미지를 입을때 마다 활성화 되는 메서드
    public void playerHitDamage(float takeDamage)
    {
        // 플레이어의 체력을 감소시킵니다.
        presentHealth -= takeDamage;
        // 데미지를 받았음을 UI에 표시합니다.
        StartCoroutine(PlayerDamage());
        // 체력 슬라이더를 업데이트합니다.
        hpSlider.SetHealth(presentHealth);

        // 체력이 0 이하이면 플레이어가 사망합니다.
        if (presentHealth <= 0)
        {
            PlayerDie();
        }
    }
    // 플레이어의 사망을 처리할 메서드
    private void PlayerDie()
    {
        // 엔드게임 메뉴를 활성화합니다.
        EndgameMenu.SetActive(true);
        // 마우스 커서를 해제합니다.
        Cursor.lockState = CursorLockMode.None;
        // 플레이어 게임 오브젝트를 일정 시간 후에 파괴합니다.
        Destroy(gameObject, 1.0f);
    }

    // 플레이어가 데미지를 입을때마다 UI를 활성화 시킬 IEnumerator
    IEnumerator PlayerDamage()
    {
        // 플레이어 데미지 UI를 활성화합니다.
        playerDamage.SetActive(true);
        // 일정 시간 후에 UI를 비활성화합니다.
        yield return new WaitForSeconds(0.2f);
        playerDamage.SetActive(false);
    }


}