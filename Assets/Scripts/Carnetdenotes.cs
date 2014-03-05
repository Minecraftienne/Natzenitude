using UnityEngine;
using System.Collections;

public class Carnetdenotes : MonoBehaviour {
			
	public float originalWidth = 1280.0f;  // define here the original resolution
	public float originalHeight = 800.0f; // you used to create the GUI contents 
	private Vector3 scale;

	void Start(){
		originalWidth = 1280.0f;
		originalHeight = 800.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		
		scale.x = Screen.width/originalWidth; // calculate hor scale
		scale.y = Screen.height/originalHeight; // calculate vert scale
		scale.z = 1;
		
		var svMat = GUI.matrix; // save current matrix
		// substitute matrix - only scale is altered from standard
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
		
		// Barre défilante en haut
		GUI.Box(new Rect(-5, 0, 1685, 50), "");
		
		// Jours
		GUI.Label(new Rect(560, 100, 100, 20),"Jour : "+GUIPlacard.jour); // affiche le nombre de jours passés
		
		// Menu à gauche
		GUI.Box(new Rect(240, 160, 140, 500), "");
		
		GUI.Button(new Rect(250, 190, 120, 30), "Dernières infos");
		GUI.Button(new Rect(250, 230, 120, 30), "Objectifs en cours");
		GUI.Button(new Rect(250, 270, 120, 30), "Plan d'évolution");
		GUI.Button(new Rect(250, 310, 120, 30), "Mes notes");
		
		if (GUI.Button(new Rect(250, 610, 120, 30), "Retour")) {
			
			Application.LoadLevel ("Placard");
			
		}
		
		// Affichage au centre
		GUI.Box(new Rect(410, 160, 700, 500), "");
		
		// restore matrix before returning
		GUI.matrix = svMat; // restore matrix
	}

}
