using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Location : MonoBehaviour
{
	[SerializeField] private LevelList[] list;
	[SerializeField] private PCon player;
	[SerializeField] private Transform cam, ll;

 	[SerializeField] public int level, chapter;
 	[SerializeField] private bool rest;
 	[SerializeField] private KeyMan plakeys;
 	[SerializeField] private GameObject[] cutscenes, audios;
 	[SerializeField] private GameObject c4, c5, master_cut;
 	public bool dont;
	public void SetController(){
		//player.transform.position = playerPos.position;
		//player.LegalControl = LegalControl;
		//cam.position = new Vector3(camPos.position.x, camPos.position.y, -10);
		//player.GetComponent<KeyMan>().eye = eye;
	}

	public void Awake(){
		// for(int i = 0; i < list.Length; ++i){
		// 	list[i]._controlls = new Controllers[10];
		// 	for(int j = 0; j < 10; ++j){
		// 		int cons = 0;

		// 		if((list[i].controlls[j] & 1) != 0){
		// 			++cons;
		// 		}
		// 		if((list[i].controlls[j] & 2) != 0){
		// 			++cons;
		// 		}
		// 		if((list[i].controlls[j] & 4) != 0){
		// 			++cons;
		// 		}
		// 		if((list[i].controlls[j] & 8) != 0){
		// 			++cons;
		// 		}
		// 		print(i);
		// 		print(j);
		// 		list[i]._controlls[j].able = new int[cons];
		// 		cons = 0;
		// 		if((list[i].controlls[j] & 1) != 0){
		// 			list[i]._controlls[j].able[cons] = 1;
		// 			++cons;
		// 		}
		// 		if((list[i].controlls[j] & 2) != 0){
		// 			list[i]._controlls[j].able[cons] = 2;
		// 			++cons;
		// 		}
		// 		if((list[i].controlls[j] & 4) != 0){
		// 			list[i]._controlls[j].able[cons] = 3;
		// 			++cons;
		// 		}
		// 		if((list[i].controlls[j] & 8) != 0){
		// 			list[i]._controlls[j].able[cons] = 4;
		// 			++cons;
		// 		}

		// 	}
		// }
	}

	public void NextLevel(){
		plakeys.Reset();
		Transform lv = transform.GetChild(chapter > 0 ? chapter : 0).GetChild(level > 0 ? level : 0);
		lv.gameObject.SetActive(false);
		++level;
		if(level >= list[chapter > 0 ? chapter : 0].levels) {
			level = 0; 
			audios[chapter > 0 ? chapter : 0].SetActive(false);
			++chapter;
			audios[chapter].SetActive(true);
			if(PlayerPrefs.GetInt("elighable") < chapter) {PlayerPrefs.SetInt("elighable", chapter);} 
			if(!dont){
				if(chapter == 3) {c4.SetActive(true); StartCoroutine("AwaitPlaying");}
				else if(chapter == 4) {c5.SetActive(true); StartCoroutine("AwaitPlaying");} 
				else cutscenes[chapter].SetActive(true);
			} else cutscenes[chapter].SetActive(true);
			
			master_cut.SetActive(true);
		}
		player.LegalControl = list[chapter].controlls[level];
		player._LegalControl = list[chapter]._controlls[level];
		
		if(chapter > 1) player._chapterTime = 10f;

		cam.position = new Vector3((level * 14.2f) + 2.25f, (chapter * 10.35f), -10f);
		player.transform.position = list[chapter].playerPoses[level];
		player.SetFirstPossibleController();
		lv = transform.GetChild(chapter).GetChild(level);
		lv.gameObject.SetActive(true);
	}

	public void RestartLevel(){
		// rest = 20;
		plakeys.Reset();
		plakeys.RestartKeys();
		list[chapter].eyes[level].SetActive(false);
		player.LegalControl = list[chapter].controlls[level];
		cam.position = new Vector3((level * 14.2f) + 2.25f, (chapter * 10.35f), -10f);
		player.transform.position = list[chapter].playerPoses[level];
		Transform lv = transform.GetChild(chapter).GetChild(level);
		for(int i = 0; i < lv.childCount; ++i){
			lv.GetChild(i).gameObject.SetActive(false);
		}
		ll = lv;
		rest = true;
	}

	void Update(){
		// if(rest > 0 && --rest == 0){
			// plakeys.RestartKeys();
		// }
		if(rest){
			for(int i = 0; i < ll.childCount; ++i){
				if(ll.GetChild(i).name != "Eye") ll.GetChild(i).gameObject.SetActive(true);
			}
			rest = false;
		}

		
	}

	public void DisplayEye(){
		list[chapter].eyes[level].SetActive(true);
	}

	IEnumerator AwaitPlaying(){
		float delay = 0f;

		if(chapter == 3) {delay = (float) c4.GetComponent<VideoPlayer>().clip.length;}
		else if(chapter == 4) {delay = (float) c5.GetComponent<VideoPlayer>().clip.length;}
		yield return new WaitForSeconds(delay);
		c4.SetActive(false);
		c5.SetActive(false);
		cutscenes[chapter].SetActive(true);
		PlayerPrefs.SetInt("chapter", chapter); 
		SceneManager.LoadScene(2);
	}
}

[System.Serializable]
public struct LevelList {
	public int levels;
	public int[] controlls;
	public Controllers[] _controlls;
	public Vector3[] playerPoses;
	public GameObject[] eyes;
}

[System.Serializable]
public struct Controllers {
	public int[] able;
}