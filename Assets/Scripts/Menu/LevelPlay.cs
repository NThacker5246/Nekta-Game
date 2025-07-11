using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPlay : MonoBehaviour
{
	[SerializeField] private GameObject men;
	[SerializeField] private bool bl;

	public void Play(){
		SceneManager.LoadScene(1);
	}
	
	public void Exit(){
		SceneManager.LoadScene(0);
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			bl = !bl;
			men.SetActive(bl);
		}
	}
}
