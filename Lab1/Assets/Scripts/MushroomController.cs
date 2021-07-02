using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    private Rigidbody2D mushroomBody;
    public float speed = 10;
    private Vector2 currentDirection;
    private Vector2 currentPosition;
    private bool move = false;

    void Start()
    {
        mushroomBody = GetComponent<Rigidbody2D>();
        mushroomBody.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        // random direction
        int randomDirection = Random.Range(0,2);
        if (randomDirection == 0) {currentDirection = new Vector2(1,0);}
        else {currentDirection = new Vector2(-1,0);}

        mushroomBody.AddForce(currentDirection * 5, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        MoveMushroom(currentDirection);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) 
        {
            move = true;
        }
        if (col.gameObject.CompareTag("End")) 
        {
            //Destroy(this.gameObject);
        }
        if (col.gameObject.CompareTag("Player")) 
        {
            move = false;
        }
        if (col.gameObject.CompareTag("Pipe")) 
        {
            currentDirection *= -1;
        }
    }

    void MoveMushroom(Vector2 currentDirection)
    {
        if (move == true) 
        {
            currentPosition = mushroomBody.position;
            Vector2 nextPosition = currentPosition + speed * currentDirection * Time.fixedDeltaTime;
            mushroomBody.MovePosition(nextPosition);
        }
    }

    void OnBecameInvisible(){
        //Destroy(gameObject);	
    }
}
