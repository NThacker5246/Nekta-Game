using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipController : MonoBehaviour
{
	[SerializeField] private PCon player;
    
    void OnTriggerEnter2D(Collider2D other){
    	if(other.tag == "Player"){
    		player.SkipControl();
    		gameObject.SetActive(false);
    	}
    	if(other.tag == "Bullet"){
    		player.SkipControl();
    		gameObject.SetActive(false);
    	}
    }
}
