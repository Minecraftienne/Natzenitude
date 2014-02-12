using UnityEngine;
using System.Collections;

public class Stock : MonoBehaviour {


	public float originalWidth;  // define here the original resolution
	public float originalHeight; // you used to create the GUI contents 
	private Vector3 scale;

	void Start(){
		originalWidth = 1280.0f;
		originalHeight = 800.0f;
	}
	
	void Update(){
	}

	public void OnGUI() {
		
		scale.x = Screen.width/originalWidth; // calculate hor scale
		scale.y = Screen.height/originalHeight; // calculate vert scale
		scale.z = 1;



		var svMat = GUI.matrix; // save current matrix
		// substitute matrix - only scale is altered from standard
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
		
		// Barre défilante en haut
		GUI.Box(new Rect(-5, 0, 1685, 50), "");
		
		// Jours
		GUI.Label(new Rect(560, 100, 100, 20),"Jour : "+ Placard.jour); // affiche le nombre de jours passés
		
		// Menu à gauche
		GUI.Box(new Rect(240, 160, 130, 500), "");
		
		GUI.Button(new Rect(250, 190, 110, 30), "Matériel");
		GUI.Button(new Rect(250, 230, 110, 30), "Produits");
		GUI.Button(new Rect(250, 270, 110, 30), "Outils");
		
		if (GUI.Button(new Rect(250, 330, 110, 30), "Boutique")) {			
			Application.LoadLevel ("Boutique");
		}
		
		if (GUI.Button(new Rect(250, 610, 110, 30), "Retour")) {			
			Application.LoadLevel ("Placard");
		}
		
		// Affichage au centre
		GUI.Box(new Rect(400, 160, 700, 500), "");
		
		// restore matrix before returning
		GUI.matrix = svMat; // restore matrix
	}

}