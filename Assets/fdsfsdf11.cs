using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fdsfsdf11 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // ��� ���� ������Ʈ�� �˻��Ͽ� ����� ������ ������Ʈ�� �ִ��� Ȯ���մϴ�.
        AudioListener[] audioListeners = FindObjectsOfType<AudioListener>();

        // ã�� ��� ����� ������ ���
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
