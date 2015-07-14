using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetGold : MonoBehaviour {

	public Text GoldText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GoldText.text = "Oro: " + MoneyCount.Money.ToString ();
	}
}
