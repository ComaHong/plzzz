using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class Zombie1 : MonoBehaviour
{
    [Header("좀비 체력과 데미지")]
    private float zombieHealth = 100f;
    private float presentHealth;
    public float giveDamage = 5f;
    public HealthBar hpSlider;

    [Header("좀비 필요한것들")]
    public NavMeshAgent zombieAgent; // 네비매쉬 에이전트 컴포넌트
    public Transform LookPoint; // 좀비가 바라볼 위치
    public Camera attackinRaycastArea; // 좀비시점 레이캐스트용 카메라 컴포넌트
    public Transform playerBody; // 플레이어의 위치
    public LayerMask PlayerLayer; // 플레이어 레이어

    [Header("좀비 가이드 바")]
    public GameObject[] walkPoints; // 좀비 웨이포인트
    int currentZombiePosition = 0;
    public float zombieSpeed; // 좀비 이동속도
    public float walkingpointRadius = 2f; // 좀비 이동할 반지름

    [Header("좀비 애니메이션")]
    public Animator zombieanim;

    [Header("좀비 공격 바")]
    public float timeBtwAttack; //이전공격과 다음공격 사이의 시간
    bool previouslyAttack; // 이전공격

    [Header("좀비 mood/states")]
    public float visionRadius; // 좀비의 시야범위
    public float attackingRadius; // 좀비의 공격범위
    public bool playerInvisionRadius; // 플레이어가 좀비시야에 들어 왔는지를 판별할 bool타입 변수
    public bool playerInattackingRadius; // 플레이어가 좀비의공격범위안에 들어 왔는지를 판별할 bool타입 변수

    private void Awake()
    {
        presentHealth = zombieHealth;
        // 시작하면 네비메쉬컴포넌트를 가져옴
        zombieAgent = GetComponent<NavMeshAgent>();
        hpSlider.GiveFullHealth(zombieHealth);
        playerBody = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    private void Update()
    {
        // CheckSphere로 좀비가 플레이어를 판별함
        playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, PlayerLayer);
        playerInattackingRadius = Physics.CheckSphere(transform.position, attackingRadius, PlayerLayer);

        // 플레이어가 좀비시야범위,공격범위 둘다 들어오지 않았을때 
        if (!playerInvisionRadius && !playerInattackingRadius)
        {
            Guard();
        }
        // 플레이어가 좀비시야범위에 들어오면서, 공격범위에는 들어오지 않았을때
        if (playerInvisionRadius && !playerInattackingRadius)
        {
            Pursueplayer();
        }
        // 플레이어가 좀비시야범위와 공격범위에 둘다  들어왔을때
        if (playerInattackingRadius && playerInattackingRadius)
        {
            AttackPlayer();
        }
    }

    // 좀비가 플레이어를 발견하지 못했을때 사용할 메서드
    private void Guard()
    {
        if (Vector3.Distance(walkPoints[currentZombiePosition].transform.position, transform.position) < walkingpointRadius)
        {
            currentZombiePosition = Random.Range(0, walkPoints.Length);
            if (currentZombiePosition >= walkPoints.Length)
            {
                currentZombiePosition = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, walkPoints[currentZombiePosition].transform.position, Time.deltaTime * zombieSpeed);
        transform.LookAt(walkPoints[currentZombiePosition].transform.position);
    }
    // 좀비가 플레이어를 발견했을때 사용할 메서드
    private void Pursueplayer()
    {
        if (zombieAgent.SetDestination(playerBody.position))
        {
            // 애니메이션
            zombieanim.SetBool("Running", true);
            zombieanim.SetBool("Walking", false);
            zombieanim.SetBool("Attacking", false);
            zombieanim.SetBool("Died", false);
        }
        else
        {
            zombieanim.SetBool("Walking", false);
            zombieanim.SetBool("Running", false);
            zombieanim.SetBool("Attacking", false);
            zombieanim.SetBool("Died", true);
        }
    }
    // 좀비의 시야,공격사거리에 플레이어가 모두 들어왔을떄 사용할 메서드
    private void AttackPlayer()
    {
        zombieAgent.SetDestination(transform.position);
        transform.LookAt(LookPoint);
        if (!previouslyAttack)
        {
            RaycastHit hitinfo;
            if (Physics.Raycast(attackinRaycastArea.transform.position, attackinRaycastArea.transform.forward, out hitinfo, attackingRadius))
            {
                Debug.Log("공격중" + hitinfo.transform.name);

                PlayerController playerBody = hitinfo.transform.GetComponent<PlayerController>();

                if (playerBody != null)
                {
                    playerBody.playerHitDamage(giveDamage);
                }

                zombieanim.SetBool("Attacking", true);
                zombieanim.SetBool("Walking", false);
                zombieanim.SetBool("Running", false);
                zombieanim.SetBool("Died", false);
            }
            previouslyAttack = true;
            Invoke(nameof(ActiveAttacking), timeBtwAttack);
        }
    }

    private void ActiveAttacking()
    {
        previouslyAttack = false;

    }
    public void zombieHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        hpSlider.SetHealth(presentHealth);

        if (presentHealth <= 0)
        {
            zombieanim.SetBool("Attacking", false);
            zombieanim.SetBool("Walking", false);
            zombieanim.SetBool("Running", false);
            zombieanim.SetBool("Died", true);

            zombieDie();
        }
    }

    // 좀비가 죽었을때 사용할 메서드
    private void zombieDie()
    {
        zombieAgent.SetDestination(transform.position);
        zombieSpeed = 0f;
        attackingRadius = 0f;
        visionRadius = 0f;
        playerInattackingRadius = false;
        playerInattackingRadius = false;
        Destroy(gameObject, 5.0f);
    }
}
