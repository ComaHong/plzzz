using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCount : MonoBehaviour
{
    public Text ammoText;
    public Text magText;

    public static AmmoCount Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }
    public void UpdateAmmoText(int presentAmmo)
    {
        ammoText.text = "Ammo. " + presentAmmo;
    }

    public void UpdateMagText(int mag)
    {
        magText.text = "Magazines. " + mag;
    }
}
