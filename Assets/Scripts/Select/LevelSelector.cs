using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
	[SerializeField] private int level;
	[SerializeField] private Text txt;
	[SerializeField] private Transform lp;

	[SerializeField] private Vector3[] menu;

	void Start(){
		lp.localPosition = menu[level];
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			level++;
			txt.text = "" + (level+1);
			lp.localPosition = menu[level];
		} else if(Input.GetKeyDown(KeyCode.DownArrow)){
			level--;
			txt.text = "" + (level+1);		
			lp.localPosition = menu[level];	
		}

		if(Input.GetKeyDown(KeyCode.Space)){
			PlayerPrefs.SetInt("level", level);
			SceneManager.LoadScene(2);
		}

		if(Input.GetKeyDown(KeyCode.Q)){
			SceneManager.LoadScene(0);
		}
	}
}
