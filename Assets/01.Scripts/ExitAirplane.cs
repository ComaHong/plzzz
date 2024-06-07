using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExitAirplane : MonoBehaviour
{
    public GameObject player; // 플레이어 오브젝트
    public GameObject airplane; // 비행기 오브젝트
    public GameObject playerbody; // 플레이어 바디오브젝트
    public float exitForce = 10f; // 비행기에서 나올 때 적용할 힘
    public float startDelay = 5f; // 게임 시작 후 비행기가 출발하기까지의 지연 시간
    public GameObject airplanecam; // 비행기에서 사용할 캠
    public GameObject maincam; // 비행기에서 나오면 사용할 캠
    private float mouseX; // 카메라가 입력받을 마우스 X축값
    private float mouseY; // 카메라가 입력받을 마우스 Y축값
    public GameObject minimapiconmesh;


    public bool isPlayerInside = false; // 플레이어가 비행기 안에 있는지 여부
    private Vector3 playerStartPositionRelativeToAirplane; // 플레이어의 비행기 상대적인 시작 위치


    private void OnEnable()
    {
        StartCoroutine(StartFlying());
        playerbody.SetActive(false);
        maincam.SetActive(false);
        airplanecam.SetActive(true);
        if (minimapiconmesh != null)
        {
            // 시작할 때 Mesh Renderer를 비활성화합니다.
            minimapiconmesh.GetComponent<MeshRenderer>().enabled = false;
        }
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }

    }

    private IEnumerator StartFlying()
    {
        yield return new WaitForSeconds(startDelay);

        // 비행기가 출발하는 로직을 여기에 추가
        Debug.Log("Airplane is now flying!");

        yield return null;
    }
    private void Start()
    {
        maincam.SetActive(false);
        airplanecam.SetActive(true);
       

    }


    private void Update()
    {

        // 플레이어가 비행기 안에 있으면 Q 키를 눌러서 비행기에서 나갈 수 있음
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("test");
            playerbody.SetActive(true);
            //OutAirplane();
            MeshRenderer meshRenderer = minimapiconmesh.GetComponent<MeshRenderer>();
            meshRenderer.enabled = !meshRenderer.enabled;
            maincam.SetActive(true); // 주 카메라 활성화
            airplanecam.SetActive(false); // 비행기 카메라 비활성화.
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.enabled = true;
            }

        }
        // 마우스 입력 값을 가져옵니다.
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

    }

    
}
