using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SwitchCamera : MonoBehaviour
{
    [Header("Camera to Assign")]
    public GameObject AimCam; // 줌했을때 사용할 에임캠
    public GameObject AimCanvas; // 에임캠을 담당하는 캔버스
    public GameObject ThirdPersonCam; // 플레이어 3인칭 카메라
    public GameObject ThirdPersonCanvas; // 플레이어 3인칭 카메라 담당 캔버스
    

    [Header("Camera Animator")]
    public Animator anim; // 플레이어의 애니메이터

    private void Start()
    {
        // 시작할떈 3인칭 카메라 true 에임캠 flase
        ThirdPersonCam.SetActive(true);
        ThirdPersonCanvas.SetActive(true);
        AimCam.SetActive(false);
        AimCanvas.SetActive(false);

    }
    private void Update()
    {
        if (Input.GetButton("Fire2") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetBool("Idle", false);
            anim.SetBool("IdleAim", true);
            //anim.SetBool("RifleWalk", true);
            anim.SetBool("Walk", true);

            ThirdPersonCam.SetActive(false);
            ThirdPersonCanvas.SetActive(false);
            AimCam.SetActive(true);
            AimCanvas.SetActive(true);

        }

        else if (Input.GetButton("Fire2"))
        {
            anim.SetBool("Idle", false);
            anim.SetBool("IdleAim", true);
            //anim.SetBool("RifleWalk", false);
            anim.SetBool("Walk", false);
            ThirdPersonCam.SetActive(false);
            ThirdPersonCanvas.SetActive(false);
            AimCam.SetActive(true);
            AimCanvas.SetActive(true);

        }
        else
        {
            anim.SetBool("Idle", true);
            anim.SetBool("IdleAim", false);
         //  anim.SetBool("RifleWalk", false);
            ThirdPersonCam.SetActive(true);
            ThirdPersonCanvas.SetActive(true);
            AimCam.SetActive(false);
            AimCanvas.SetActive(false);


        }
    }
}


