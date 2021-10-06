using UnityEngine;
using System.Collections;

public class GlobalVariables : MonoBehaviour {

	public static int Money=0;
	public static int HP=0;
	public static int TurretCost=1000;
	public static int UpgradeCost=1000; 
	public static GameObject CurrentTurret;
    public static GameObject PlaceHolderTurret;
    public static GameObject CurrentTurretPlace;
	public static Transform FinishTarget;
    public static int CurrentTurretLevel=1000; 
	public static bool DestroyTurret=false;
	public static int CurrentWave=0;
	public static int LastLevel=7;
	public static bool LevelCleared=false; 
	public static bool LevelFailed=false; 
	public static int TargetFPS=30;
	public static bool GameStarted = false;
	public static float GameSpeed=1;
	public static int CurrentBullets = 0;
	public static int MaximunBullets=50;
	public static int MaxRevives=1;// 0 also counts
	public static int Revives=1;
	public static int HPReward=5;
	public static bool ApplyRewards = false;
	public static string TurretTag="Turret";
	public static string FinishTargetTag="Finish";
	public static string CreepTag="CreepG";
	public static string CreepPathCheckerTag="PathChecker";
	public static string CreepRenderTag="CreepRender";
	public static string TurretBuildingTag="TurretBuildingArea";
	public static string MainCameraTag = "MainCamera";


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
