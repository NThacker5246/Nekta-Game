using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
	[SerializeField] private bool inColl;
	//[SerializeField] private Location nextLevel;
	//[SerializeField] private PCon player;
	[SerializeField] private Location locall;


	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player") {
			inColl = true;
			//player = other.GetComponent<PCon>();
		} else if(other.tag == "Bullet"){
			//nextLevel.SetController();
			//player.SwitchControl();
			locall.NextLevel();
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Player") {
			inColl = false;
		}
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.E) && inColl){
			locall.NextLevel();
		}
	}
}
