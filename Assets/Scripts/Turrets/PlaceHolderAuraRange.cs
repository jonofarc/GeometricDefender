using UnityEngine;
using System.Collections;

public class PlaceHolderAuraRange : MonoBehaviour {
    private float turretRange = 1;
    // Use this for initialization
    void Start() {
        //  if there is one selected we find the turret range of the current selected turret
        if (GlobalVariables.CurrentTurret != null)
        {
           
            TargetCreep myscript = GlobalVariables.CurrentTurret.gameObject.GetComponentInChildren<TargetCreep>();

            if (myscript != null)
            {
                turretRange = myscript.turretRange;
            }
            
            this.transform.localScale = new Vector3(turretRange, 0.01f, turretRange);
            this.GetComponent<Renderer>().enabled = true;

        }
        else {
            Debug.Log("Range not avaible no turrret selected");
        }


    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
