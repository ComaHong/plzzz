using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    [Header("Player Punch Var")] // 플레이어 펀치 헤더바
    public Camera cam; // 사용할 캠
    public float giveDamegeOf = 10f;// 펀치로 가할 데미지
    public float punchingRnage = 2f; // 펀치의 사정거리
    
    [Header("Punch Effects")] // 플레이어 펀치 이펙트 헤더
    public GameObject WoodedEffect; // 펀치 이펙트

    // 레이캐스트를 사용하는 펀치 메서드
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
                Debug.Log("우드이펙트 생성");
                Destroy(Woodgo, 1f);
                Debug.Log("우드이펙트 파괴");
            }
        }

    }
}
