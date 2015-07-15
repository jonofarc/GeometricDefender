using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameGUI : MonoBehaviour {

	public Text InfoText;
	public GameObject CannonTurret;
	public GameObject MachineGunTurret;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI()
	{

	

	}
	public void FastFoward(){
		/*if(){
			Time.timeScale = 2.0F;
		}
		else{
			Time.timeScale = 1.0F;
		}*/
	}
	public void MainMenuButtton (){
		Application.LoadLevel("MainMenu");

	}
	public void CannonTurretButton(){

		GlobalVariables.DestroyTurret=false;
		GlobalVariables.CurrentTurret = CannonTurret;
		SelectTurret cost = (SelectTurret) CannonTurret.GetComponent(typeof(SelectTurret));
		GlobalVariables.TurretCost = cost.getTurretCost();
		InfoText.text = "Costo Torre: "+GlobalVariables.TurretCost.ToString ();

	}
	public void MachineGunTurretButton(){
		GlobalVariables.DestroyTurret=false;
		GlobalVariables.CurrentTurret = MachineGunTurret;
		SelectTurret cost = (SelectTurret) MachineGunTurret.GetComponent(typeof(SelectTurret));
		GlobalVariables.TurretCost = cost.getTurretCost();
		InfoText.text = "Costo Torre: "+GlobalVariables.TurretCost.ToString ();

	}
	public void DestroyTurretButton(){
		GlobalVariables.DestroyTurret=true; 
		InfoText.text = "Selecciona torre a destruir (se regresara la mitad del costo de la torre)";
	}
}
