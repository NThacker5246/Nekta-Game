using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBonus : MonoBehaviour
{
	[SerializeField] private bool flag;
	[SerializeField] private PlayerAnima anim;
	[SerializeField] private AudioSource src;

	void Awake(){
		anim = GetComponent<PlayerAnima>();
		anim.SetLimits(0, 7);
	}

	void OnTriggerEnter2D(Collider2D other){
		if(!flag) {
			if(other.tag == "Player"){
				other.GetComponent<KeyMan>().AddKey(gameObject);
				StartCoroutine("Dest");
				flag = true;
				src.Play();
			} else if(other.tag == "Bullet"){
				other.GetComponent<Bullet>().player.AddKey(gameObject);
				StartCoroutine("Dest");
				flag = true;
				src.Play();
			}
		}
	}

	IEnumerator Dest(){
		anim.SetLimits(8, 11);
		yield return new WaitForSeconds(0.5f);
		gameObject.SetActive(false);
		StopCoroutine("Dest");
	}

	void OnEnable(){
		flag = false;
		anim.SetLimits(0, 7);
	}
}