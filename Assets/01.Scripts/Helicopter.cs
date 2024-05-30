using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    public GameObject helicopter; // �︮���� ������Ʈ
    public PlayerController playerController; // �÷��̾� ��Ʈ�ѷ� ��ũ��Ʈ
    public Animation helicopterAnimation; // �︮���� �ִϸ�����
    public float forwardSpeed = 100f; // �︮������ Z�� �̵� �ӵ�
    public float descendSpeed = 10f; // �︮������ Y�� �ϰ� �ӵ�
    public float animationStopSpeed = 0.5f; // �ִϸ����� �ӵ��� ���̴� �ӵ�

    private Vector3 initialPosition = new Vector3(386.799988f, 161.699997f, -114f); // �︮������ �ʱ� ��ġ
    private Vector3 forwardTargetPosition = new Vector3(336.529999f, 161.699997f, 621f); // Z�� ��ǥ ��ġ
    private Vector3 finalTargetPosition = new Vector3(336.529999f, 20.1599998f, 621f); // ���� ��ǥ ��ġ

    private bool isMovingForward = false; // Z�� ���� �̵� ������ ����
    private bool isDescending = false; // Y�� ���� �ϰ� ������ ����
    private bool isSlowingDown = false; // �ִϸ����� �ӵ��� ���̴� ������ ����
    private float radius = 5f; // �÷��̾� �ݰ� ��

    void Start()
    {
        transform.position = initialPosition; // �ʱ� ��ġ�� ����
        helicopter.SetActive(false); // �︮���� ��Ȱ��ȭ ���·� ����
       
    }

    
   

    // �︮���͸� Ȱ��ȭ�ϰ� �̵��� �����ϴ� �޼���
    public void ActivateHelicopter()
    {
        helicopter.SetActive(true); // �︮���� Ȱ��ȭ
        isMovingForward = true; // �︮���� Z�� �̵� ����
    }
    public void StartHelicopter()
    {
        if (isMovingForward)
        {
            // �︮���͸� Z�� �������� �̵�
            transform.position = Vector3.MoveTowards(transform.position, forwardTargetPosition, forwardSpeed * Time.deltaTime);

            // Z�� ��ǥ ��ġ�� �����ϸ� Z ���� �̵� �����ϰ� Y ���� �̵� ����
            if (transform.position == forwardTargetPosition)
            {
                isMovingForward = false;
                isDescending = true;
            }
        }

        if (isDescending)
        {
            // �︮���͸� Y�� �������� õõ�� �ϰ�
            transform.position = Vector3.MoveTowards(transform.position, finalTargetPosition, descendSpeed * Time.deltaTime);

            // ���� ��ǥ ��ġ�� �����ϸ� �̵� ����
            if (transform.position == finalTargetPosition)
            {
                isDescending = false;
                isSlowingDown = true;
            }
        }
        if (isSlowingDown)
        {
            // �ִϸ��̼��� �ӵ��� ���������� ���ҽ�Ŵ
            foreach (AnimationState state in helicopterAnimation)
            {
                state.speed = Mathf.Lerp(state.speed, 0, animationStopSpeed * Time.deltaTime);
            }

        }
    }
}