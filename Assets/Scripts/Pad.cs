using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pad : MonoBehaviour
{

	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private float velocity;
	[SerializeField] private PlayerAnima an;

	void Awake(){
		an = GetComponent<PlayerAnima>();
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player" && rb == null){
			rb = other.GetComponent<Rigidbody2D>();
			// an = other.GetComponent<PlayerAnima>();
			rb.velocity = new Vector3(rb.velocity.x, velocity);
			an.SetLimits(1, 4);
			an.SetLimitsAwait(0, 0);
			//an.SetBool("Jmp", true);
			//an.SetBool("Jmp", false);
		} else if(other.tag == "Player"){
			rb.velocity = new Vector3(rb.velocity.x, velocity);
			an.SetLimits(1, 4);
			an.SetLimitsAwait(0, 0);
			//an.SetBool("Jmp", true);
			//an.SetBool("Jmp", false);
		}
		// rb.velocity = new Vector3(rb.velocity.x, velocity);
	}
}
