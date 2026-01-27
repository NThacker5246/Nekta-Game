using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable CS0162

public class Relay : MonoBehaviour
{

	[SerializeField] private GameObject[] active;
	[SerializeField] private GameObject[] passive;

	[SerializeField] private bool flag;
	[SerializeField] private int counter;
	[SerializeField] private byte mode = 0;

	private BoxCollider2D[] _ac, _pc;
	private SpriteRenderer[] _as, _ps;

	void Awake(){
		// StartCoroutine("UpdateWall");
		_ac = new BoxCollider2D[active.Length];
		_pc = new BoxCollider2D[passive.Length];
		_as = new SpriteRenderer[active.Length];
		_ps = new SpriteRenderer[passive.Length];

		for(int i = 0; i < active.Length; ++i){
			_ac[i] = active[i].GetComponent<BoxCollider2D>();
			_as[i] = active[i].GetComponent<SpriteRenderer>();
		}
		for(int i = 0; i < passive.Length; ++i){
			_pc[i] = passive[i].GetComponent<BoxCollider2D>();
			_ps[i] = passive[i].GetComponent<SpriteRenderer>();
		}
	}

	IEnumerator UpdateWall(){
		switch(mode){
			case 0: 
				while(true) {
					flag = flag ^ true;
					for(byte i = 0; i < active.Length; i++){
						// active[i].SetActive(flag);
						_ac[i].isTrigger = flag;
						_as[i].color = new Color(1f, 1f, 1f, flag ? 0.5f : 1f);
					}

					for(byte i = 0; i < passive.Length; i++){
						_pc[i].isTrigger = !flag;
						_ps[i].color = new Color(1f, 1f, 1f, flag ? 1f : 0.5f);
					}

					yield return new WaitForSeconds(2f);
				}
				break;
			case 1:
				while(true){
					_ac[counter].isTrigger = true;
					_as[counter].color = new Color(1f, 1f, 1f, 0.5f);
					// active[counter].SetActive(false);
					++counter;
					if(counter == active.Length) counter = 0;
					// active[counter].SetActive(true);
					_ac[counter].isTrigger = false;
					_as[counter].color = new Color(1f, 1f, 1f, 1f);
					yield return new WaitForSeconds(2f);
				}
				break;
		}
		
	}

	void OnEnable(){
		StartCoroutine("UpdateWall");
	}
}
