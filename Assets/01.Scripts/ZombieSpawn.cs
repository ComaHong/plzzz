using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    [Header("좀비 생성 바")]
    public GameObject zombiePrefab; // 좀비 프리팹
    public Transform zombieSpawnPosition; // 좀비의 스폰 위치
    public GameObject dangerZone1; //좀비의 스폰 위치(dangerZone)
    private float repeatCycle = 1f; // 스폰시간단위

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
        if(other.gameObject.tag == "Player")
        {
            InvokeRepeating("EnemySpawner", 1f, repeatCycle);
            StartCoroutine(dangerZoneTimer());
            Destroy(gameObject, 10f);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
    void EnemySpawner()
    {
        Instantiate(zombiePrefab, zombieSpawnPosition.position, zombieSpawnPosition.rotation);
    }
    IEnumerator dangerZoneTimer()
    {
        dangerZone1.SetActive(true);
        yield return new WaitForSeconds(5f);
        dangerZone1.SetActive(false);
    }
}
