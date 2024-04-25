using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : MonoBehaviour
{
    public GameObject flight; // 비행기 오브젝트
    public float speed = 100f; // 비행기의 이동 속도
    public float distance = 4100f; // 이동할 거리

    private float initialPositionX; // 비행기의 초기 Z 위치
    private bool isMoving = true; // 이동 중인지 여부

    // Start is called before the first frame update
    void Start()
    {
        initialPositionX = transform.position.x; // 초기 Z 위치 저장
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            // 비행기를 앞으로 이동
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            // 이동한 거리 계산
            float movedDistance = transform.position.x - initialPositionX;

            // 이동할 거리에 도달하면 비행기를 사라지게 하고 이동 종료
            if (movedDistance >= distance)
            {
                isMoving = false;
                gameObject.SetActive(false);
            }
        }
    }
}
