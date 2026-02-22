using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public Vector3 toMov;
	public KeyMan player;
	[SerializeField] private float vel;
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private Collider2D col;
	[SerializeField] private PlayerAnima pla;
	public int counter;

	void Update(){
		if(counter > 0){
			rb.velocity = (Vector3.MoveTowards(transform.position, toMov, Time.deltaTime*vel) - transform.position) / Time.deltaTime;
			col.isTrigger = true;
			StartCoroutine("Reboot");
			counter -= 1;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "floor"){
			pla.SetLimits(1, 2);
		} else if(other.tag == "boss"){
			pla.SetLimits(3, 5);
		} else if(other.tag == "spike"){
			pla.SetLimits(3, 5);
			rb.velocity = Vector2.zero;
			StartCoroutine("Destroy");
		}
	}
	
	void OnTriggerExit2D(Collider2D other){
		pla.SetLimits(0, 0);
	}

	IEnumerator Reboot(){
		yield return new WaitForSeconds(0.05f);
		col.isTrigger = false;
		yield return new WaitForSeconds(1.95f);
		gameObject.SetActive(false);
	}

	IEnumerator Destroy(){
		// yield return new WaitForSeconds(0.05f);
		// col.isTrigger = false;
		yield return new WaitForSeconds(0.2f);
		gameObject.SetActive(false);
	}
}
