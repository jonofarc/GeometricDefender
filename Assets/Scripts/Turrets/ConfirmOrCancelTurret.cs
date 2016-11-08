using UnityEngine;
using System.Collections;

public class ConfirmOrCancelTurret : MonoBehaviour {
    //confirm boolean dictates if its for the confirm option or not
    //true means confirm false means cancel
    public bool confirm=true;
	
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseDown()
    {

        if (confirm)
        {
           
            GlobalVariables.CurrentTurretPlace.SendMessage("PlaceTurret");
        }
        else {
            GlobalVariables.CurrentTurretPlace.SendMessage("destroyPlaceHolder");
        }
        

    }
}
