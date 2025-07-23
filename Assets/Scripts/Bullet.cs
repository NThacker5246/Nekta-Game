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
	public int counter;

	void Update(){
		if(counter > 0){
			rb.velocity = (Vector3.MoveTowards(transform.position, toMov, Time.deltaTime*vel) - transform.position) / Time.deltaTime;
			col.isTrigger = true;
			StartCoroutine("Reboot");
			counter -= 1;
		}
	}

	IEnumerator Reboot(){
		yield return new WaitForSeconds(0.05f);
		col.isTrigger = false;
		yield return new WaitForSeconds(1.95f);
		gameObject.SetActive(false);
	}
}
