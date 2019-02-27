using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAudioPlayer : MonoBehaviour {

	private AudioSource CameraAudioSource;
	// Use this for initialization
	void Start () {
		CameraAudioSource = this.gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlaySound(string SoundPath){
		//AudioClip AddTurretSoundEffect = Resources.Load<AudioClip>("Sound/NewTurretSoundEffect");
		AudioClip AddTurretSoundEffect = Resources.Load<AudioClip>(SoundPath);
		if (AddTurretSoundEffect != null) {
			CameraAudioSource.clip = AddTurretSoundEffect;
			CameraAudioSource.Play ();
		} else {
			Debug.LogError ("Sound effect "+ SoundPath +" not found");
		}
	}

}
