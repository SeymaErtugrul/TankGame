using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthManagerScript : MonoBehaviour
{
    
    public float healthAmount = 100f;

    public Transform takipEdilenObje;

    public GameObject uiElementPrefab;

    public Transform parentTransform; 

    GameObject newUIElement;

    Image newUIFill;

    public bool isDie = false;

    public GameObject particle;

    public GameObject goldCoin;

    private void Start()
    {
        newUIElement = Instantiate(uiElementPrefab, parentTransform);

        newUIFill = newUIElement.GetComponentInChildren<Image>();

      
    }
    private void Update()
    {
        healthbarFollow();
        if (isDie)
        {
            StartCoroutine("wait");

           
            //gameObject.SetActive(false);
            //newUIElement.SetActive(false);
        }

       
    }

    public void takeDamage(float damage)
    {
   
        healthAmount -= damage;
        newUIFill.fillAmount = healthAmount / 100f;
        if (healthAmount <= 0)
        {
            particle.SetActive(true);
            isDie = true;
        }

    }

    private void healthbarFollow()
    {
        if (takipEdilenObje != null && newUIElement != null)
        {
            Vector3 objeEkrandakiPozisyon = Camera.main.WorldToScreenPoint(takipEdilenObje.position + new Vector3(0, 0, 2));
            newUIFill.rectTransform.position = objeEkrandakiPozisyon;
        }
      
    }

    private void heal(float heal)
    {
        healthAmount += heal;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        newUIFill.fillAmount = healthAmount / 100;
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
        Destroy(newUIElement);
        Instantiate(goldCoin,transform.position+new Vector3(0,0.2f,0),transform.rotation);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bomb"))
        {
            takeDamage(20);
          
        }
    }
}
