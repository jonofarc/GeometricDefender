using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class DragTurrets : MonoBehaviour {
	public bool DragAssistEnabled = true;
	[Header("Drag Assist on Screen %")]
	public Vector2 DragAssist;
	public Vector3 DragAssistCordinates;
	public bool ActivateCoolDowns = false;
	private GameObject[] ButtonsCheckmarks;
	public float CoolDownTime = 3.0f;
	// Use this for initialization
	void Start () {
		Vector2 s = new Vector2 (Screen.width,Screen.height);
		DragAssistCordinates = new Vector3 ((s.x*DragAssist.x/100f),(s.y*DragAssist.y/100f),0); 
		ButtonsCheckmarks = GameObject.FindGameObjectsWithTag ("Checkmark");

	}

	void FixedUpdate()
	{
		if(ActivateCoolDowns){
			foreach (GameObject ButtonCheckmark in ButtonsCheckmarks ){
				ButtonCheckmark.GetComponent<Image>().fillAmount += 1.0f / CoolDownTime * Time.deltaTime;
			}
			// if we Finished CD time
			if(ButtonsCheckmarks[ButtonsCheckmarks.Length-1].GetComponent<Image>().fillAmount >= 1){
				ActivateCoolDowns = false;
			}
		}
	}
	// Update is called once per frame
	void Update () {
		
		if (Input.GetMouseButton (0)) { 
			RaycastHit hit; 
			Ray ray;
			if (DragAssistEnabled) {
				ray = Camera.main.ScreenPointToRay ((Input.mousePosition+DragAssistCordinates)); 
			} else {
				ray = Camera.main.ScreenPointToRay (Input.mousePosition); 
			}

			if (Physics.Raycast (ray, out hit, 100.0f)) {
				
				//Debug.Log ("You selected the " + hit.transform.name); // ensure you picked right object
				if (hit.transform.gameObject.tag == GlobalVariables.TurretBuildingTag || hit.transform.gameObject.tag == GlobalVariables.TurretTag) {
					hit.transform.gameObject.SendMessage ("DragSelect", SendMessageOptions.DontRequireReceiver);


					if(hit.transform.gameObject.tag == GlobalVariables.TurretTag && GlobalVariables.PlaceHolderTurret != null) {
						Destroy(GlobalVariables.PlaceHolderTurret);
					}
					//this.SendMessage ("CheckTurretOptions",true);
				} else if (GlobalVariables.PlaceHolderTurret != null) {
					

					Destroy (GlobalVariables.PlaceHolderTurret);
					GlobalVariables.CurrentTurret.SendMessage ("DisableAura",SendMessageOptions.DontRequireReceiver);
					
				} 


			}
		}else if(Input.GetMouseButtonUp(0) ){
			if (GlobalVariables.PlaceHolderTurret != null && ActivateCoolDowns == false) {
				this.SendMessage ("ConfirmTurret", true);
				if (GlobalVariables.GameStarted) {
					ActivateCoolDowns = true;	
					foreach (GameObject ButtonCheckmark in ButtonsCheckmarks) {
						ButtonCheckmark.GetComponent<Image> ().fillAmount = 0;
					}
				}


			} else {
				this.SendMessage ("ConfirmTurret", false);
			}

			RaycastHit hit; 
			Ray ray;
			if (DragAssistEnabled) {
				ray = Camera.main.ScreenPointToRay ((Input.mousePosition+DragAssistCordinates)); 
			} else {
				ray = Camera.main.ScreenPointToRay (Input.mousePosition); 
			}

			if (Physics.Raycast (ray, out hit, 100.0f)) {

				if (GlobalVariables.DestroyTurret) {
					hit.transform.gameObject.SendMessage ("DestroyTurret",0.5f, SendMessageOptions.DontRequireReceiver);
				}
			}


		}
	}
	public void DragPossitionUPToogle(){
		DragAssistEnabled = !DragAssistEnabled;
	}


}
