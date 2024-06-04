using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class Zombie1 : MonoBehaviour
{
    [Header("���� ü�°� ������")]
    private float zombieHealth = 100f;
    private float presentHealth;
    public float giveDamage = 5f;
    public HealthBar hpSlider;

    [Header("���� �ʿ��Ѱ͵�")]
    public NavMeshAgent zombieAgent; // �׺�Ž� ������Ʈ ������Ʈ
    public Transform LookPoint; // ���� �ٶ� ��ġ
    public Camera attackinRaycastArea; // ������� ����ĳ��Ʈ�� ī�޶� ������Ʈ
    public Transform playerBody; // �÷��̾��� ��ġ
    public LayerMask PlayerLayer; // �÷��̾� ���̾�

    [Header("���� ���̵� ��")]
    public GameObject[] walkPoints; // ���� ��������Ʈ
    int currentZombiePosition = 0;
    public float zombieSpeed; // ���� �̵��ӵ�
    public float walkingpointRadius = 2f; // ���� �̵��� ������

    [Header("���� �ִϸ��̼�")]
    public Animator zombieanim;

    [Header("���� ���� ��")]
    public float timeBtwAttack; //�������ݰ� �������� ������ �ð�
    bool previouslyAttack; // ��������

    [Header("���� mood/states")]
    public float visionRadius; // ������ �þ߹���
    public float attackingRadius; // ������ ���ݹ���
    public bool playerInvisionRadius; // �÷��̾ ����þ߿� ��� �Դ����� �Ǻ��� boolŸ�� ����
    public bool playerInattackingRadius; // �÷��̾ �����ǰ��ݹ����ȿ� ��� �Դ����� �Ǻ��� boolŸ�� ����

    private void Awake()
    {
        presentHealth = zombieHealth;
        // �����ϸ� �׺�޽�������Ʈ�� ������
        zombieAgent = GetComponent<NavMeshAgent>();
        hpSlider.GiveFullHealth(zombieHealth);
        playerBody = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    private void Update()
    {
        // CheckSphere�� ���� �÷��̾ �Ǻ���
        playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, PlayerLayer);
        playerInattackingRadius = Physics.CheckSphere(transform.position, attackingRadius, PlayerLayer);

        // �÷��̾ ����þ߹���,���ݹ��� �Ѵ� ������ �ʾ����� 
        if (!playerInvisionRadius && !playerInattackingRadius)
        {
            Guard();
        }
        // �÷��̾ ����þ߹����� �����鼭, ���ݹ������� ������ �ʾ�����
        if (playerInvisionRadius && !playerInattackingRadius)
        {
            Pursueplayer();
        }
        // �÷��̾ ����þ߹����� ���ݹ����� �Ѵ�  ��������
        if (playerInattackingRadius && playerInattackingRadius)
        {
            AttackPlayer();
        }
    }

    // ���� �÷��̾ �߰����� �������� ����� �޼���
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
    // ���� �÷��̾ �߰������� ����� �޼���
    private void Pursueplayer()
    {
        if (zombieAgent.SetDestination(playerBody.position))
        {
            // �ִϸ��̼�
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
    // ������ �þ�,���ݻ�Ÿ��� �÷��̾ ��� �������� ����� �޼���
    private void AttackPlayer()
    {
        zombieAgent.SetDestination(transform.position);
        transform.LookAt(LookPoint);
        if (!previouslyAttack)
        {
            RaycastHit hitinfo;
            if (Physics.Raycast(attackinRaycastArea.transform.position, attackinRaycastArea.transform.forward, out hitinfo, attackingRadius))
            {
                Debug.Log("������" + hitinfo.transform.name);

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

    // ���� �׾����� ����� �޼���
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
