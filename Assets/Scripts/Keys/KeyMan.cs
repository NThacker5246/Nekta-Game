using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyMan : MonoBehaviour
{
	[SerializeField] private byte keys;
	[SerializeField] private Text txt;
	public GameObject eye;

	public void AddKey(){
		keys += 1;
		txt.text = "Keys: " + keys;
		if(keys == 3){
			eye.SetActive(true);
			keys = 0;
		}
	} 
}
