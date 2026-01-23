using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sounds : MonoBehaviour
{
	[SerializeField] private AudioSource[] sfx;
	[SerializeField] private AudioSource[] music;
	[SerializeField] private Image msc, sf;
	[SerializeField] private Sprite a, b, c, d;

	public void InverseMusic(){
		int tes = (PlayerPrefs.GetInt("e_music") ^ 1);
		PlayerPrefs.SetInt("e_music", tes);
		if(tes == 1) msc.sprite = b;
		else msc.sprite = a;
		for(int i = 0; i < music.Length; ++i){
			if(tes == 1){
				music[i].mute = true;
			} else {
				music[i].mute = false;
			}
		}
	}

	public void SetMusic(){
		int tes = PlayerPrefs.GetInt("e_music");
		if(msc != null){
			if(tes == 1) msc.sprite = b;
			else msc.sprite = a;
		}

		for(int i = 0; i < music.Length; ++i){
			if(tes == 1){
				music[i].mute = true;
			} else {
				music[i].mute = false;
			}
		}
	}

	public void InverseSFX(){
		int tes = (PlayerPrefs.GetInt("e_sfx") ^ 1);
		PlayerPrefs.SetInt("e_sfx", tes);
		if(tes == 1) sf.sprite = d;
		else sf.sprite = c;
		for(int i = 0; i < sfx.Length; ++i){
			if(tes == 1){
				sfx[i].mute = true;
			} else {
				sfx[i].mute = false;
			}
		}
	}

	public void SetSFX(){
		int tes = PlayerPrefs.GetInt("e_sfx");
		if(sf != null){
			if(tes == 1) sf.sprite = d;
			else sf.sprite = c;
		}

		for(int i = 0; i < sfx.Length; ++i){
			if(tes == 1){
				sfx[i].mute = true;
			} else {
				sfx[i].mute = false;
			}
		}
	}

	public void Awake(){
		if(!PlayerPrefs.HasKey("e_sfx")){
			PlayerPrefs.SetInt("e_sfx", 0);
		}
		if(!PlayerPrefs.HasKey("e_music")){
			PlayerPrefs.SetInt("e_music", 0);
		}
		SetSFX();
		SetMusic();
	}
}
