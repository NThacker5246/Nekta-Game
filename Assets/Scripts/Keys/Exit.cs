using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
	[SerializeField] private bool inColl;
	//[SerializeField] private Location nextLevel;
	//[SerializeField] private PCon player;
	[SerializeField] private Location locall;
	[SerializeField] private bool flag;
	[SerializeField] private PlayerAnima anim, player;
	[SerializeField] private GameObject swapCanvas;


	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player") {
			inColl = true;
			anim.SetLimits(13, 14);
			player = other.GetComponent<PlayerAnima>();
		} else if(!flag && other.tag == "Bullet"){
			//nextLevel.SetController();
			//player.SwitchControl();
			locall.NextLevel();
			flag = !flag;

		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Player") {
			inColl = false;
			anim.SetLimits(9, 10);
		}
	}

	void Update(){
		if(!flag && Input.GetKeyDown(KeyCode.E) && inColl){
			flag = !flag;
			StartCoroutine("End");
		}
	}

	void OnEnable(){
		anim = GetComponent<PlayerAnima>();
		anim.SetLimits(0, 3);
		anim.SetLimitsAwait(9, 10);
	}

	IEnumerator End(){
		anim.SetLimits(4, 8);
		if(player != null){
			player.SetMode(0);
			player.SetLimitsAsBlock(15, 25);
			//player.SetLimitsAwait(0, 6);
			yield return new WaitForSeconds(1f);
		} 
		//yield return new WaitForSeconds(1f);
		swapCanvas.SetActive(true);
		yield return new WaitForSeconds(0.75f);
		locall.NextLevel();
		swapCanvas.SetActive(false);
	}
}
