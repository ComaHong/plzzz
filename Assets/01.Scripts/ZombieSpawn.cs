using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    [Header("���� ���� ��")]
    public GameObject[] zombiePrefab; // ���� ������
    public Transform zombieSpawnPosition; // ������ ���� ��ġ
    public GameObject dangerZone1; //������ ���� ��ġ(dangerZone)
    private float repeatCycle = 1f; // �����ð�����

    // �����
    public AudioClip DangerZoneSound; // �������� �߻�����
    public AudioSource audioSource; // ������ҽ�

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InvokeRepeating("EnemySpawner", 1f, repeatCycle);
            audioSource.PlayOneShot(DangerZoneSound);
            StartCoroutine(dangerZoneTimer());
            Destroy(gameObject, 10f);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
    void EnemySpawner()
    {
        int randomIndex = Random.Range(0, zombiePrefab.Length);
        Instantiate(zombiePrefab[randomIndex], zombieSpawnPosition.position, zombieSpawnPosition.rotation);
    }
    IEnumerator dangerZoneTimer()
    {
        dangerZone1.SetActive(true);
        yield return new WaitForSeconds(5f);
        dangerZone1.SetActive(false);
    }
}
