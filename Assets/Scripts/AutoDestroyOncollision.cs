﻿using UnityEngine;
using System.Collections;

public class AutoDestroyOncollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag==GlobalVariables.CreepTag){
			Destroy (this.gameObject);
		}

	}


}
