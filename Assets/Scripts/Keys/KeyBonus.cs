using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBonus : MonoBehaviour
{
	[SerializeField] private bool flag;

	void OnTriggerEnter2D(Collider2D other){
		if(!flag) {
			if(other.tag == "Player"){
				other.GetComponent<KeyMan>().AddKey();
				Destroy(gameObject);
				flag = true;
			} else if(other.tag == "Bullet"){
				other.GetComponent<Bullet>().player.AddKey();
				Destroy(gameObject);
				flag = true;
			}
		}
	}
}
