using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutScript : MonoBehaviour
{
	[SerializeField] private VideoPlayer src;
	[SerializeField] private bool isStart;
	[SerializeField] private bool isStarted;
	[SerializeField] private GameObject audio;

	void OnEnable(){
		src = GetComponent<VideoPlayer>();
		src.Play();
		isStarted = false;
		if(audio != null) audio.SetActive(false);
	}

	void Update(){
		if((!src.isPlaying || Input.GetKey(KeyCode.Return)) && isStarted){
			if(audio != null) audio.SetActive(true);
			if(isStart){
				SceneManager.LoadScene(1);
			}
			transform.parent.gameObject.SetActive(false);
			gameObject.SetActive(false);
		} else if(src.isPlaying){
			isStarted = true;
		}
	}
}
