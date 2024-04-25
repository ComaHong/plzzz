using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider hpSlider;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GiveFullHealth(float health)
    {
        hpSlider.maxValue = health;

        hpSlider.value = health;
    }
    public void SetHealth(float health)
    {
        hpSlider.value = health;
    }
}
