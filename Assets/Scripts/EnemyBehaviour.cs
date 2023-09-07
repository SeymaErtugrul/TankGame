using DG.Tweening;
using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask grounLayer, playerLayer;

    //attack
    public GameObject bullet;
    public Transform spawnerPoint;
    private bool isAttacking = true;
    Ray theRay;


    public float rayCastRange;
    public GameObject raycastObject;

    //patroll
    public float distanceMax;
    public float detectionRadius;
    Vector3 random;
    public int movementMod; //0 randompoint, 1 chase


    //upgrade
    public string Tag = "upgradeDouble";
    bool doubleShoot = false;

    private enum AIState
    {
        Patrol,
        Chase,
        Attack
    }

    private AIState currentState = AIState.Patrol;

    private void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = true;
        StartCoroutine("randomPoint");
        Debug.Log(Vector3.Distance(transform.position, player.position));

    }
    private void Awake()
    {
        player = GameObject.Find("TankCube").transform;
    }

    private void Update()
    {

        switch (currentState)
        {
            case AIState.Patrol:
                randomPoint();
                break;

            case AIState.Chase:
                chase();
                break;

            case AIState.Attack:
                attack();
                break;
        }

    }


    private void changeState(AIState newState)
    {
        currentState = newState;
    }

    IEnumerator attackIE()
    {
        if (doubleShoot)
        {
            doubleShoot = false;
            Instantiate(bullet, spawnerPoint.position + spawnerPoint.right, spawnerPoint.rotation);
            Instantiate(bullet, spawnerPoint.position - spawnerPoint.right, spawnerPoint.rotation);
            Debug.Log("bombalRRRR atildi");
            changeState(AIState.Attack);

        }
        else
        {
      
            GameObject go = Instantiate(bullet, spawnerPoint.transform.position, spawnerPoint.rotation);
            Debug.Log("bomba atildi");
            yield return new WaitForSeconds(3f);
      
        }

    }


    private bool isPlayerInAttackRange()
    {

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);  //playera olan mesafenin max mesafeden küçük olup olmadığını döndürüyor

        return distanceToPlayer <= distanceMax;
    }


    public void attack()
    {
        Vector3 direction = Vector3.forward;
        theRay = new Ray(transform.position + new Vector3(0, 1, 0), transform.TransformDirection(direction * rayCastRange));
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), transform.TransformDirection(direction * rayCastRange));
        if (!isPlayerInAttackRange())
        {
            changeState(AIState.Patrol);
        }

        if (Physics.Raycast(theRay, out RaycastHit hit, rayCastRange))
        {
            if (hit.collider.CompareTag("Player")) //playerla temas edip etmediğini buldum
            {
                Debug.Log("playerhit");

                if (isPlayerInAttackRange())
                {
                    Debug.Log("player burda");

                }
            }


        }

    }

    IEnumerator randomPoint()
    {

        do
        {
            random = transform.position + Random.insideUnitSphere * detectionRadius;
        } while (!isPointWithinSceneBounds(random));

        NavMeshHit hit;
        NavMesh.SamplePosition(random, out hit, detectionRadius, NavMesh.AllAreas);

        agent.SetDestination(hit.position);
        Debug.Log("pozisyon verildi");
        yield return new WaitForSeconds(2f);
        StartCoroutine("randomPoint");
        upgradeCollect();
        if (isPlayerInAttackRange())
        {
            changeState(AIState.Chase);
        }
    }

    bool isPointWithinSceneBounds(Vector3 point)
    {
        Vector3 sceneMin = new Vector3(-16f, -0.1f, -9f);
        Vector3 sceneMax = new Vector3(16f, -0.1f, 9f);
        return point.x >= sceneMin.x && point.x <= sceneMax.x &&
               point.z >= sceneMin.z && point.z <= sceneMax.z;
    }
    public void chase()
    {

        float distanceToPlayer = Vector3.Distance(transform.position, player.position); //playera olan mesafesini buldum
        if (distanceToPlayer <= 6)
        {
            agent.speed = 0f;
            Vector3 directionToPlayer = player.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.DORotateQuaternion(targetRotation, 0.5f).SetEase(Ease.OutQuad);
        }
        else if (isPlayerInAttackRange() && distanceToPlayer >= 6)
        {
            agent.speed = 3.5f;
            agent.SetDestination(new Vector3(player.position.x, player.position.y, player.position.z));
        }

        else
        {
            agent.speed = 3.5f;
            changeState(AIState.Patrol);
        }

    }



    public void upgradeCollect()
    {
        GameObject[] targetObjects = GameObject.FindGameObjectsWithTag(Tag);

        foreach (GameObject targetObject in targetObjects)
        {
            float distanceToTarget = Vector3.Distance(transform.position, targetObject.transform.position);

            if (distanceToTarget <= detectionRadius)
            {
                agent.SetDestination(targetObject.transform.position);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("upgradeDouble"))
        {
            doubleShoot = true;
            other.gameObject.SetActive(false);
        }
    }
}
