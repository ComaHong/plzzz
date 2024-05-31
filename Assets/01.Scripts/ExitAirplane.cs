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



    public bool isPlayerInside = false; // �÷��̾ ����� �ȿ� �ִ��� ����
    private Vector3 playerStartPositionRelativeToAirplane; // �÷��̾��� ����� ������� ���� ��ġ


    private void OnEnable()
    {
        StartCoroutine(StartFlying());
        playerbody.SetActive(false);
        maincam.SetActive(false);
        airplanecam.SetActive(true);

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
        player.transform.SetParent(airplane.transform);

    }


    private void Update()
    {

        // �÷��̾ ����� �ȿ� ������ Q Ű�� ������ ����⿡�� ���� �� ����
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("test");
            playerbody.SetActive(true);
            OutAirplane();

        }
        // ���콺 �Է� ���� �����ɴϴ�.
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

    }

    private void OutAirplane()
    {
        // �÷��̾ ����� ������ �̵���Ű�� �߷¿� ���� ����߸�

        Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();
        maincam.SetActive(true); // �� ī�޶� Ȱ��ȭ
        Debug.Log("ī�޶� Ȱ��ȭ");
        airplanecam.SetActive(false); // ����� ī�޶� ��Ȱ��ȭ
        player.transform.SetParent(null); // �θ� ����
        playerRigidbody.isKinematic = false; // �߷� ����
        playerRigidbody.AddForce(Vector3.down * exitForce, ForceMode.Impulse); // ����߸���
                                                                               // PlayerController ��ũ��Ʈ Ȱ��ȭ
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = true;
        }

    }
}
