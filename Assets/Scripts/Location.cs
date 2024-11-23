using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
	[SerializeField] private Transform camPos;
	[SerializeField] private Transform playerPos;

	[SerializeField] private GameObject eye;


	[SerializeField] private bool[] LegalControl;
	[SerializeField] private PCon player;
	[SerializeField] private Transform cam;
 
	public void SetController(){
		player.transform.position = playerPos.position;
		player.LegalControl = LegalControl;
		cam.position = new Vector3(camPos.position.x, camPos.position.y, -10);
		player.GetComponent<KeyMan>().eye = eye;
	}
}