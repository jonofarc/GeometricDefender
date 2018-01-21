using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragTurrets : MonoBehaviour {
	public bool DragAssistEnabled = true;
	[Header("Drag Assist on Screen %")]
	public Vector2 DragAssist;
	public Vector3 DragAssistCordinates;

	// Use this for initialization
	void Start () {
		Vector2 s = new Vector2 (Screen.width,Screen.height);
		DragAssistCordinates = new Vector3 ((s.x*DragAssist.x/100f),(s.y*DragAssist.y/100f),0); 
	
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
			if (GlobalVariables.PlaceHolderTurret != null){
				this.SendMessage ("ConfirmTurret",true);

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
