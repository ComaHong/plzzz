using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public GameObject gun;
    //public Transform gunPivot; // �� ��ġ�� ������
    //public Transform leftHandMount; // ���� ���� ������, �޼��� ��ġ�� ����
    //public Transform rightHandMount; // ���� ������ ������, �������� ��ġ�� ����

    private Animator playerAnimator; // �ִϸ����� ������Ʈ

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }


    void Update()
    {

    }
    // �ִϸ������� IK ����
    private void OnAnimatorIK(int layerIndex)
    {

        // IK�� ����Ͽ� �޼��� ��ġ�� ȸ���� ���� ���� �����̿� ����
        //playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        //playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        //playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandMount.position);
        //playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandMount.rotation);

        //// IK�� ����Ͽ� �������� ��ġ�� ȸ���� ���� ������ �����̿� ����
        //playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        //playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        //playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount.position);
        //playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount.rotation);
    }
}
