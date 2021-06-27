using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debris : MonoBehaviour
{
    private Rigidbody2D debrisBody;
    private Vector3 scaler;

    // Start is called before the first frame update
    void Start()
    {
        debrisBody = GetComponent<Rigidbody2D>();
        scaler  =  transform.localScale  / (float) 30 ;
        StartCoroutine("ScaleOut");
    }

    IEnumerator  ScaleOut(){

        Vector2 direction =  new  Vector2(Random.Range(-1.0f, 1.0f), 1);
        debrisBody.AddForce(direction.normalized  *  10, ForceMode2D.Impulse);
        debrisBody.AddTorque(10, ForceMode2D.Impulse);
        // wait for next frame
        yield  return  null;

        // render for 0.5 second
        for (int step =  0; step  < 30; step++)
        {
            this.transform.localScale  =  this.transform.localScale  -  scaler;
            // wait for next frame
            yield  return  null;
        }

        Destroy(gameObject);

    }
}
