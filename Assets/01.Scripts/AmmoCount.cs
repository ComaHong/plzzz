using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCount : MonoBehaviour
{
    // 총알텍스트 오브젝트
    public Text ammoText;
    // 탄창텍스트 오브젝트
    public Text magText;
    // 이 스크립트의 인스턴스를 전역적으로 접근할 수 있도록 정적 변수로 설정
    public static AmmoCount Instance;

    private void Awake()
    {
        // 인스턴스가 비어있다면 이것을 인스턴스화함
        if (Instance == null)
        {
            Instance = this;
        }

    }
     // 총알 텍스트 업데이트 메서드
    public void UpdateAmmoText(int presentAmmo)
    {
        ammoText.text = "Ammo. " + presentAmmo;
    }
    // 탄창 텍스트 업데이트 메서드
    public void UpdateMagText(int mag)
    {
        magText.text = "Magazines. " + mag;
    }
}
