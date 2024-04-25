using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : MonoBehaviour
{
    public GameObject flight; // ����� ������Ʈ
    public float speed = 100f; // ������� �̵� �ӵ�
    public float distance = 4100f; // �̵��� �Ÿ�

    private float initialPositionX; // ������� �ʱ� Z ��ġ
    private bool isMoving = true; // �̵� ������ ����

    // Start is called before the first frame update
    void Start()
    {
        initialPositionX = transform.position.x; // �ʱ� Z ��ġ ����
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            // ����⸦ ������ �̵�
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            // �̵��� �Ÿ� ���
            float movedDistance = transform.position.x - initialPositionX;

            // �̵��� �Ÿ��� �����ϸ� ����⸦ ������� �ϰ� �̵� ����
            if (movedDistance >= distance)
            {
                isMoving = false;
                gameObject.SetActive(false);
            }
        }
    }
}
