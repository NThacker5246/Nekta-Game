using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnima : MonoBehaviour
{
	[SerializeField] private SpriteRenderer player;
	[SerializeField] private int[] lengthes = new int[4];
	[SerializeField] private Sprite[] platform;
	[SerializeField] private Sprite[] ball;
	[SerializeField] private Sprite[] wallclip;
	[SerializeField] private Sprite[] gun;
	
	[SerializeField] private int mode;
	
	[SerializeField] private int counter;
	[SerializeField] private int lim1, lim2;

	public void SetMode(int mode){
		this.mode = mode;
		SetLimits(lim1, lim2);
		counter = lim1;
		counter = 0;
		Redraw();
	}

	public void Redraw(){
		if(counter > lengthes[mode]){
			SetLimits(lim1, lim2);
			counter = lim1;
		}
		switch(mode){
			case 0: player.sprite = platform[counter]; break;
			case 1: player.sprite = ball[counter % 3]; break;
			case 2: player.sprite = wallclip[counter]; break;
			case 3: player.sprite = gun[counter & 3]; break;
		}
		++counter;
		if(counter > lim2) counter = lim1;
	}

	public void SetLimits(int l1, int l2){
		if(lim1 == l1 && lim2 == l2) return;
		lim1 = l1 > 0 ? l1 : 0; 
		Sprite[] ptr;
		switch(mode){
			case 0: ptr = platform; break;
			case 1: ptr = ball; break;
			case 2: ptr = wallclip; break;
			case 3: ptr = gun; break;
			default: ptr = platform; break;
		}
		lim2 = l2 < ptr.Length ? l2 : (ptr.Length - 1);
	}

	IEnumerator Animate(){
		while(true){
			Redraw();
			yield return new WaitForSeconds(0.125f);
		}
	}

	void Awake(){
		StartCoroutine("Animate");
		lengthes[0] = platform.Length;
		lengthes[1] = ball.Length;
		lengthes[2] = wallclip.Length;
		lengthes[3] = gun.Length;
	}




}
