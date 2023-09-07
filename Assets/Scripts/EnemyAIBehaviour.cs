using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIBehaviour : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    public LayerMask playerLayer;
    public Transform spawnerPoint;
    //patroll
    public float distanceMax;

    public float detectionRadius;
    Vector3 random;

    //upgrade
    public string Tag = "upgradeDouble";
    bool doubleShoot = false;

    //attack
    Ray theRay;
    public float rayCastRange;
    public GameObject raycastObject;
    public bool canShoot = false;
    public GameObject bullet;
    bool playerHitRay;

    public GameObject enemy;
    public enum AIState
    {
        Patrol,
        Chase,
        Attack
    }
    private void changeState(AIState newState)
    {
        currentState = newState;
    }

    private AIState currentState = AIState.Patrol;
    void Start()
    {
        StartCoroutine("stateMachine");
       
  
    }

    private IEnumerator stateMachine()
    {
        
            switch (currentState)
            {
                case AIState.Patrol:
                    StartCoroutine("patrol");
                    break;

                case AIState.Chase:
                    StartCoroutine("chase");
                    break;

                case AIState.Attack:
                    StartCoroutine("attackIE");
                    break;

                default:
                    // Durum bulunamadıysa yapılacak işlem
                    break;
            }

            yield return null; // Bir sonraki frame'de devam et
        
    }

    IEnumerator patrol()
    {
       

        random = transform.position + UnityEngine.Random.insideUnitSphere * detectionRadius;
        NavMeshHit hit;
        NavMesh.SamplePosition(random, out hit, detectionRadius, NavMesh.AllAreas);

        agent.SetDestination(hit.position);
        Debug.Log("pozisyon verildi");
        upgradeCollect();
        yield return new WaitForSeconds(2f);
        if (isPlayerInAttackRange())
        {
            changeState(AIState.Chase);
            StartCoroutine("chase");
            yield return null;
        }
        else
        {
            StartCoroutine("patrol");
            yield return new WaitForSeconds(0.5f);
        }

    }

    private bool İsPlayerInAttackRange()
    {

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);  //playera olan mesafenin max mesafeden küçük olup olmadığını döndürüyor

        return distanceToPlayer <= distanceMax;
    }


  IEnumerator chase() 
    { 
        float distanceToPlayer = Vector3.Distance(transform.position, player.position); //playera olan mesafesini buldum
        if (distanceToPlayer <= 6)
        {
            agent.speed = 0f;
            Vector3 directionToPlayer = player.position - spawnerPoint.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.DORotateQuaternion(targetRotation, 1f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                StartCoroutine("wait");
                changeState(AIState.Attack);
            });
          
            yield return null;

        }
        else if (isPlayerInAttackRange())
        {
            agent.speed = 3.5f;
            agent.SetDestination(new Vector3(player.position.x, player.position.y, player.position.z));
            yield return new WaitForSeconds(0.5f);
            changeState(AIState.Attack);

        }

        else
        {
            agent.speed = 3.5f;
            changeState(AIState.Patrol);
            StartCoroutine("patrol");
            yield return new WaitForSeconds(1f);
        }
        StartCoroutine("chase");
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
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



    private bool isPlayerInAttackRange()
    {

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);  //playera olan mesafenin max mesafeden küçük olup olmadığını döndürüyor

        return distanceToPlayer <= distanceMax;
    }



    private IEnumerator shootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(3f);
        canShoot = true;
    }



    IEnumerator attackIE()
    {
        if (doubleShoot)
        {
            doubleShoot = false;
            Instantiate(bullet, spawnerPoint.position + spawnerPoint.right, spawnerPoint.rotation);
            Instantiate(bullet, spawnerPoint.position - spawnerPoint.right, spawnerPoint.rotation);
            yield return new WaitForSeconds(2f);
        }
  else if(isPlayerInAttackRange() &&canShoot)
        {
            Instantiate(bullet, spawnerPoint.transform.position, spawnerPoint.rotation);
            StartCoroutine("shootCooldown");
            yield return null;
        }  
      


        if (!isPlayerInAttackRange())
        {
            changeState(AIState.Patrol);
        }
    
    }



    public void raycastHit()
    {
        Vector3 direction = Vector3.forward;
        theRay = new Ray(transform.position + new Vector3(0, 1, 0), transform.TransformDirection(direction * rayCastRange));
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), transform.TransformDirection(direction * rayCastRange));

        if (!isPlayerInAttackRange())
        {
            changeState(AIState.Patrol);
        }

        if (Physics.Raycast(theRay, out RaycastHit hit, rayCastRange,playerLayer))
        {
            if (hit.collider.CompareTag("Player")) //playerla temas edip etmediğini buldum
            {
            
                if (isPlayerInAttackRange())
                {
                    StartCoroutine("attackIE");

                }
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
        if (other.gameObject.CompareTag("trap"))
        {
            enemy.GetComponent<HealthManagerScript>().takeDamage(10);
        }
    }

    void Update()
    {
        raycastHit();
    }
   
}
