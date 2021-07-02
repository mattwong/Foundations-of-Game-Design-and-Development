using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameConstants gameConstants;
    private float originalX;
    private int moveRight;
    private Vector2 velocity;
    private Rigidbody2D enemyBody;
    private SpriteRenderer enemySprite;
    private bool end = false;
    public bool dead = false;

    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        enemySprite = GetComponent<SpriteRenderer>();
        // get the starting position
        originalX = transform.position.x;
        // randomise initial direction
		moveRight = Random.Range(0, 2) == 0 ? -1 : 1;
        ComputeVelocity();

        GameManager.OnPlayerDeath += EnemyRejoice;
    }

    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX) < gameConstants.maxOffset)
        {
            if (moveRight < 0) {
                enemySprite.flipX = true;
            }
            else enemySprite.flipX = false;
            MoveGomba();
        }
        else {
            // change direction
            moveRight *= -1;
            if (moveRight < 0) {
                enemySprite.flipX = true;
            }
            else enemySprite.flipX = false;
            ComputeVelocity();
            MoveGomba();
        }
    }

    void ComputeVelocity(){
        velocity = new Vector2((moveRight)*gameConstants.maxOffset / gameConstants.enemyPatroltime, 0);
    }

    void MoveGomba(){
        if (end == false) {
            enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
        }
    }

    void  OnTriggerEnter2D(Collider2D other){
		// check if it collides with Mario
		if (other.gameObject.tag == "Player" && dead == false){
			// check if collides on top
			float yoffset = (other.transform.position.y - this.transform.position.y);
			if (yoffset > 0.75f){
				KillSelf();
			}
			else if (dead == false){
				CentralManager.centralManagerInstance.damagePlayer();
			}
		}
	}

    void KillSelf(){
		// enemy dies
        dead = true;
		CentralManager.centralManagerInstance.increaseScore();
		StartCoroutine(flatten());
		Debug.Log("Kill sequence ends");
	}

    IEnumerator flatten(){
		Debug.Log("Flatten starts");
        float initialScale_y = this.transform.localScale.y;
		int steps = 5;
		float stepper = initialScale_y/(float) steps;

		for (int i = 0; i < steps; i++){
			this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y - stepper, this.transform.localScale.z);

			// make sure enemy is still above ground
			this.transform.position = new Vector3(this.transform.position.x, gameConstants.groundSurface  +  GetComponent<SpriteRenderer>().bounds.extents.y, this.transform.position.z);
			yield return null;
		}
		Debug.Log("Flatten ends");
        this.transform.localScale = new Vector3(this.transform.localScale.x, initialScale_y, this.transform.localScale.z);
        dead = false;
		this.gameObject.SetActive(false);
		Debug.Log("Enemy returned to pool");
		yield break;
	}

    void EnemyRejoice()
    {
        StartCoroutine(enemyDance());
    }

    IEnumerator enemyDance()
    {
        end = true;
        while (true) {
            moveRight *= -1;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
