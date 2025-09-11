using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
	[SerializeField] private LevelList[] list;
	[SerializeField] private PCon player;
	[SerializeField] private Transform cam;

 	[SerializeField] public int level, chapter;
 	[SerializeField] private int rest;
 	[SerializeField] private KeyMan plakeys;

	public void SetController(){
		//player.transform.position = playerPos.position;
		//player.LegalControl = LegalControl;
		//cam.position = new Vector3(camPos.position.x, camPos.position.y, -10);
		//player.GetComponent<KeyMan>().eye = eye;
	}

	public void NextLevel(){
		plakeys.txt.text = "Keys: 0";
		++level;
		if(level >= list[chapter].levels) {level = 0; ++chapter;}
		player.LegalControl = list[chapter].controlls[level];
		cam.position = new Vector3((level * 14.2f) + 2.25f, (chapter * 10.35f), -10f);
		player.transform.position = list[chapter].playerPoses[level];
		player.SetFirstPossibleController();
	}

	public void RestartLevel(){
		rest = 20;
		plakeys.txt.text = "Keys: 0";
		list[chapter].eyes[level].SetActive(false);
		player.LegalControl = list[chapter].controlls[level];
		cam.position = new Vector3((level * 14.2f) + 2.25f, (chapter * 10.35f), -10f);
		player.transform.position = list[chapter].playerPoses[level];
	}

	void Update(){
		if(rest > 0 && --rest == 0){
			plakeys.RestartKeys();
		}
		
	}

	public void DisplayEye(){
		list[chapter].eyes[level].SetActive(true);
	}
}

[System.Serializable]
public struct LevelList {
	public int levels;
	public int[] controlls;
	public Vector3[] playerPoses;
	public GameObject[] eyes;
}