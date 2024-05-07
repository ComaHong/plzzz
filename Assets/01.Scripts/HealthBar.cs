using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // 플레이어의 체력을 나타내는 슬라이더
    public Slider hpSlider;


    // 플레이어에게 최대 체력을 설정하는 메서드
    public void GiveFullHealth(float health)
    {
        // 슬라이더의 최대값을 받은 체력 값으로 설정합니다.
        hpSlider.maxValue = health;
        // 슬라이더의 현재값도 받은 체력 값으로 설정하여 플레이어의 최대 체력을 표시합니다.
        hpSlider.value = health;
    }
    // 플레이어의 현재 체력을 설정하는 메서드
    public void SetHealth(float health)
    {
        // 슬라이더의 값을 받은 체력 값으로 설정하여 플레이어의 현재 체력을 표시합니다.
        hpSlider.value = health;
    }
}
