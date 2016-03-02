using UnityEngine;
using System.Collections;

public class Quitter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		Application.Quit ();
	}

	void OnMouseEnter ()
	{
		this.GetComponent<Renderer>().material.color = Color.black;
	}

	void OnMouseExit ()
	{
		this.GetComponent<Renderer>().material.color = Color.white;
	}
}
