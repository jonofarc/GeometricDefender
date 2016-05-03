using UnityEngine;
using System.Collections;

public class GlobalVariables : MonoBehaviour {

	public static int Money=0;
	public static int HP=0;
	public static int TurretCost=1000;
	public static int UpgradeCost=1000; 
	public static GameObject CurrentTurret;
	public static int CurrentTurretLevel=1000; 
	public static bool DestroyTurret=false;
	public static int CurrentWave=0;
	public static int LastLevel=7;
	public static bool LevelCleared=false; 


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
