using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBonus : MonoBehaviour
{
	[SerializeField] private bool flag;
	[SerializeField] private Animator anim;

	void Awake(){
		anim = GetComponent<Animator>();
	}

	void OnTriggerEnter2D(Collider2D other){
		if(!flag) {
			if(other.tag == "Player"){
				other.GetComponent<KeyMan>().AddKey();
				StartCoroutine("Dest");
				flag = true;
			} else if(other.tag == "Bullet"){
				other.GetComponent<Bullet>().player.AddKey();
				StartCoroutine("Dest");
				flag = true;
			}
		}
	}

	IEnumerator Dest(){
		anim.SetTrigger("Dest");
		yield return new WaitForSeconds(1f);
		gameObject.SetActive(false);
		StopCoroutine("Dest");
	}
}