using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyMan : MonoBehaviour
{
	[SerializeField] private byte keys;
	[SerializeField] public Text txt;
	[SerializeField] private Image tx;
	[SerializeField] private Sprite[] sp;
	[SerializeField] private Location locall;
	[SerializeField] private GameObject[] lastkeys = new GameObject[3];
	[SerializeField] private int counter;

	public void AddKey(GameObject that){
		keys += 1;
		lastkeys[counter] = that;
		++counter;
		if(counter == 3) counter = 0;
		// txt.text = "Keys: " + keys;
		tx.sprite = sp[keys];
		if(keys == 3){
			//eye.SetActive(true);
			keys = 0;
			locall.DisplayEye();
			//txt.text = "Keys: " + keys;
		}
	} 

	public void RestartKeys(){
		// for(int i = 0; i < 3; ++i) {lastkeys[i].SetActive(true);}
		//counter = 0;
		keys = 0;
	}

	public void Reset(){
		keys = 0;
		tx.sprite = sp[keys];
	}
}
