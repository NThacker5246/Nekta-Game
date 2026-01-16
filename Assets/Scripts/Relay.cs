using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relay : MonoBehaviour
{

	[SerializeField] private GameObject[] active;
	[SerializeField] private GameObject[] passive;

	[SerializeField] private bool flag;
	[SerializeField] private int counter;
	[SerializeField] private byte mode = 0;

	void Start(){
		// StartCoroutine("UpdateWall");
	}

	IEnumerator UpdateWall(){
		switch(mode){
			case 0: 
				while(true) {
					flag = flag ^ true;
					for(byte i = 0; i < active.Length; i++){
						// active[i].SetActive(flag);
						active[i].GetComponent<BoxCollider2D>().isTrigger = flag;
						active[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, flag ? 0.5f : 1f);
					}

					for(byte i = 0; i < passive.Length; i++){
						passive[i].GetComponent<BoxCollider2D>().isTrigger = !flag;
						passive[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, flag ? 1f : 0.5f);
					}

					yield return new WaitForSeconds(2f);
				}
				break;
			case 1:
				while(true){
					active[counter].SetActive(false);
					++counter;
					if(counter == active.Length) counter = 0;
					active[counter].SetActive(true);
					yield return new WaitForSeconds(2f);
				}
				break;
		}
		
	}

	void OnEnable(){
		StartCoroutine("UpdateWall");
	}
}
