using UnityEngine;
using System.Collections;

public class GUIPlacard : MonoBehaviour {
	
	public static int jour;
	private Vector3 scale;
	public float originalWidth;  // define here the original resolution
	public float originalHeight; // you used to create the GUI contents 
		
	public GUIStyle customBoutonPause ;
	public GUIStyle customBoutonNormal ;
	public GUIStyle customBoutonAccelerer ;	
	public GUIStyle customBoutonPause2 ;
	
	private bool boutonPause = false;

	// Use this for initialization
	void Start () {
		InvokeRepeating("Journees", 10, 10);
		originalWidth = 1280.0f;
		originalHeight = 800.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void Journees () {
		
		jour++;
		
	}
	
	void OnGUI() {


		scale.x = Screen.width/originalWidth; // calculate hor scale
		scale.y = Screen.height/originalHeight; // calculate vert scale
		scale.z = 1;
		
		Matrix4x4 svMat = GUI.matrix; // save current matrix
		// substitute matrix - only scale is altered from standard
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
		
		/* --------------------------------------------------------------------- */

		// Barre défilante en haut
		GUI.Box(new Rect(-5, 0, 1685, 50), "");
		
		// Jours
		GUI.Label(new Rect(560, 100, 100, 20),"Jour : "+jour); // affiche le nombre de jours passés
		
		/* --------------------------------------------------------------------- */

		if (boutonPause == false) {
			
			if (GUI.Button(new Rect(620, 90, 40, 40), "", customBoutonPause)) {
				
				boutonPause = true;
				Time.timeScale = 0;
			}
		}
		
		if (boutonPause == true) {
			
			if (GUI.Button(new Rect(620, 90, 40, 40), "", customBoutonPause2)) {
				
				boutonPause = false;
				Time.timeScale = 1;
			}
		}
		
		// Bouton temps normal
		if (GUI.Button(new Rect(660, 90, 40, 40), "", customBoutonNormal)) {
			
			boutonPause = false;
			Time.timeScale = 1;
		}
		
		// Bouton accélérer
		if (GUI.Button(new Rect(700, 90, 40, 40), "", customBoutonAccelerer)) {
			
			boutonPause = false;
			Time.timeScale = 1;
			jour++;
		}
		
		/* --------------------------------------------------------------------- */

		// Menu à gauche
		if (GUI.Button(new Rect(240, 210, 110, 30), "Carnet de notes")) {
			
			Application.LoadLevel ("Carnetdenotes");
		}
		
		if (GUI.Button(new Rect(240, 260, 110, 30), "Entretien")) {
			
			Application.LoadLevel ("Entretien");
		}
		
		if (GUI.Button(new Rect(240, 310, 110, 30), "Stock")) {
			
			Application.LoadLevel ("Stock");
		}
		
		if (GUI.Button(new Rect(240, 360, 110, 30), "Boutique")) {
			
			Application.LoadLevel ("Boutique");
		}
		
		/* --------------------------------------------------------------------- */

		// Au centre le placard
		GUI.Box(new Rect(400, 150, 360, 480), "");
		
		// Le magasin en haut à droite
		GUI.Box(new Rect(800, 150, 300, 350), "");
		
		//GUI.Label(new Rect(810, 155, 100, 20),"Argent : "+Boutique.argent); // affiche l'argent du joueur
		
		// Menu à droite en bas du magasin
		if (GUI.Button(new Rect(900, 540, 110, 30), "Aide")) {
			
			Application.LoadLevel ("Aide");
		}
		
		GUI.Button(new Rect(900, 590, 110, 30), "Options");
		
		/* --------------------------------------------------------------------- */
		
		// restore matrix before returning
		//GUI.matrix = svMat; // restore matrix
	}
}
