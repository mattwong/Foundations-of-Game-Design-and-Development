using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameConstants gameConstants;
    void Awake()
    {
        // spawn two gombaEnemy
        for (int j = 0; j < 2; j++)
            spawnFromPooler(ObjectType.gombaEnemy);
    }

    void Start()
    {
        GameManager.SpawnEnemy += SpawnRandom;
    }

    void spawnFromPooler(ObjectType i){
        // static method access
        GameObject item = ObjectPooler.sharedInstance.getPooledObject(i);
        if (item != null){
            //set position, and other necessary states
            item.transform.position = new Vector3(Random.Range(-10f, -1f), gameConstants.groundSurface, 0);
            //item.transform.localScale = new Vector3(1,1,1);
            item.SetActive(true);
        }
        else{
            Debug.Log("not enough items in the pool.");
        }
    }

    void SpawnRandom()
    {
        ObjectType type = Random.Range(0, 2) == 0 ? ObjectType.gombaEnemy : ObjectType.greenEnemy;
        Debug.Log("Enemy spawned: "+ type);
        spawnFromPooler(type);
    }
}
