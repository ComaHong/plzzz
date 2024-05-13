using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHead : MonoBehaviour
{
    private Animator playerAnimator; // 플레이어의 애니메이터 컴포넌트를 저장할 변수
    public Transform target; // 머리를 따라가게 할 대상의 위치를 나타내는 Transform 변수
    public float weight = 1f; // 머리를 따라가는 정도를 결정하는 가중치 값

    public Transform ParachuteRigPos;
    public Transform righthandle; // 오브젝트의 손잡이(Transform)
    public Transform leftHandle; // 왼손의 손잡이(Transform)

    [Range(0,1)]
    public float a;
    // Start is called before the first frame update
    void Start()
    {
        // 시작할 때 플레이어의 애니메이터 컴포넌트를 가져와서 저장
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnAnimatorIK(int layerIndex)
    {

        ParachuteRigPos.position = playerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, a);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, a);

        // 애니메이터의 머리를 target의 위치로 바라보도록 설정
        //playerAnimator.SetLookAtPosition(target.position);
        //// 머리를 따라가는 정도를 설정 (가중치 적용)
        //playerAnimator.SetLookAtWeight(weight);

        //// Animator 컴포넌트가 존재하고 왼손과 오른손의 손잡이(Transform)가 존재할 때
        //if (playerAnimator != null && leftHandle != null && righthandle != null)
        //{
        //    // IK를 사용하여 플레이어의 왼손 위치를 설정합니다.
        //    playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandle.position);
        //    // IK를 사용하여 플레이어의 왼손 회전을 설정합니다.
        //    playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandle.rotation);

        //    // IK를 사용하여 플레이어의 오른손 위치를 설정합니다.
        //    playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, righthandle.position);
        //    // IK를 사용하여 플레이어의 오른손 회전을 설정합니다.
        //    playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, righthandle.rotation);
        //}
    }


}
