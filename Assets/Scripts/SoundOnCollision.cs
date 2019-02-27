using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnCollision : MonoBehaviour {
	private AudioSource NoBuildZoneAudioSource;
	// Use this for initialization
	void Start () {
		NoBuildZoneAudioSource = this.gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag.Equals(GlobalVariables.CreepTag)){
			NoBuildZoneAudioSource.Play ();
		}
	}
}
