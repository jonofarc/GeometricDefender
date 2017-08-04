using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragTurrets : MonoBehaviour {
	public bool DragAssist = true;
	public Vector3 DragAssistCordinates;

	// Use this for initialization
	void Start () {
		DragAssistCordinates = new Vector3 (0,(Screen.height/13f),0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if (Input.GetMouseButton (0)) { 
			RaycastHit hit; 
			Ray ray;
			if (DragAssist) {
				ray = Camera.main.ScreenPointToRay ((Input.mousePosition+DragAssistCordinates)); 
			} else {
				ray = Camera.main.ScreenPointToRay (Input.mousePosition); 
			}

			if (Physics.Raycast (ray, out hit, 100.0f)) {
				
				Debug.Log ("You selected the " + hit.transform.name); // ensure you picked right object
				if (hit.transform.gameObject.tag == GlobalVariables.TurretBuildingTag) {
					hit.transform.gameObject.SendMessage ("setPlaceHolder", SendMessageOptions.DontRequireReceiver);
					//this.SendMessage ("CheckTurretOptions",true);
				} else if (GlobalVariables.PlaceHolderTurret != null){
					

					Destroy(GlobalVariables.PlaceHolderTurret);
					
				}

			}
		}else if(Input.GetMouseButtonUp(0) ){
			if (GlobalVariables.PlaceHolderTurret != null){
				this.SendMessage ("ConfirmTurret",true);
			}
		}
	}
	public void DragPossitionUPToogle(){
		DragAssist = !DragAssist;
	}
}
