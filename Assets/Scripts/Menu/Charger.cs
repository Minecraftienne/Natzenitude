using UnityEngine;
using System.Collections;

public class Charger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		
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
