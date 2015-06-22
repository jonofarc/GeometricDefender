using UnityEngine;
using System.Collections;
using System;
namespace UnityStandardAssets.Characters.ThirdPerson
{
[RequireComponent(typeof (NavMeshAgent))]
[RequireComponent(typeof (ThirdPersonCharacter))]
public class CheckPath : MonoBehaviour {
	
	public Transform target;
	public float PathCheckInterval=1f;
	private NavMeshPath path;
	private float elapsed = 0.0f; 
	private GameObject LatestTurret;
	
		public NavMeshAgent agent { get; private set; } // the navmesh agent required for the path finding
		public ThirdPersonCharacter character { get; private set; } // the character we are controlling

	
	void Start () {
		path = new NavMeshPath();
		elapsed = 0.0f;

			// get the components on the object we need ( should not be null due to require component so no need to check )
			agent = GetComponentInChildren<NavMeshAgent>();
			character = GetComponent<ThirdPersonCharacter>();
			
			agent.updateRotation = false;
			agent.updatePosition = true;
		
	}
	void Update () {

		// Update the way to the goal every second.
		elapsed += Time.deltaTime;
		if (elapsed > PathCheckInterval) {
			elapsed -= PathCheckInterval;
			//Debug.Log(NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path));
			//ReCheckPath();
			NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
			
		}
		for (int i = 0; i < path.corners.Length-1; i++) {
			
			Debug.DrawLine(path.corners[i], path.corners[i+1], Color.red);	
		}
	
		if(path.status.ToString()!="PathComplete"&&LatestTurret!=null){
			LatestTurret.SendMessage("ReverseTurret");
			/*
			GameObject[] creeps;
			creeps = GameObject.FindGameObjectsWithTag("Creeps");

			foreach (GameObject creep in creeps) {
				NavMeshAgent creepNav;
				creepNav = GetComponent<NavMeshAgent>();
				creepNav.SetDestination(target.position);
				NavMesh.CalculatePath(creep.transform.position, target.position, NavMesh.AllAreas, path);
				creepNav.SetPath(path);
			}
			*/
		}
		
			if (target != null)
			{
				agent.SetDestination(target.position);
				
				agent.SetPath(path);
				
				// use the values to move the character
				character.Move(agent.desiredVelocity, false, false);
			}
			else
			{
				// We still need to call the character's move function, but we send zeroed input as the move param.
			//	character.Move(Vector3.zero, false, false);
			}

	}
	
	void SetLatestTurret (GameObject RecivedTurret){
		LatestTurret = RecivedTurret;
		elapsed += PathCheckInterval;
	
	}
	
	
	
	
}
}
