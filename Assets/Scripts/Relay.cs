using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relay : MonoBehaviour
{
	[SerializeField] private GameObject[] active;
	[SerializeField] private GameObject[] passive;

	[SerializeField] private bool flag;

	void Start(){
		StartCoroutine("UpdateWall");
	}

	IEnumerator UpdateWall(){
		while(true) {
			flag = flag ^ true;
			for(byte i = 0; i < active.Length; i++){
				active[i].SetActive(flag);
			}

			for(byte i = 0; i < passive.Length; i++){
				passive[i].SetActive(!flag);
			}

			yield return new WaitForSeconds(1f);
		}
	}
}
