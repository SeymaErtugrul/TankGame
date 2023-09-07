using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackBehaviourScript : MonoBehaviour
{
    public GameObject bomb;
    public Transform spawnPoint;

    public float doubleShootDuration = 3.0f; // İkili ateş süresi (saniye)
    private bool upgradeDouble = false;
    private bool canShoot = true;
    public float shootCoolDown = 1.2f;

    public PlayerInput PlayerInput;

    public CharacterController controller;


    private int coins = 0;
    private void Start()
    {
        PlayerInput = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("upgradeDouble"))
        {
            Debug.Log("upgrade");
            StartCoroutine(EnableDoubleShoot());
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("goldCoins"))
        {
            coins = PlayerPrefs.GetInt("coins", coins);
            coins += 10;
            PlayerPrefs.SetInt("coins", coins);
            Debug.Log(PlayerPrefs.GetInt("coins",coins));
            PlayerPrefs.Save();
        }
    }

    private void shoot()
    {
        if (canShoot)
        {
            if (upgradeDouble)
            {
                DoubleBomb();

            }
            else
            {
                InstantiateBomb();

            }
          
            StartCoroutine(ShootCooldown());
        }
    }

    public void InstantiateBomb()
    {
        Instantiate(bomb, spawnPoint.position, spawnPoint.rotation);
    }

    public void DoubleBomb()
    {
        Instantiate(bomb, spawnPoint.position + spawnPoint.right, spawnPoint.rotation);
        Instantiate(bomb, spawnPoint.position - spawnPoint.right, spawnPoint.rotation);
    }

    private IEnumerator EnableDoubleShoot()
    {
        upgradeDouble = true;
        yield return new WaitForSeconds(doubleShootDuration);
        upgradeDouble = false;
    }

    private IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCoolDown);
        canShoot = true;
    }

    private void Update()
    {
        if (PlayerInput.actions["Fire"].triggered)
        {
            shoot();
        }
    }
}



