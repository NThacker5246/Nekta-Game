using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
	[SerializeField] private Location locall;

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			locall.RestartLevel();
		}

	}
}
