using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutScript : MonoBehaviour
{
	[SerializeField] private VideoPlayer src;
	[SerializeField] private bool isStarted;
	[SerializeField] private AudioSource audio;

	void OnEnable(){
		src = GetComponent<VideoPlayer>();
		src.Play();
		isStarted = false;
		if(audio != null) audio.mute = true;
	}

	void Update(){
		if(!src.isPlaying && isStarted){
			if(audio != null) audio.mute = false;
			transform.parent.gameObject.SetActive(false);
			gameObject.SetActive(false);
		} else if(src.isPlaying){
			isStarted = true;
		}
	}
}
