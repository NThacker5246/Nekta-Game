using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootloader : MonoBehaviour
{
	[SerializeField] private Transform player;
	[SerializeField] private Transform _camera;

	[SerializeField] private Transform[] players;
	[SerializeField] private Transform[] cameras;
	[SerializeField] private GameObject[] eyes;
	[SerializeField] private int[] controllers;

	void Awake(){
		int lv = PlayerPrefs.GetInt("level");
		player.position = players[lv].position;
		_camera.position = cameras[lv].position;
		player.GetComponent<KeyMan>().eye = eyes[lv];
		player.GetComponent<PCon>().LegalControl = controllers[lv];
	}
}
