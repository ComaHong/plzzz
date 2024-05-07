using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHead : MonoBehaviour
{
    private Animator playerAnimator; // 플레이어의 애니메이터 컴포넌트를 저장할 변수
    public Transform target; // 머리를 따라가게 할 대상의 위치를 나타내는 Transform 변수
    public float weight = 1f; // 머리를 따라가는 정도를 결정하는 가중치 값

    // Start is called before the first frame update
    void Start()
    {
        // 시작할 때 플레이어의 애니메이터 컴포넌트를 가져와서 저장
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnAnimatorIK(int layerIndex)
    {
        // 애니메이터의 머리를 target의 위치로 바라보도록 설정
        playerAnimator.SetLookAtPosition(target.position);
        // 머리를 따라가는 정도를 설정 (가중치 적용)
        playerAnimator.SetLookAtWeight(weight);
    }


}
