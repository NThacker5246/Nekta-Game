using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSwapper : MonoBehaviour
{
	[SerializeField] private Sprite[] tilemaps;
	[SerializeField] private int counter;
	[SerializeField] private Image img;
	[SerializeField] private float dt = 0.5f;

	void Awake(){
		// StartCoroutine("Anima");
	}

	void OnEnable(){
		StartCoroutine("Anima");
	}

	void OnDisable(){
		StopCoroutine("Anima");
		counter = 0;
	}

	IEnumerator Anima(){
		while(true){
			yield return new WaitForSeconds(dt);
			if(++counter == tilemaps.Length) counter = 0;
			img.sprite = tilemaps[counter];
		}
	}
}
