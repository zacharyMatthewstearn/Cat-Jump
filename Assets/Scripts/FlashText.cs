using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashText : MonoBehaviour {
	private Text text;
	private bool aToB = true;
	private float alpha = 1f;
	public float changeRate = 0.04f;

	void Start () {
		text = gameObject.GetComponent<Text> ();	
	}
	

	void Update () {
		if (aToB) {
			alpha -= changeRate;
			if (alpha <= 0) {
				alpha = 0;
				aToB = false;
			}
		}
		else {
			alpha += changeRate;
			if (alpha >= 1) {
				alpha = 1;
				aToB = true;
			}
		}
		text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
	}
}
