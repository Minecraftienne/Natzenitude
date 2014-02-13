using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour {

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
		this.renderer.material.color = Color.black;
	}
	
	void OnMouseExit ()
	{
		this.renderer.material.color = Color.white;
	}
}
