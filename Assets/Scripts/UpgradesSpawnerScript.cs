using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UpgradesSpawnerScript : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize;

    private List<GameObject> pooledObjects;
    private int nextObjectIndex = 0;

    public GameObject player;
    Vector3 Pos;

    Transform[] upgradePoints;

    NavMeshHit hit;
    private void Start()
    {
        initializePool();
        StartCoroutine("spawn");
    }


    private void initializePool()
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pooledObjects.Add(obj);
         
        }
    }

    public GameObject getPooledObject()
    {

        GameObject obj = pooledObjects[nextObjectIndex];
        nextObjectIndex++;

        // Eğer havuzdaki nesnelerin sonuna geldiysek başa dön
        if (nextObjectIndex >= pooledObjects.Count)
        {
            nextObjectIndex = 0;
        }

        return obj;
    }

    public void returnToPool(GameObject obj)
    {
        // Nesneyi havuza geri koy
        obj.SetActive(false);
        
    }


    IEnumerator spawn()
    {
        while (true)
        {
            for (int i = 0; i < poolSize; i++)
            {

                Pos.y = 0;
                Pos.x = Random.RandomRange(-17, 17);
                Pos.z = Random.RandomRange(-8, 8);


                if (NavMesh.SamplePosition(Pos, out hit, 10.0f, NavMesh.AllAreas))
                {
                    Pos = hit.position;
                    getPooledObject().transform.position = Pos;
                    getPooledObject().SetActive(true);
                }


                yield return new WaitForSeconds(3f);
            }


        }
        
    }
}
