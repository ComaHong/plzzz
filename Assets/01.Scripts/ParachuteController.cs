using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParachuteController : MonoBehaviour
{
    public float descentSpeed = 0.5f; // 낙하 속도
    public float rotationSpeed = 20.0f; // 회전 속도 (degrees per second)
    public float destroyDelay = 5.0f; // 낙하산이 삭제되기 전 대기 시간

    void Start()
    {
        // 낙하산 하강 및 회전 시작
        StartCoroutine(FallAndDestroy());
    }

    IEnumerator FallAndDestroy()
    {
        float elapsedTime = 0f;

        while (elapsedTime < destroyDelay)
        {
            // 시간 경과
            elapsedTime += Time.deltaTime;

            // 낙하산 하강
            transform.Translate(Vector3.left * descentSpeed * Time.deltaTime, Space.World);

            // 낙하산 회전
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

            yield return null;
        }

        // 낙하산 삭제
        Destroy(gameObject);
    }
}
