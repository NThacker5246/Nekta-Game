using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSCamera : MonoBehaviour
{
	[SerializeField] private Transform player;

	void Update(){
		if(transform.position.x != player.position.x || transform.position.y != player.position.y){
			transform.position = new Vector3(player.position.x, player.position.y, -10);
		}
	}
}
