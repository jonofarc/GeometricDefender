using UnityEngine;
using System.Collections;

public class TranslateTexture : MonoBehaviour {
	private Material OriginalMaterial;
	private float CurrentOffsetX=0;
	public float speed = 1;
	// Use this for initialization
	void Start () {
		
		OriginalMaterial = this.gameObject.GetComponent<Renderer> ().material;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(CurrentOffsetX >=1){
			CurrentOffsetX = 0;

		}
		CurrentOffsetX=CurrentOffsetX+ (Time.deltaTime/speed);

		this.gameObject.GetComponent<Renderer> ().material.mainTextureOffset = new Vector2(CurrentOffsetX, 0);	
	}
}
