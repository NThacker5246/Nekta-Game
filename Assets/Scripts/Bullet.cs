using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public Vector3 toMov;
	public KeyMan player;
	[SerializeField] private float vel;
	[SerializeField] private Rigidbody2D rb;
	public bool flag;

	void Update(){
		if(flag){
			rb.velocity = (Vector3.MoveTowards(transform.position, toMov, Time.deltaTime*vel) - transform.position) / Time.deltaTime;
			StartCoroutine("Reboot");
			flag = false;
		}
	}

	IEnumerator Reboot(){
		yield return new WaitForSeconds(2);
		gameObject.SetActive(false);
	}
}
