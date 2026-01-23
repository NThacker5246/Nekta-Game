using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenAdaptive : MonoBehaviour
{
	[SerializeField] private CanvasScaler sc;

	void Awake(){
		float factor = Screen.width / 1920f;
		sc.scaleFactor = factor;
	}
}
