using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Starter : MonoBehaviour
{
	[SerializeField] private VideoPlayer vd;

	void Awake(){
		StartCoroutine("LoadGame");
	}

	IEnumerator LoadGame(){
		yield return new WaitForSeconds((float) vd.clip.length);
		SceneManager.LoadScene(1);
	}

}
