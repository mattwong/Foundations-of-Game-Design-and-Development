using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float maxSpeed = 10;
    private Rigidbody2D marioBody;
    private Animator marioAnimator;
    private float moveHorizontal;
    private bool onGroundState = true;
    public float upSpeed;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    //public Transform enemyLocation;
    public ParticleSystem dustCloud;
    public ParticleSystem fire;
    private bool falling = false;
    private bool isDead = false;


    void Start()
    {
        Application.targetFrameRate =  30;
	    marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
        GameManager.OnPlayerDeath += PlayerDeathSequence;
    }

    void Update()
    {
        if (isDead == true){
            return;
        }

        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));

        // toggle state
        if (Input.GetKeyDown("a") && faceRightState){
            faceRightState = false;
            marioSprite.flipX = true;

            if (Mathf.Abs(marioBody.velocity.x)>1.0) {
                marioAnimator.SetTrigger("onSkid");
            }
        }

        if (Input.GetKeyDown("d") && !faceRightState){
            faceRightState = true;
            marioSprite.flipX = false;

            if (Mathf.Abs(marioBody.velocity.x)>1.0) {
                marioAnimator.SetTrigger("onSkid");
            }
        }

        // if (!onGroundState && countScoreState)
        // {
        //     if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
        //     {
        //         countScoreState = false;
        //         score++;
        //         Debug.Log(score);
        //     }
        // }

        if (marioBody.velocity.y<0f)
        {
            falling = true;
        }

        // powerups
        if (Input.GetKeyDown("z")){
            CentralManager.centralManagerInstance.consumePowerup(KeyCode.Z, this.gameObject);
        }
        if (Input.GetKeyDown("x")){
            CentralManager.centralManagerInstance.consumePowerup(KeyCode.X, this.gameObject);
        }
    }

    void FixedUpdate()
    {
        if (isDead == true){
            return;
        }

        marioAnimator.SetBool("onGround", onGroundState);

        // dynamic rigidbody
        moveHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0){
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody.velocity.magnitude < maxSpeed)
                    marioBody.AddForce(movement * speed);
        }
        
        if (Input.GetKeyDown("space") && onGroundState){
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            //countScoreState = true;
        }

        if (!Input.anyKey && onGroundState){
            // make horizontal velocity 0
            marioBody.velocity = marioBody.velocity * new Vector2(0,1);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Obstacles")) && falling)
        {
            dustCloud.Play();
            onGroundState = true;
            falling = false;
            //countScoreState = false;
            //scoreText.text = "Score: " + score.ToString();
        } 
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.CompareTag("Enemy"))
    //     {
    //         Debug.Log("Collided with Gomba!");
    //         Time.timeScale = 0.0f;
    //         restartButton.SetActive(true);
    //     }
    // }

    void PlayerDeathSequence()
    {
        isDead = true;
        fire.Play();
    }
}
