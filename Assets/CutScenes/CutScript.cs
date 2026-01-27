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
	[SerializeField] private new GameObject audio;
	[SerializeField] private PCon pla;

	void OnEnable(){
		src = GetComponent<VideoPlayer>();
		src.Play();
		isStarted = false;
		if(audio != null) audio.SetActive(false);
		if(pla != null) pla.enabled = false;
	}

	void Update(){
		if((!src.isPlaying || Input.GetKey(KeyCode.Return)) && isStarted){
			if(audio != null) audio.SetActive(true);
			if(pla != null) pla.enabled = true;
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
