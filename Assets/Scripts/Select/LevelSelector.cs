using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
	[SerializeField] private int chapter;
	[SerializeField] private Image txt;
	[SerializeField] private Transform lp;

	[SerializeField] private Vector3[] menu;
	[SerializeField] private Sprite[] nums;

	void Start(){
		lp.localPosition = menu[chapter];
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			print(PlayerPrefs.GetInt("elighable"));
			if(chapter + 1 > PlayerPrefs.GetInt("elighable")) return;
			++chapter;
			txt.sprite = nums[chapter];
			lp.localPosition = menu[chapter];
		} else if(Input.GetKeyDown(KeyCode.DownArrow)){
			if (--chapter < 0) chapter = 0;
			txt.sprite = nums[chapter];		
			lp.localPosition = menu[chapter];	
		}

		if(Input.GetKeyDown(KeyCode.Space)){
			PlayerPrefs.SetInt("level", 0);
			PlayerPrefs.SetInt("chapter", chapter);
			SceneManager.LoadScene(2);
		}

		if(Input.GetKeyDown(KeyCode.Q)){
			SceneManager.LoadScene(0);
		}
	}
}
