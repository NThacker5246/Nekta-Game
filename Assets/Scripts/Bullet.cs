using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public Vector3 toMov;
	public KeyMan player;
	[SerializeField] private float vel;

	void Update(){
		//transform.position = Vector3.MoveTowards(transform.position, toMov, Time.deltaTime*vel);
	} 
}
