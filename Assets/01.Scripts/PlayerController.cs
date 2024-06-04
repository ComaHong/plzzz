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

    public GameObject minimapiconmesh; // 미니맵 아이콘 메쉬렌더러 컴포넌트
        public GameObject EndgameMenu; // 엔드게임 메뉴패널
    public InventoryObject inventory; // 
    public GameObject ItemUI; // 아이템상호작용 UI 텍스트
    public GameObject PlayerInventoryUi; // 플레이어의 인벤토리 UI패널
    public GameObject inventoryUi; // 인벤토리 패널
    private bool isinventoryUiActive; // 인벤토리 UI패널이 켜져잇는지 확인할 bool변수
    private bool isPlayerinventoryUiActive; // 플레이어 인벤토리 UI패널이 켜져잇는지 확인할 bool변수

    public Transform centerTr; // 플레이어의 center Transform
    public Transform player; // 플레이어의 Transform
    public Animator anim; // 플레이어의 애니메이터
    public GameObject cam; // 3인칭시점카메라
    public GameObject akmObject; // 총 오브젝트

    public GameObject playerbody; //플레이어 바디

    [Header("낙하산 부품들")]
    public float descentSpeed = 0.5f; // 낙하 속도
    public float rotationSpeed = 20.0f; // 회전 속도 (degrees per second)
    public float destroyDelay = 5.0f; // 낙하산이 삭제되기 전 대기 시간
    public GameObject parachuteui; //낙하산오브젝트의Ui
    public GameObject parachuteisgroundui; // 낙하산에서 땅으로 뛰어내림을 표시할 UI
    public GameObject parachute; // 낙하산 오브젝트
    public Animator paraanim; // 낙하산오브젝트의 애니메이터
    private bool qKeyPressed = false; // 낙하와 낙하산을 핌을 관리할 bool변수
    private bool parachuteDeployed = false; // 낙하산이 펴졌는지를 체크할 bool변수
       private bool isFlying = false; // 플레이어가 날고있음을 체크할 bool변수
    private bool parachuteReady = false; // 낙하산이 준비되었는지 판별한 bool변수
    private bool lastQkey = false; // 마지막 Q눌림을 체크할 bool 변수
    private enum JumpState { Initial, Flying, ParachuteReady, ParachuteDeployed, Landing }
    private JumpState currentState = JumpState.Initial;

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
    private bool isGround = false; // 땅을 밟고 있는지 확인할 bool 변수
    public bool isAKMActive;
    private bool paraUIACtive = false;
    private bool paraUIunactive = false;
    
    private void Awake()
    {
        cam.SetActive(false);
        if(minimapiconmesh != null)
        {
            // 시작할 때 Mesh Renderer를 비활성화합니다.
            minimapiconmesh.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    // 시작하면 사용될 메서드
    void Start()
    {

        // 테스팅 끝나면 주석 해체해야함***playerbody.SetActive(false);    
       
        rb = GetComponent<Rigidbody>(); // 시작하면 플레이어의 리지드바디 컴포넌트를 찾아서 할당
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 락걸기
        presentHealth = playerHealth; // 플레이어의 현재체력을 초기에 설정된값으로 할당
        hpSlider.GiveFullHealth(playerHealth); // 플레이어의 Silder Ui의 MaxValue와 Value에 플레이어의 체력을 할당
        anim.SetBool("Walk", false);
        anim.SetBool("Run", false);

       



    }

    // 계속 업데이트 체킹할 메서드
    void Update()
    {

        //if (player.position.y <= 200f && !paraUIACtive)
        //{
        //    parachuteReady = true;
        //    parachuteui.gameObject.SetActive(true);
        //    paraUIACtive = true;

        //}
        //if (player.position.y <= 60f && !paraUIunactive)
        //{
        //    parachuteisgroundui.SetActive(true);
        //    paraUIunactive = true;
        //}



        // 플레이어의 인풋담당 메서드
        GetPlayerInputs();
        // 움직임과 화면전환메서드
        MoveAndRotate();
        // 뛰기메서드
        Run();
        // 점프 메서드
        Jump();
        // 인벤토리 UI껏다켰다 메서드
        ToggleInventory();
        // 발아래를 체크할 메서드
        //SurfaceCheck();
        // 인벤토리 저장 및 불러오는 메서드
        InventorySaveandLoad();
        // 낙하산 관련 메서드
        if (player.position.y <= 250.0f && !parachuteReady && !parachuteDeployed) 
        {
            parachuteui.SetActive(true);
            parachuteReady = true;
        }
        else if (player.position.y <= 60f && !lastQkey)
        {
            parachuteisgroundui.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Q) && !lastQkey)
        {
            switch (currentState)
            {
                case JumpState.Initial:
                    StartFlying();

                    break;
                case JumpState.Flying:
                    if (parachuteReady)
                    {

                        DeployParachute();

                    }
                    break;
                case JumpState.ParachuteDeployed:
                    if (player.position.y <= 60.0f)
                    {
                        PrepareLanding();
                        anim.SetBool("Landing", true);

                    }
                    break;
            }
        }
        // 앉기 메서드
        IsCrouch();


    }
    // 플레이어와 다른 콜라이더가 맞닿은 순간만 사용할 메서드
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            rb.useGravity = true;


            //anim.SetBool("Jumping", true);
            anim.SetBool("isGround", true);
        }

    }
    // 플레이어와 다른 콜라이더와 맞닿아있을때 사용할 메서드
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            rb.useGravity = true;
            //anim.SetBool("Jumping", true);
            anim.SetBool("isGround", true);
        }

    }
    // 플레이어와 콜라이더와 닿아있다가 떨어질때 사용할 메서드
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
            anim.SetBool("isGround", false);

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
                //anim.SetBool("RifleIdle", false);
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

                //anim.SetBool("RifleWalk", false);
                //anim.SetBool("RifleIdle", false);
                anim.SetBool("RifleRun", false);
                anim.SetBool("Idle", true);
            }
            else
            {
                anim.SetBool("RifleWalk", false);
            }
        }
        if (akmObject.activeSelf)
        {
            //anim.SetBool("RifleIdle", true);
        }
        else
        {
            //anim.SetBool("RifleIdle", false);
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
                anim.SetBool("RifleIdle", false);
                anim.SetBool("RifleWalk", false);
                anim.SetBool("RifleRun", false);

            }


        }
        // 플레이어가 뒤로갈떄는 이동속도 감소
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
            anim.SetBool("Jumping", true);
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
    // 플레이어와 오브젝트와 닿아있을 떄 사용할 메서드
    public void OnTriggerStay(Collider other)
    {
        // 닿아있는 오브젝트의 item스크립트를 가져와서 item변수에 담음
        var item = other.GetComponent<Item>();
        // 만약 아이템 스크립트가 존재한다면 itemUI 키기
        if (item)
        {
            ItemUI.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                isinventoryUiActive = !isinventoryUiActive; // 상태를 반전시킴 (켜져 있으면 끄고, 꺼져 있으면 켬)

                inventoryUi.SetActive(isinventoryUiActive); // 대상 오브젝트의 활성화 여부를 설정
            }


            // F키를 눌러 나의 인벤토리 창에 아이템을 추가하고 오브젝트 즉시 파괴, ItemUI 끄기
            if (Input.GetKeyDown(KeyCode.F))
            {
                inventory.AddItem(item.item, 1);
                Destroy(other.gameObject);
                ItemUI.gameObject.SetActive(false);

            }

        }
    }
    // 플레이어와 오브젝트가 떨어질 때 사용할 메서드
    public void OnTriggerExit(Collider other)
    {
        // 아이템UI오브젝트 끄기
        ItemUI.gameObject.SetActive(false);
    }
    void ToggleInventory()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isPlayerinventoryUiActive = !isPlayerinventoryUiActive; // 상태를 반전시킴 (켜져 있으면 끄고, 꺼져 있으면 켬)

            PlayerInventoryUi.SetActive(isPlayerinventoryUiActive); // 대상 오브젝트의 활성화 여부를 설정
        }


    }
    void InventorySaveandLoad()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
            Debug.Log("인벤토리 저장");
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            inventory.Load();
            Debug.Log("인벤토리 저장값 불러오기 성공");
        }
    }

   
    void IsCrouch()
    {
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


    IEnumerator FallAndDestroy()
    {
        float descentSpeed = 2.0f;
        float rotationSpeed = 45.0f;
        float destroyDelay = 3.0f;
        float elapsedTime = 0f;

        while (elapsedTime < destroyDelay)
        {
            elapsedTime += Time.deltaTime;
            parachute.transform.Translate(Vector3.down * descentSpeed * Time.deltaTime, Space.World);
            parachute.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        Destroy(parachute);
    }
    void StartFlying()
    {

        playerbody.SetActive(true);
        anim.SetBool("Flying", true);
        anim.SetBool("Flying2", true);
        MeshRenderer meshRenderer = minimapiconmesh.GetComponent<MeshRenderer>();
        meshRenderer.enabled = !meshRenderer.enabled;
        Debug.Log("Flying");
        isFlying = true;
        currentState = JumpState.Flying;


    }

    void DeployParachute()
    {
        parachuteui.SetActive(false);
        parachute.SetActive(true);
        paraanim.SetBool("Open", true);
        anim.SetBool("Open", true);
        parachuteDeployed = true;
        parachuteReady = false;
        Debug.Log("DeployParachu");

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(h, 0f, v).normalized;
        if (direction.magnitude > 0.1f)
        {
            anim.SetBool("Holding", true);

        }
        currentState = JumpState.ParachuteDeployed;
    }

    void PrepareLanding()
    {
        parachuteisgroundui.SetActive(false);
        Debug.Log("낙하산 그라운드 유아이 꺼짐");
        rb.useGravity = true;
        Debug.Log("플레이어 중력 켜짐");
        anim.SetBool("Falling", true);
        Debug.Log("Falling 애니메이션 출력");

        if (player.position.y <= 30f)
        {

            Debug.Log("플레이어 상공 10미터");
            anim.SetBool("Landing", true);
            parachuteisgroundui.SetActive(false);
            Debug.Log("Landing애니메이션 출력");
            StartCoroutine(FallAndDestroy());
            Debug.Log("낙하산 제거 코루틴 실행");
            lastQkey = true;
            anim.SetBool("Flying", false);

            currentState = JumpState.Landing;
        }



    }
}


