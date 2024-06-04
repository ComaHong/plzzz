using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHead : MonoBehaviour
{
    private Animator playerAnimator; // �÷��̾��� �ִϸ����� ������Ʈ�� ������ ����
    public Transform target; // �Ӹ��� ���󰡰� �� ����� ��ġ�� ��Ÿ���� Transform ����
    public float weight = 1f; // �Ӹ��� ���󰡴� ������ �����ϴ� ����ġ ��

    // ���ϻ�
    //public Transform ParachuteLeftRigPos;
    //public Transform ParachuteRightRigPos;
    public Transform righthandle; // ������Ʈ�� ������(Transform)
    public Transform leftHandle; // �޼��� ������(Transform)

    [Range(0,1)]
    public float leftHandWeight;
    public float rightHandWeight;
    // Start is called before the first frame update
    void Start()
    {
        // ������ �� �÷��̾��� �ִϸ����� ������Ʈ�� �����ͼ� ����
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnAnimatorIK(int layerIndex)
    {

        //ParachuteLeftRigPos.position = playerAnimator.GetIKHintPosition(AvatarIKHint.LeftElbow);
        //ParachuteRightRigPos.position = playerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandWeight);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandWeight);
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandWeight);

        //�ִϸ������� �Ӹ��� target�� ��ġ�� �ٶ󺸵��� ����
        playerAnimator.SetLookAtPosition(target.position);
        // �Ӹ��� ���󰡴� ������ ���� (����ġ ����)
        playerAnimator.SetLookAtWeight(weight);

        // Animator ������Ʈ�� �����ϰ� �޼հ� �������� ������(Transform)�� ������ ��
        if (playerAnimator != null && leftHandle != null)
        {
            // IK�� ����Ͽ� �÷��̾��� �޼� ��ġ�� �����մϴ�.
            playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandle.position);
            // IK�� ����Ͽ� �÷��̾��� �޼� ȸ���� �����մϴ�.
            playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandle.rotation);

           
        }
        if (playerAnimator != null && righthandle != null)
        {
            // IK�� ����Ͽ� �÷��̾��� ������ ��ġ�� �����մϴ�.
            playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, righthandle.position);
            // IK�� ����Ͽ� �÷��̾��� ������ ȸ���� �����մϴ�.
            playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, righthandle.rotation);
        }
    }


}
