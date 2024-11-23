using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
	[SerializeField] private bool inColl;
	[SerializeField] private Location nextLevel;
	[SerializeField] private PCon player;

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player") {
			inColl = true;
			player = other.GetComponent<PCon>();
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Player") {
			inColl = false;
		}
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.E) && inColl){
			nextLevel.SetController();
			player.SwitchControl();
		}
	}
}
