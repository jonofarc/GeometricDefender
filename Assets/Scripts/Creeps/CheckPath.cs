﻿using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]

public class CheckPath : MonoBehaviour {
	
	public Transform target;
	public float PathCheckInterval=1f; 
	private UnityEngine.AI.NavMeshPath path;
	private float elapsed = 0.0f;  
	private float elapsed2 = 0.0f;
	public GameObject[] LatestTurret; 
	private int LatestTurretNumber=0;
	private bool doSetPath = true;
	 
	public UnityEngine.AI.NavMeshAgent agent { get; private set; } // the navmesh agent required for the path finding
		

	
	void Start () { 
		LatestTurret = new GameObject[10];
		path = new UnityEngine.AI.NavMeshPath();
		elapsed = 0.0f;

			// get the components on the object we need ( should not be null due to require component so no need to check )
			agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
			
			
			agent.updateRotation = true;
			agent.updatePosition = true;
		InvokeRepeating ("PathCheck",PathCheckInterval,PathCheckInterval);
		//Invoke ("PathCheck",PathCheckInterval);
		
	}
	void FixedUpdate () {
		for (int i = 0; i < path.corners.Length-1; i++) {

			Debug.DrawLine(path.corners[i], path.corners[i+1], Color.red);	
		}
	}
	void PathCheck(){


		UnityEngine.AI.NavMesh.CalculatePath(transform.position, target.position, UnityEngine.AI.NavMesh.AllAreas, path);
		for (int i = 0; i < path.corners.Length-1; i++) {

			Debug.DrawLine(path.corners[i], path.corners[i+1], Color.red);	
		}

		if(path.status.ToString()!="PathComplete"&&LatestTurret[0]!=null){
			Debug.Log ("am quitando");

			Debug.Log(LatestTurret[0].transform.gameObject.name);
			Debug.Log(LatestTurret[0].transform.GetChild(0).gameObject);


			//LatestTurret[0].transform.gameObject.GetComponent<Collider>().enabled=true;
			//Destroy(LatestTurret[0].transform.GetChild(0).gameObject);
			//DecreaseLatestTurret();

			//GlobalVariables.DestroyTurret=true; 
			SelectTurret mySelectTurret = LatestTurret[0].transform.GetChild(0).gameObject.GetComponent<SelectTurret>();
			if (GlobalVariables.GameStarted) {
				mySelectTurret.DestroyTurret (0.5f);
			} else {
				mySelectTurret.DestroyTurret (1.0f);
			}

			//GlobalVariables.DestroyTurret=false; 
		//	Invoke("ReSetCreepsPath",Time.deltaTime);




		}
		/*
		if (target != null && doSetPath)
		{
			agent.SetDestination(target.position);

			agent.SetPath(path);


		}
		*/
		else
		{
			// We still need to call the character's move function, but we send zeroed input as the move param.
			//	character.Move(Vector3.zero, false, false);
		}


	



	}
	void ReSetCreepsPathRequest (){

		Invoke("ReSetCreepsPathExecution",0.1f);

	}
	void ReSetCreepsPathExecution (){

		GameObject[] CurrentCreeps= GameObject.FindGameObjectsWithTag(GlobalVariables.CreepTag);
		foreach (GameObject creep in CurrentCreeps) {
			Debug.Log ("REquesting patchcheck from Checkpath");
			creep.SendMessage ("PathCheck");
		}

	}
	
	void SetLatestTurret (GameObject RecivedTurret){


		ReArrangeLatestTurret();
		LatestTurret[0] = RecivedTurret;
		elapsed += PathCheckInterval;
		if(LatestTurretNumber+1<LatestTurret.Length){
			LatestTurretNumber++;
		}
		else{

		}

	
	}
	void ReArrangeLatestTurret (){

		for(int i=LatestTurretNumber; i>0; i--){

			if(LatestTurret[i-1]!=null){
				LatestTurret[i] = LatestTurret[i-1];
			}

		}


	}
	void DecreaseLatestTurret(){
		LatestTurret[LatestTurretNumber] = null;
		for(int i=0; i<LatestTurretNumber-1; i++){

			if(LatestTurret[i+1]!=null){
				LatestTurret[i] = LatestTurret[i+1];
			}
			
		}
		LatestTurretNumber--;

	}

	void OnDisable()
	{
		doSetPath = false;
	}
	
	
	
}

