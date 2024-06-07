using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellicopterFollow : MonoBehaviour
{
    public Cinemachine.CinemachineDollyCart dollyCart;
   
    void Update()
    {
        if (dollyCart != null)
        {
            transform.position = dollyCart.transform.position;
            transform.rotation = dollyCart.transform.rotation;
        }
    }
}
