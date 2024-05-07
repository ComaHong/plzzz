using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    [Header("Player Punch Var")] // �÷��̾� ��ġ �����
    public Camera cam; // ����� ķ
    public float giveDamegeOf = 10f;// ��ġ�� ���� ������
    public float punchingRnage = 2f; // ��ġ�� �����Ÿ�
    
    [Header("Punch Effects")] // �÷��̾� ��ġ ����Ʈ ���
    public GameObject WoodedEffect; // ��ġ ����Ʈ

    // ����ĳ��Ʈ�� ����ϴ� ��ġ �޼���
    public void Punch()
    {
        RaycastHit hitinfo;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitinfo, punchingRnage))
        {
            Debug.Log(hitinfo.transform.name);

            ObjectToHit objectToHit = hitinfo.transform.GetComponent<ObjectToHit>();

            if (objectToHit != null)
            {
                objectToHit.ObjectHitDamage(giveDamegeOf);
                GameObject Woodgo = Instantiate(WoodedEffect, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));
                Debug.Log("�������Ʈ ����");
                Destroy(Woodgo, 1f);
                Debug.Log("�������Ʈ �ı�");
            }
        }

    }
}
