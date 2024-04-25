using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHead : MonoBehaviour
{
    private Animator playerAnimator;
    public Transform target;

    //public Transform leftHandMount;
    //public Transform rightHandMount;

    public float weight = 1f;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnAnimatorIK(int layerIndex)
    {
        playerAnimator.SetLookAtPosition(target.position);
        playerAnimator.SetLookAtWeight(weight);


        //// IK를 사용하여 왼손의 위치와 회전을 총의 왼쪽 손잡이에 맞춤
        //playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        //playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        //playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandMount.position);
        //playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandMount.rotation);

        //// IK를 사용하여 오른손의 위치와 회전을 총의 오른쪽 손잡이에 맞춤
        //playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        //playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        //playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount.position);
        //playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount.rotation);


        //var clip  = anim.GetCurrentAnimatorClipInfo(0)[0].clip;

        //anim.SetIKPosition(AvatarIKGoal.LeftHand , slaphand.position);
        //if (clip.name == "punch")
        //{
        //    var time = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        //    animSlap.Play("slap", 0, time);
        //    anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        //}
        //else
        //{
        //    anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
        //}

    }


}
