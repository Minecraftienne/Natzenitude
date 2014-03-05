using UnityEngine;
using System.Collections;

public class NouvellePartie : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {		
		Application.LoadLevel ("Placard");
	}

	void OnMouseEnter ()
	{
		this.renderer.material.color = Color.black;
	}
	
	void OnMouseExit ()
	{
		this.renderer.material.color = Color.white;
	}
}
