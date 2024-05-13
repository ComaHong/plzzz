using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHead : MonoBehaviour
{
    private Animator playerAnimator; // �÷��̾��� �ִϸ����� ������Ʈ�� ������ ����
    public Transform target; // �Ӹ��� ���󰡰� �� ����� ��ġ�� ��Ÿ���� Transform ����
    public float weight = 1f; // �Ӹ��� ���󰡴� ������ �����ϴ� ����ġ ��

    public Transform ParachuteRigPos;
    public Transform righthandle; // ������Ʈ�� ������(Transform)
    public Transform leftHandle; // �޼��� ������(Transform)

    [Range(0,1)]
    public float a;
    // Start is called before the first frame update
    void Start()
    {
        // ������ �� �÷��̾��� �ִϸ����� ������Ʈ�� �����ͼ� ����
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnAnimatorIK(int layerIndex)
    {

        ParachuteRigPos.position = playerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, a);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, a);

        // �ִϸ������� �Ӹ��� target�� ��ġ�� �ٶ󺸵��� ����
        //playerAnimator.SetLookAtPosition(target.position);
        //// �Ӹ��� ���󰡴� ������ ���� (����ġ ����)
        //playerAnimator.SetLookAtWeight(weight);

        //// Animator ������Ʈ�� �����ϰ� �޼հ� �������� ������(Transform)�� ������ ��
        //if (playerAnimator != null && leftHandle != null && righthandle != null)
        //{
        //    // IK�� ����Ͽ� �÷��̾��� �޼� ��ġ�� �����մϴ�.
        //    playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandle.position);
        //    // IK�� ����Ͽ� �÷��̾��� �޼� ȸ���� �����մϴ�.
        //    playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandle.rotation);

        //    // IK�� ����Ͽ� �÷��̾��� ������ ��ġ�� �����մϴ�.
        //    playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, righthandle.position);
        //    // IK�� ����Ͽ� �÷��̾��� ������ ȸ���� �����մϴ�.
        //    playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, righthandle.rotation);
        //}
    }


}
