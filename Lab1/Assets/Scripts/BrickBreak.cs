using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBreak : MonoBehaviour
{
    private bool broken = false;
    public GameObject prefab;
    public GameObject flower;
    public GameConstants gameConstants;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void  OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("Player") &&  !broken){
            broken  =  true;
            // assume we have 5 debris per box
            for (int x =  0; x<5; x++){
                Instantiate(prefab, transform.position, Quaternion.identity);
            }
            gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled  =  false;
            gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled  =  false;
            GetComponent<EdgeCollider2D>().enabled = false;

            Instantiate(flower, new Vector3(Random.Range(6f, 12f), gameConstants.groundSurface, 0), Quaternion.identity);
        }
    }
}
