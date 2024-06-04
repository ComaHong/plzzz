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

    public GameObject minimapiconmesh; // �̴ϸ� ������ �޽������� ������Ʈ
        public GameObject EndgameMenu; // ������� �޴��г�
    public InventoryObject inventory; // 
    public GameObject ItemUI; // �����ۻ�ȣ�ۿ� UI �ؽ�Ʈ
    public GameObject PlayerInventoryUi; // �÷��̾��� �κ��丮 UI�г�
    public GameObject inventoryUi; // �κ��丮 �г�
    private bool isinventoryUiActive; // �κ��丮 UI�г��� �����մ��� Ȯ���� bool����
    private bool isPlayerinventoryUiActive; // �÷��̾� �κ��丮 UI�г��� �����մ��� Ȯ���� bool����

    public Transform centerTr; // �÷��̾��� center Transform
    public Transform player; // �÷��̾��� Transform
    public Animator anim; // �÷��̾��� �ִϸ�����
    public GameObject cam; // 3��Ī����ī�޶�
    public GameObject akmObject; // �� ������Ʈ

    public GameObject playerbody; //�÷��̾� �ٵ�

    [Header("���ϻ� ��ǰ��")]
    public float descentSpeed = 0.5f; // ���� �ӵ�
    public float rotationSpeed = 20.0f; // ȸ�� �ӵ� (degrees per second)
    public float destroyDelay = 5.0f; // ���ϻ��� �����Ǳ� �� ��� �ð�
    public GameObject parachuteui; //���ϻ������Ʈ��Ui
    public GameObject parachuteisgroundui; // ���ϻ꿡�� ������ �پ���� ǥ���� UI
    public GameObject parachute; // ���ϻ� ������Ʈ
    public Animator paraanim; // ���ϻ������Ʈ�� �ִϸ�����
    private bool qKeyPressed = false; // ���Ͽ� ���ϻ��� ���� ������ bool����
    private bool parachuteDeployed = false; // ���ϻ��� ���������� üũ�� bool����
       private bool isFlying = false; // �÷��̾ ���������� üũ�� bool����
    private bool parachuteReady = false; // ���ϻ��� �غ�Ǿ����� �Ǻ��� bool����
    private bool lastQkey = false; // ������ Q������ üũ�� bool ����
    private enum JumpState { Initial, Flying, ParachuteReady, ParachuteDeployed, Landing }
    private JumpState currentState = JumpState.Initial;

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
    private bool isGround = false; // ���� ��� �ִ��� Ȯ���� bool ����
    public bool isAKMActive;
    private bool paraUIACtive = false;
    private bool paraUIunactive = false;
    
    private void Awake()
    {
        cam.SetActive(false);
        if(minimapiconmesh != null)
        {
            // ������ �� Mesh Renderer�� ��Ȱ��ȭ�մϴ�.
            minimapiconmesh.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    // �����ϸ� ���� �޼���
    void Start()
    {

        // �׽��� ������ �ּ� ��ü�ؾ���***playerbody.SetActive(false);    
       
        rb = GetComponent<Rigidbody>(); // �����ϸ� �÷��̾��� ������ٵ� ������Ʈ�� ã�Ƽ� �Ҵ�
        Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ�� ���ɱ�
        presentHealth = playerHealth; // �÷��̾��� ����ü���� �ʱ⿡ �����Ȱ����� �Ҵ�
        hpSlider.GiveFullHealth(playerHealth); // �÷��̾��� Silder Ui�� MaxValue�� Value�� �÷��̾��� ü���� �Ҵ�
        anim.SetBool("Walk", false);
        anim.SetBool("Run", false);

       



    }

    // ��� ������Ʈ üŷ�� �޼���
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



        // �÷��̾��� ��ǲ��� �޼���
        GetPlayerInputs();
        // �����Ӱ� ȭ����ȯ�޼���
        MoveAndRotate();
        // �ٱ�޼���
        Run();
        // ���� �޼���
        Jump();
        // �κ��丮 UI�����״� �޼���
        ToggleInventory();
        // �߾Ʒ��� üũ�� �޼���
        //SurfaceCheck();
        // �κ��丮 ���� �� �ҷ����� �޼���
        InventorySaveandLoad();
        // ���ϻ� ���� �޼���
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
        // �ɱ� �޼���
        IsCrouch();


    }
    // �÷��̾�� �ٸ� �ݶ��̴��� �´��� ������ ����� �޼���
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
    // �÷��̾�� �ٸ� �ݶ��̴��� �´�������� ����� �޼���
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
    // �÷��̾�� �ݶ��̴��� ����ִٰ� �������� ����� �޼���
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
            anim.SetBool("isGround", false);

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
                //anim.SetBool("RifleIdle", false);
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
                anim.SetBool("RifleIdle", false);
                anim.SetBool("RifleWalk", false);
                anim.SetBool("RifleRun", false);

            }


        }
        // �÷��̾ �ڷΰ����� �̵��ӵ� ����
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
            anim.SetBool("Jumping", true);
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
    // �÷��̾�� ������Ʈ�� ������� �� ����� �޼���
    public void OnTriggerStay(Collider other)
    {
        // ����ִ� ������Ʈ�� item��ũ��Ʈ�� �����ͼ� item������ ����
        var item = other.GetComponent<Item>();
        // ���� ������ ��ũ��Ʈ�� �����Ѵٸ� itemUI Ű��
        if (item)
        {
            ItemUI.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                isinventoryUiActive = !isinventoryUiActive; // ���¸� ������Ŵ (���� ������ ����, ���� ������ ��)

                inventoryUi.SetActive(isinventoryUiActive); // ��� ������Ʈ�� Ȱ��ȭ ���θ� ����
            }


            // FŰ�� ���� ���� �κ��丮 â�� �������� �߰��ϰ� ������Ʈ ��� �ı�, ItemUI ����
            if (Input.GetKeyDown(KeyCode.F))
            {
                inventory.AddItem(item.item, 1);
                Destroy(other.gameObject);
                ItemUI.gameObject.SetActive(false);

            }

        }
    }
    // �÷��̾�� ������Ʈ�� ������ �� ����� �޼���
    public void OnTriggerExit(Collider other)
    {
        // ������UI������Ʈ ����
        ItemUI.gameObject.SetActive(false);
    }
    void ToggleInventory()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isPlayerinventoryUiActive = !isPlayerinventoryUiActive; // ���¸� ������Ŵ (���� ������ ����, ���� ������ ��)

            PlayerInventoryUi.SetActive(isPlayerinventoryUiActive); // ��� ������Ʈ�� Ȱ��ȭ ���θ� ����
        }


    }
    void InventorySaveandLoad()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
            Debug.Log("�κ��丮 ����");
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            inventory.Load();
            Debug.Log("�κ��丮 ���尪 �ҷ����� ����");
        }
    }

   
    void IsCrouch()
    {
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
        Debug.Log("���ϻ� �׶��� ������ ����");
        rb.useGravity = true;
        Debug.Log("�÷��̾� �߷� ����");
        anim.SetBool("Falling", true);
        Debug.Log("Falling �ִϸ��̼� ���");

        if (player.position.y <= 30f)
        {

            Debug.Log("�÷��̾� ��� 10����");
            anim.SetBool("Landing", true);
            parachuteisgroundui.SetActive(false);
            Debug.Log("Landing�ִϸ��̼� ���");
            StartCoroutine(FallAndDestroy());
            Debug.Log("���ϻ� ���� �ڷ�ƾ ����");
            lastQkey = true;
            anim.SetBool("Flying", false);

            currentState = JumpState.Landing;
        }



    }
}


