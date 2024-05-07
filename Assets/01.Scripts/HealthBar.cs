using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // �÷��̾��� ü���� ��Ÿ���� �����̴�
    public Slider hpSlider;


    // �÷��̾�� �ִ� ü���� �����ϴ� �޼���
    public void GiveFullHealth(float health)
    {
        // �����̴��� �ִ밪�� ���� ü�� ������ �����մϴ�.
        hpSlider.maxValue = health;
        // �����̴��� ���簪�� ���� ü�� ������ �����Ͽ� �÷��̾��� �ִ� ü���� ǥ���մϴ�.
        hpSlider.value = health;
    }
    // �÷��̾��� ���� ü���� �����ϴ� �޼���
    public void SetHealth(float health)
    {
        // �����̴��� ���� ���� ü�� ������ �����Ͽ� �÷��̾��� ���� ü���� ǥ���մϴ�.
        hpSlider.value = health;
    }
}
