using UnityEngine;
using System.Collections;

public class Aide : MonoBehaviour {

	#region Variables
	public float originalWidth = 1280.0f;  // define here the original resolution
	public float originalHeight = 800.0f; // you used to create the GUI contents 
	private Vector3 scale;

	// Scrolling View
	private Vector2 scrollViewVector = Vector2.zero;

	private bool activeLabelBut = false, activeLabelArgent = false, activeLabelSucces = false, activeLabelObjectifs = false,
	activeLabelChallenges = false;

	// Les succès sont faux au début du jeu
	public bool succes365 = false, succesRichesse = false, succesTueurPlantes = false, succesInsectes = false,
	succcesMiniJeux = false, succesMalchanceux = false, succesTropGourmand = false, succesCollectionneur = false,
	succesActionReaction = false, succesFiasco = false;

	// Images des succès
	public Texture2D miniature365, miniatureRichesse, miniatureTueurPlantes, miniatureInsectes, miniatureMiniJeux,
	miniatureMalchanceux, miniatureTropGourmand, miniatureCollectionneur, miniatureActionReaction, miniatureFiasco;

	// Son
	public AudioClip succesReussi;
	#endregion

	void Start() {
		originalWidth = 1280.0f;
		originalHeight = 800.0f;
	}
	
	// Update is called once per frame
	void Update() {

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
		GUI.Label(new Rect(560, 100, 100, 20),"Jour : "+Placard.jour); // affiche le nombre de jours passés
		
		// Menu à gauche
		GUI.Box(new Rect(240, 160, 140, 500), "");

		if (GUI.Button (new Rect (250, 190, 120, 30), "But du jeu")) {

			activeLabelBut = !activeLabelBut;
			activeLabelArgent = false;
			activeLabelSucces = false;
			activeLabelObjectifs = false;
			activeLabelChallenges = false;
		}

		if (activeLabelBut) {

			GUI.Label (new Rect (450, 190, 600, 50), "Le but de Natzenitude est que vous preniez soin de votre plante jusqu'à ce qu'elle soit arrivée" +
			" à son dernier stade d'évolution, où vous pourrez alors la vendre et gagner de l'argent en fonction" +
			" de différents critères ; ensuite vous aurez la possibilité d'en rachetez d'autres via la Boutique.");
		}
	
		if (GUI.Button (new Rect (250, 230, 120, 30), "Gagner de l'argent")) {

			activeLabelArgent = !activeLabelArgent;
			activeLabelBut = false;
			activeLabelSucces = false;
			activeLabelObjectifs = false;
			activeLabelChallenges = false;
		}

		if (activeLabelArgent) {

			GUI.Label (new Rect (450, 190, 600, 180), "Pour gagner de l'argent il faut vendre votre plante une fois qu'elle a atteint son dernier stade d'évolution." +
			"\n\nLe prix est déterminé par plusieurs critères : " +
			"\n\n• la qualité => moins la plante aura subi de dommages (trop d'engrais, pas assez d'arrosage...), meilleur prix vous pourrez en tirez." +
			"\n• la quantité => correspond au rendement au m² par rapport à la puissance de lampe, plus vous produisez plus vous vendez." +
			"\n• la rapidité => en évitant les malus et retards de croissance, et en obtenant des bonus de croissance, la plante grandira plus vite et vous gagnerez plus d'argent.");
		}

		#region Succès
		if (GUI.Button (new Rect (250, 270, 120, 30), "Succès")) {

			activeLabelSucces = !activeLabelSucces;
			activeLabelArgent = false;
			activeLabelBut = false;
			activeLabelObjectifs = false;
			activeLabelChallenges = false;
		}

		if (activeLabelSucces) {

			// Début de la ScrollView
			scrollViewVector = GUI.BeginScrollView (new Rect (0, 0, 1110, 900), scrollViewVector, new Rect (0, 0, 1000, 1400));

			// Affichage des succès (image, titre, description)
			GUI.DrawTexture(new Rect(430, 190, 100, 100), miniature365, ScaleMode.ScaleToFit, true);
			GUI.Label (new Rect (460, 180, 100, 20), "1 an");
			GUI.Label (new Rect (560, 220, 200, 20), "Atteindre 365 jours.");

			GUI.DrawTexture(new Rect(430, 300, 100, 100), miniatureRichesse, ScaleMode.ScaleToFit, true);
			GUI.Label (new Rect (450, 290, 100, 20), "Richesse");
			GUI.Label (new Rect (560, 330, 200, 20), "Avoir 10 000 euros.");

			GUI.DrawTexture(new Rect(430, 410, 100, 100), miniatureTueurPlantes, ScaleMode.ScaleToFit, true);
			GUI.Label (new Rect (440, 400, 100, 20), "Tueur de plantes");
			GUI.Label (new Rect (560, 440, 300, 20), "Tuer sa plante (peu importe la cause).");

			GUI.DrawTexture(new Rect(430, 520, 100, 100), miniatureInsectes, ScaleMode.ScaleToFit, true);
			GUI.Label (new Rect (430, 510, 150, 20), "La folie des insectes");
			GUI.Label (new Rect (560, 550, 400, 20), "Voir sa plante se faire attaquée par des insectes.");

			GUI.DrawTexture(new Rect(430, 630, 100, 100), miniatureMiniJeux, ScaleMode.ScaleToFit, true);
			GUI.Label (new Rect (460, 620, 100, 20), "Je gère");
			GUI.Label (new Rect (560, 660, 300, 20), "Réussir tous les mini-jeux.");

			GUI.DrawTexture(new Rect(430, 740, 100, 100), miniatureMalchanceux, ScaleMode.ScaleToFit, true);
			GUI.Label (new Rect (440, 730, 100, 20), "Malchanceux");
			GUI.Label (new Rect (560, 770, 500, 20), "En une seule partie avoir été victime de tous les incidents possibles.");
			
			GUI.DrawTexture(new Rect(430, 850, 100, 100), miniatureTropGourmand, ScaleMode.ScaleToFit, true);
			GUI.Label (new Rect (440, 840, 100, 20), "Trop gourmand");
			GUI.Label (new Rect (560, 880, 400, 20), "Tuer sa plante en lui donnant trop d'engrais.");

			GUI.DrawTexture(new Rect(430, 960, 100, 100), miniatureCollectionneur, ScaleMode.ScaleToFit, true);
			GUI.Label (new Rect (440, 950, 100, 20), "Collectionneur");
			GUI.Label (new Rect (560, 990, 500, 20), "Acheter tout ce que la boutique offre (matériel, produits...).");

			GUI.DrawTexture(new Rect(430, 1070, 100, 100), miniatureActionReaction, ScaleMode.ScaleToFit, true);
			GUI.Label (new Rect (440, 1060, 100, 20), "Action/Réaction");
			GUI.Label (new Rect (560, 1100, 400, 20), "Résoudre en une seule journée un incident.");

			GUI.DrawTexture(new Rect(430, 1180, 100, 100), miniatureFiasco, ScaleMode.ScaleToFit, true);
			GUI.Label (new Rect (460, 1170, 100, 20), "Fiasco");
			GUI.Label (new Rect (560, 1210, 400, 20), "Ne plus avoir d'argent ni de plante en vie.");

			GUI.EndScrollView();

			// Vérification pour voir si les succès ont été accomplis
			if (Placard.jour >= 365) {

				succes365 = true;
				audio.PlayOneShot(succesReussi);
				GUI.Box (new Rect (490, 10, 250, 80), "");
				GUI.DrawTexture(new Rect(500, 0, 100, 100), miniature365, ScaleMode.ScaleToFit, true);
				GUI.Label (new Rect (610, 40, 200, 20), "Succès 1 an réussi");
			}

			if (Boutique.argent >= 10000) {

				succesRichesse = true;
				audio.PlayOneShot(succesReussi);
				GUI.Box (new Rect (490, 10, 250, 80), "");
				GUI.DrawTexture(new Rect(500, 0, 100, 100), miniatureRichesse, ScaleMode.ScaleToFit, true);
				GUI.Label (new Rect (610, 40, 200, 20), "Succès Richesse réussi");
			}

		/* if (Plante.etat.morte == true), alors succesTueurPlantes = true;

		   if (Plante.Incidents.incidentInsectes == 1), alors succesInsectes = true;

		   if (MiniJeux.nom1 == true && MiniJeux.nom2 == true && etc), alors succesMiniJeux = true;
		   le mini-jeux devient true seulement s'il a déjà été réussi une fois?

		   if (??), alors succesMalchanceux = true;
		   peut etre faire un compteur d'incidents, exemple on part de 0 et à chaque fois qu'on a un incident d'un tel
		   type on augmente de 1, ainsi si incidentVentilation on augmente de 1 un int (compteurIncidentVentilation)
		   si compteurIncidentVentilation >= à 1 && compteurIncidentInsectes >= 1 etc... alors le succès est vrai

		   if (Plante.etat.morte == true && engraisTrop && Plante.Incidents.incidentEngrais == 1),
		   alors succesTropGourmand = true;

		   if (?), alors succesCollectionneur = true;

	       if (?), alors succesActionReaction = true;

		   if (Boutique.argent == 0 && Plante.etat.morte == true), alors succesFiasco = true; */
		}
		#endregion

		if (GUI.Button (new Rect (250, 310, 120, 30), "Objectifs")) {

			activeLabelObjectifs = !activeLabelObjectifs;
			activeLabelArgent = false;
			activeLabelSucces = false;
			activeLabelBut = false;
			activeLabelChallenges = false;
		}

		if (activeLabelObjectifs) {

			GUI.Label (new Rect (450, 190, 600, 50), "Les objectifs journaliers sont facultatifs mais sachez que si vous les accomplissez," +
		    " la plante obtient un bonus de croissance le lendemain, tandis que si vous ne les réalisez pas," +
		    " la plante peut obtenir un malus de croissance le lendemain.");
		}

		if (GUI.Button (new Rect (250, 350, 120, 30), "Challenges")) {

			activeLabelChallenges = !activeLabelChallenges;
			activeLabelArgent = false;
			activeLabelSucces = false;
			activeLabelObjectifs = false;
			activeLabelBut = false;
		}

		if (activeLabelChallenges) {

			GUI.Label (new Rect (450, 190, 600, 80), "Ce sont des opportunités qui apparaissent aléatoirement dans la petite fenêtre de la boutique." +
			" Un vendeur apparaît dans le cadre et vous propose un mini-jeu que vous êtes libre d'accepter ou de refuser." +
			" Si vous réussissez le mini-jeu, le vendeur peut soit vous faire une remise sur un objet de la boutique au choix," +
	        " soit vous donnez de l'argent, ou soit vous offrir un objet de la boutique au hasard, c'est aléatoire.");
		}
		
		if (GUI.Button(new Rect(250, 610, 120, 30), "Retour")) {
			
			Application.LoadLevel ("Placard");
		}
		
		// Affichage au centre
		GUI.Box(new Rect(410, 160, 700, 500), "");
		
		// restore matrix before returning
		GUI.matrix = svMat; // restore matrix
	}

}
