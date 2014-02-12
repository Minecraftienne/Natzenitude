using UnityEngine;
using System.Collections;

public class Plante : MonoBehaviour {


	// Influe température
	public float temperatureExt;
	public float temperatureInt;
	public int taillePlacard;
	public bool aeration;
	
	// Arrosage
	public bool arrosage;
	public bool arrosageTrop;
	public bool arrosagePasAssez;
	
	// Engrais
	public bool engrais;
	public bool engraisTrop;
	public bool engraisPasAssez;
	
	// Lampe
	public bool lampe;
	public bool lampeProche;
	public bool lampeEloignee;


	// Images des plantes
	public Texture2D plantePlantation;
	
	public Texture2D planteNoireE1;
	public Texture2D planteNoireE2;
	public Texture2D planteNoireE3;
	public Texture2D planteNoireE4;
	
	public Texture2D planteJauneE1;
	public Texture2D planteJauneE2;
	public Texture2D planteJauneE3;
	public Texture2D planteJauneE4;
	
	public Texture2D planteVerteE1;
	public Texture2D planteVerteE2;
	public Texture2D planteVerteE3;
	public Texture2D planteVerteE4;
	
	public Texture2D planteVerteClaireE1;
	public Texture2D planteVerteClaireE2;
	public Texture2D planteVerteClaireE3;
	public Texture2D planteVerteClaireE4;

	// Use this for initialization
	void Start () {
		// A partir du jour 5 (60 secondes = 5 jours car on commence à 0), un incident random tous les 1 jour
		InvokeRepeating("Incidents", 60, 10);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	

	

	void Incidents () {
		
		int incidentVentilation = Random.Range(1, 11); // 1 à 10
		int incidentArrosage = Random.Range(1, 6); // 1 à 5
		int incidentEngrais = Random.Range(1, 11); // 1 à 10
		int incidentLampe = Random.Range(1, 6); // 1 à 5
		int incidentInsectes = Random.Range(1, 26); // 1 à 25
		
		if (incidentVentilation == 1) {
			
			print("incident 1 : mauvaise gestion de la ventilation");
		}
		
		else if (incidentInsectes == 1) {
			
			print("incident 2 : invasion d'insectes");
		}
		
		else if (arrosageTrop && incidentArrosage == 1) {
			
			print("incident 3 : trop d'arrosage");
		}
		
		else if (arrosagePasAssez && incidentArrosage == 1) {
			
			print("incident 4 : manque d'arrosage");
		}
		
		else if (engraisTrop && incidentEngrais == 1) {
			
			print("incident 5 : trop d'engrais");
		}
		
		else if (engraisPasAssez && incidentEngrais == 1) {
			
			print("incident 6 : manque d'engrais");
		}
		
		else if (lampeProche && incidentLampe == 1) {
			
			print("incident 7 : lampe trop proche");
		}
		
		else if (lampeEloignee && incidentLampe == 1) {
			
			print("incident 8 : lampe trop éloignée");
		}
	}
	
	//function Arroser() {
	
	//}
	
	float originalWidth = 1280.0f;  // define here the original resolution
	float originalHeight = 800.0f; // you used to create the GUI contents 
	private Vector3 scale;
	
	void OnGUI() {
		
		scale.x = Screen.width/originalWidth; // calculate hor scale
		scale.y = Screen.height/originalHeight; // calculate vert scale
		scale.z = 1;
		
		var svMat = GUI.matrix; // save current matrix
		// substitute matrix - only scale is altered from standard
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
		
		/* --------------------------------------------------------------------- */
		
		// si le joueur arrose trop, la plante moisit (noir)
		if (arrosageTrop && Placard.jour >= 7 && Placard.jour <14) {
			
			GUI.DrawTexture(new Rect(534, 545, 100, 100), planteNoireE1, ScaleMode.ScaleToFit, true);
		}
		
		// si le joueur n'arrose pas assez, la plante sèche (jaune)
		else if (arrosagePasAssez && Placard.jour >= 7 && Placard.jour <14) {
			
			GUI.DrawTexture(new Rect(534, 545, 100, 100), planteJauneE1, ScaleMode.ScaleToFit, true);
		}
		
		if (arrosageTrop && Placard.jour >= 14 && Placard.jour <21) {
			
			GUI.DrawTexture(new Rect(510, 480, 150, 150), planteNoireE2, ScaleMode.ScaleToFit, true);
		}
		
		else if (arrosagePasAssez && Placard.jour >= 14 && Placard.jour <21) {
			
			GUI.DrawTexture(new Rect(510, 480, 150, 150), planteJauneE2, ScaleMode.ScaleToFit, true);
		}
		
		if (arrosageTrop && Placard.jour >= 21 && Placard.jour <28) {
			
			GUI.DrawTexture(new Rect(500, 455, 175, 175), planteNoireE3, ScaleMode.ScaleToFit, true);
		}
		
		else if (arrosagePasAssez && Placard.jour >= 21 && Placard.jour <28) {
			
			GUI.DrawTexture(new Rect(500, 455, 175, 175), planteJauneE3, ScaleMode.ScaleToFit, true);
		}
		
		if (arrosageTrop && Placard.jour >= 28) {
			
			GUI.DrawTexture(new Rect(490, 430, 200, 200), planteNoireE4, ScaleMode.ScaleToFit, true);
		}
		
		else if (arrosagePasAssez && Placard.jour >= 28) {
			
			GUI.DrawTexture(new Rect(490, 430, 200, 200), planteJauneE4, ScaleMode.ScaleToFit, true);
		}
		
		/* --------------------------------------------------------------------- */
		
		if (Placard.jour >= 0 && Placard.jour <7) {
			
			GUI.DrawTexture(new Rect(530, 531, 100, 100), plantePlantation, ScaleMode.ScaleToFit, true);
		}
		
		if (Placard.jour >= 7 && Placard.jour <14) {
			
			GUI.DrawTexture(new Rect(534, 545, 100, 100), planteVerteE1, ScaleMode.ScaleToFit, true);
		}
		
		if (Placard.jour >= 14 && Placard.jour <21) {
			
			GUI.DrawTexture(new Rect(510, 480, 150, 150), planteVerteE2, ScaleMode.ScaleToFit, true);
		}
		
		if (Placard.jour >= 21 && Placard.jour <28) {
			
			GUI.DrawTexture(new Rect(500, 455, 175, 175), planteVerteE3, ScaleMode.ScaleToFit, true);
		}
		
		if (Placard.jour >= 28) {
			
			GUI.DrawTexture(new Rect(490, 430, 200, 200), planteVerteE4, ScaleMode.ScaleToFit, true);
		}
		
		/* --------------------------------------------------------------------- */
		
		// restore matrix before returning
		GUI.matrix = svMat; // restore matrix
	}
}
