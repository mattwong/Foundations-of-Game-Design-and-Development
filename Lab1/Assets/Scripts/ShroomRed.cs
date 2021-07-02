using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomRed : MonoBehaviour, ConsumableInterface
{
    public Texture t;
    private bool collected = false;
    private SpriteRenderer shroomSprite;

    void Start()
    {
        shroomSprite = GetComponent<SpriteRenderer>();
    }

	public void consumedBy(GameObject player){
		// give player speed boost
        Debug.Log("Player jump boost");
		player.GetComponent<PlayerController>().upSpeed += 5;
		StartCoroutine(removeEffect(player));
	}

	IEnumerator removeEffect(GameObject player){
		yield return new WaitForSeconds(5.0f);
		player.GetComponent<PlayerController>().upSpeed -= 5;
        Debug.Log("Player jump boost ends");
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && !collected){
            // update UI
            collected = true;
            CentralManager.centralManagerInstance.addPowerup(t, 1, this);
            StartCoroutine(collect());
        }
    }

    IEnumerator collect()
    {
        int steps = 5;
        float stepper = this.transform.localScale.x/(float) steps;

        for (int i = 0; i < steps; i++){
            this.transform.localScale = new Vector3(this.transform.localScale.x - stepper, this.transform.localScale.y - stepper, this.transform.localScale.z);
            yield return null;
        }
    }
}
