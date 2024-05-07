using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHead : MonoBehaviour
{
    private Animator playerAnimator; // �÷��̾��� �ִϸ����� ������Ʈ�� ������ ����
    public Transform target; // �Ӹ��� ���󰡰� �� ����� ��ġ�� ��Ÿ���� Transform ����
    public float weight = 1f; // �Ӹ��� ���󰡴� ������ �����ϴ� ����ġ ��

    // Start is called before the first frame update
    void Start()
    {
        // ������ �� �÷��̾��� �ִϸ����� ������Ʈ�� �����ͼ� ����
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnAnimatorIK(int layerIndex)
    {
        // �ִϸ������� �Ӹ��� target�� ��ġ�� �ٶ󺸵��� ����
        playerAnimator.SetLookAtPosition(target.position);
        // �Ӹ��� ���󰡴� ������ ���� (����ġ ����)
        playerAnimator.SetLookAtWeight(weight);
    }


}
