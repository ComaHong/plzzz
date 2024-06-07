using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExitAirplane : MonoBehaviour
{
    public GameObject player; // �÷��̾� ������Ʈ
    public GameObject airplane; // ����� ������Ʈ
    public GameObject playerbody; // �÷��̾� �ٵ������Ʈ
    public float exitForce = 10f; // ����⿡�� ���� �� ������ ��
    public float startDelay = 5f; // ���� ���� �� ����Ⱑ ����ϱ������ ���� �ð�
    public GameObject airplanecam; // ����⿡�� ����� ķ
    public GameObject maincam; // ����⿡�� ������ ����� ķ
    private float mouseX; // ī�޶� �Է¹��� ���콺 X�ప
    private float mouseY; // ī�޶� �Է¹��� ���콺 Y�ప
    public GameObject minimapiconmesh;


    public bool isPlayerInside = false; // �÷��̾ ����� �ȿ� �ִ��� ����
    private Vector3 playerStartPositionRelativeToAirplane; // �÷��̾��� ����� ������� ���� ��ġ


    private void OnEnable()
    {
        StartCoroutine(StartFlying());
        playerbody.SetActive(false);
        maincam.SetActive(false);
        airplanecam.SetActive(true);
        if (minimapiconmesh != null)
        {
            // ������ �� Mesh Renderer�� ��Ȱ��ȭ�մϴ�.
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

        // ����Ⱑ ����ϴ� ������ ���⿡ �߰�
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

        // �÷��̾ ����� �ȿ� ������ Q Ű�� ������ ����⿡�� ���� �� ����
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("test");
            playerbody.SetActive(true);
            //OutAirplane();
            MeshRenderer meshRenderer = minimapiconmesh.GetComponent<MeshRenderer>();
            meshRenderer.enabled = !meshRenderer.enabled;
            maincam.SetActive(true); // �� ī�޶� Ȱ��ȭ
            airplanecam.SetActive(false); // ����� ī�޶� ��Ȱ��ȭ.
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.enabled = true;
            }

        }
        // ���콺 �Է� ���� �����ɴϴ�.
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

    }

    
}
