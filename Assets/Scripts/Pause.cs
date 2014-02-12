using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

	public bool pauseVisible = false;
	public GUIStyle customBox = null;
	
	private Vector3 scale;
	public float originalWidth;  // define here the original resolution
	public float originalHeight; // you used to create the GUI contents 
	
	void Start(){
		originalWidth = 1280.0f;
		originalHeight = 800.0f;
	}
	
	// Update is called once per frame
	void Update(){

		// si le joueur appuie sur échap
		if (Input.GetButtonDown("Pause")) {
			
			// et si le temps se déroule
			if (Time.timeScale == 1.0f) {
				
				// alors le temps s'arrete et le menu pause apparait
				Time.timeScale = 0;
				pauseVisible = true;
			}
			
			else {
				Time.timeScale = 1.0f;
				pauseVisible = false;
			}
		}	
	}

	void OnGUI() {
		
		scale.x = Screen.width/originalWidth; // calculate hor scale
		scale.y = Screen.height/originalHeight; // calculate vert scale
		scale.z = 1;
		
		var svMat = GUI.matrix; // save current matrix
		// substitute matrix - only scale is altered from standard
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
		
		if (pauseVisible == true) {
			
			GUI.Box(new Rect(480, 230, 200, 350), "", customBox);
			
			// équivalent échap
			if (GUI.Button(new Rect(520, 240, 130, 50), "Reprendre le jeu")) {
				
				Time.timeScale = 1.0f;
				pauseVisible = false;
			}
			
			GUI.Button(new Rect(520, 310, 130, 50), "Sauvegarder");
			GUI.Button(new Rect(520, 380, 130, 50), "Charger");
			GUI.Button(new Rect(520, 450, 130, 50), "Options");
			
			if (GUI.Button(new Rect(520, 520, 130, 50), "Quitter")) {
				
				Application.Quit();
			}
		}
		// restore matrix before returning
		GUI.matrix = svMat; // restore matrix
	}
}
