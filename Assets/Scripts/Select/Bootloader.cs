using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootloader : MonoBehaviour
{
	[SerializeField] private Location locall;
	[SerializeField] private PCon player;

	void Awake(){
		int lv = PlayerPrefs.GetInt("level");
		int cp = PlayerPrefs.GetInt("chapter");
		locall.level = lv - 1;
		locall.chapter = cp;
		locall.NextLevel();
		PlayerPrefs.SetInt("level", 0);
		PlayerPrefs.SetInt("chapter", 0);
		player.SetFirstPossibleController();
		//player.position = players[lv].position;
		//_camera.position = cameras[lv].position;
		//player.GetComponent<KeyMan>().eye = eyes[lv];
		//player.GetComponent<PCon>().LegalControl = controllers[lv];
	}
}
