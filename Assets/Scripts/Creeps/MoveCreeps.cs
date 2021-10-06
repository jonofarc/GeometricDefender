using UnityEngine;
using System.Collections;
using System;


[RequireComponent (typeof(UnityEngine.AI.NavMeshAgent))]
public class MoveCreeps : MonoBehaviour
{
	public Transform target;
	private UnityEngine.AI.NavMeshPath path;

	public UnityEngine.AI.NavMeshAgent agent { get; private set; }
	// the navmesh agent required for the path finding

	private float CreepSpeed = 0;
	private float AngularSpeed = 0;
	private float AccelerationSpeed = 0;
	private bool doSetPath = true;
	private bool InitialisationComplete = false;
	 
	// Use this for initialization
	void Start ()
	{
		
		if (InitialisationComplete == false) {
			Initialisation ();
		}


	}

	public void Initialisation ()
	{
		if (GlobalVariables.FinishTarget == null) {
			target = GameObject.FindGameObjectWithTag (GlobalVariables.FinishTargetTag).transform;
			GlobalVariables.FinishTarget = target;	
		} else {
			target = GlobalVariables.FinishTarget;
		}
		//Debug.Log (GlobalVariables.FinishTarget.name);
		path = new UnityEngine.AI.NavMeshPath ();

		// get the components on the object we need ( should not be null due to require component so no need to check )
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();

		//get original NavMeshAgent Values
		CreepSpeed = agent.speed;
		AngularSpeed = agent.angularSpeed;
		AccelerationSpeed = agent.acceleration;


		agent.updateRotation = true;
		agent.updatePosition = true;
		PathCheck ();
		InitialisationComplete = true;


		//Debug.Log ("Initialisation Complete");
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		
		for (int i = 0; i < path.corners.Length - 1; i++) {

			Debug.DrawLine (path.corners [i], path.corners [i + 1], Color.red);	
		}

	}
//end update
	void PathCheck ()
	{
		
		UnityEngine.AI.NavMesh.CalculatePath (transform.position, target.position, UnityEngine.AI.NavMesh.AllAreas, path);

		
		for (int i = 0; i < path.corners.Length - 1; i++) {

			Debug.DrawLine (path.corners [i], path.corners [i + 1], Color.red);	
		}

	
		if (target != null && doSetPath) {
			agent.SetDestination (target.position);
		
			agent.SetPath (path);
		
		
		} 

		



	}


	public void CancelPathCheck ()
	{
		CancelInvoke ("PathCheck");
	}

	void OnDisable ()
	{
		doSetPath = false;
	}

}
 