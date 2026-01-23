using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootloader : MonoBehaviour
{
	[SerializeField] private Location locall;
	[SerializeField] private GameObject[] cuts;
	[SerializeField] private PCon player;

	void Awake(){
		int lv = PlayerPrefs.GetInt("level");
		int cp = PlayerPrefs.GetInt("chapter");
		if(cp < 0) cp = 0;
		locall.level = 9;
		locall.chapter = cp-1;
		locall.dont = true;
		cuts[0].SetActive(false);
		cuts[cp].SetActive(true);
		locall.NextLevel();
		locall.dont = false;
		// PlayerPrefs.SetInt("level", 0);
		// PlayerPrefs.SetInt("chapter", 0);
		player.SetFirstPossibleController();
		//player.position = players[lv].position;
		//_camera.position = cameras[lv].position;
		//player.GetComponent<KeyMan>().eye = eyes[lv];
		//player.GetComponent<PCon>().LegalControl = controllers[lv];
	}
}
