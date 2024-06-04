using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fdsfsdf11 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 모든 게임 오브젝트를 검색하여 오디오 리스너 컴포넌트가 있는지 확인합니다.
        AudioListener[] audioListeners = FindObjectsOfType<AudioListener>();

        // 찾은 모든 오디오 리스너 출력
        foreach (AudioListener listener in audioListeners)
        {
            Debug.Log("Found Audio Listener on GameObject: " + listener.gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
