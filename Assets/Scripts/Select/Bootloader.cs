using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootloader : MonoBehaviour
{
	[SerializeField] private Transform player;
	[SerializeField] private Transform camera;

	[SerializeField] private Transform[] players;
	[SerializeField] private Transform[] cameras;

	void Awake(){
		int lv = PlayerPrefs.GetInt("level");
		player.position = players[lv].position;
		camera.position = cameras[lv].position;
	}
}
