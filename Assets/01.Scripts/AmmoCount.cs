using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCount : MonoBehaviour
{
    // �Ѿ��ؽ�Ʈ ������Ʈ
    public Text ammoText;
    // źâ�ؽ�Ʈ ������Ʈ
    public Text magText;
    // �� ��ũ��Ʈ�� �ν��Ͻ��� ���������� ������ �� �ֵ��� ���� ������ ����
    public static AmmoCount Instance;

    private void Awake()
    {
        // �ν��Ͻ��� ����ִٸ� �̰��� �ν��Ͻ�ȭ��
        if (Instance == null)
        {
            Instance = this;
        }

    }
     // �Ѿ� �ؽ�Ʈ ������Ʈ �޼���
    public void UpdateAmmoText(int presentAmmo)
    {
        ammoText.text = "Ammo. " + presentAmmo;
    }
    // źâ �ؽ�Ʈ ������Ʈ �޼���
    public void UpdateMagText(int mag)
    {
        magText.text = "Magazines. " + mag;
    }
}
