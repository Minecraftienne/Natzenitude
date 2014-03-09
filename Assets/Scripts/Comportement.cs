using UnityEngine;
using System.Collections;
using System;

public class Comportement : MonoBehaviour {
	
	public double hauteur = 0;
	public double largeur = 0;
	public double longueur = 0;
	
	float depart;
	public static int jour;
	public static Temperature temp;
	public static Humidite humi;
	public static Placard placard;
	public static Pot pot;
	public static Plante planteCourante;
	public static string menuChoisit;
	public static Joueur joueur;
	public static float distance;
	
	UltiManage ultiManage;
	float placardLargeur;
	float placardHauteur;
	float potLargeur;
	float potHauteur;
	float volume;
	
	// phase d'initialisation meme si l'objet n'est pas actif
	void Awake () {
		temp = new Temperature();
		humi = new Humidite(40);
		
		joueur = new Joueur("pseudodujoueur", 500);
		
		if (joueur != null) {

			planteCourante = joueur.plantes[0];
			pot = joueur.pots[0];
		}
		
		placard = new Placard(80, 300);
		
		planteCourante.definirTexture();
		
		jour = 0;
		
		potLargeur = pot.texturePot.width;
		potHauteur = pot.texturePot.height;
		placardLargeur = placard.largeur;
		placardHauteur = placard.hauteur;
		volume = (placardLargeur * placardHauteur) - (potLargeur * potHauteur);
		temp.init(volume, joueur.lampe.puissance);
		
		ultiManage = new UltiManage();
	}
	
	/*
     * Alors pour l'update
     * chaque seconde (countDown) on fait le calcul de la température avec temp.calcul
     * et on fait le calcul du niveau d'eau
     * Ensuite tous les 2 secondes on augmente le jour de 1, pour la plante si ca croissance = vrai,
     * le jour global est toujours incrémenté quoiqu'il arrive
     * chaque plante a son propre nombre jour et est actulisé meme si pas focus
     * A chaque frame on vérifie que pour chacune de ses plantes si ces états sont soignés ou si la plante recoit un etat
     * */

	float countdown = 1;
	float countdownJour = 2;

	// Update is called once per frame
	void Update () {
		
		countdown -= Time.deltaTime;
		countdownJour -= Time.deltaTime;
		
		if (countdown <= 0) {

			temp.up(joueur.lampe.puissance);
			temp.calcul();
			niveauEau();
			
			countdown = 1;
		}
		
		if (countdownJour <= 0) {

			passerJour();
			
			countdownJour = 2;
		}
		
		for (int i = 0; i < joueur.plantes.Count; i++) {

			joueur.plantes[i].definirTexture();

			for (int j = 0; j < joueur.plantes[i].listeEtat.Count; j++) {

				joueur.plantes[i].listeEtat[j].solution(joueur.plantes[i].listeEtat, joueur.plantes[i].listeEtat[j]);
			}
		}

		if (planteCourante != null) {

			ultiManage.etat();
		}
	}
	
	// chaque seconde on décrémente de 1 pour chacune des plantes du joueur
	public void niveauEau () {  

		foreach (Plante p in joueur.plantes) {

			if (p.niveauEau != 0) {

				p.niveauEau = p.niveauEau - 1;
			}

			else {

				p.niveauEau = 0;
			}
			
			p.updatePrix();
		}
	}
	
	// le jour d'une plante et de ces états sont incrémentés
    // un état peut avoir des effet au bout de n jours, si ce nombre = -1 ça veut dire que l'état prend acte tout de suite
	public void passerJour() {
	
		foreach (Plante p in joueur.plantes) {

			if (p.croissance == true) {

				p.jour++;
			}

			foreach (Etat e in p.listeEtat) {

				if (e.jour != -1) {

					e.jour++;
				}
			}
		}

		Comportement.jour++;
	}
	
	float value = 110;
	bool isChanger = false;
	bool isGraine = false;

	void OnGUI () {

		Vector2 positionPlante = new Vector2(40, 120);
		Vector2 positionLampe = new Vector2(0, value);

		/* int xPot = (int)(x + (placardLargeur - potLargeur) / 2);
        int yPot = (int)(y + (placardHauteur - potHauteur - 10));

        int xRacine = (int)(xPot + (potLargeur - plante.racine.largeur) / 2);
        int yRacine = (int)(yPot + (potHauteur - plante.racine.hauteur) / 2);
        placard.boite("Placard", x, y);
        pot.boite("Pot",xPot, yPot);
        plante.racine.boite("R", xRacine, yRacine);*/
		
		if (planteCourante != null) {

			distance = (placardHauteur - planteCourante.texturePlante.height) - (positionLampe.y + joueur.lampe.textureLampe.height);
		}
		
		// affiche le nombre de jours de la plante et le nombre de jours global
		GUILayout.BeginArea(new Rect(300, 150, 300, 200));

		if (planteCourante != null) {

			GUI.Label(new Rect(0, 0, 100, 20), "Jour de la plante");
			GUI.Label(new Rect(100, 0, 100, 20), Convert.ToString(planteCourante.jour));
		}

		GUI.Label(new Rect(0, 30, 100, 20), "Jour du jeu");
		GUI.Label(new Rect(100, 30, 100, 20), Convert.ToString(jour));

		if (planteCourante != null) {

			GUI.Label(new Rect(0, 60, 100, 20), "Distance lampe-plante");
			GUI.Label(new Rect(100, 60, 100, 20), Convert.ToString(distance));
		}

		GUI.Label(new Rect(0, 90, 100, 20), "Place libre : ");
		GUI.Label(new Rect(100, 90, 100, 20), Convert.ToString(joueur.placeLibre));
		GUI.Label(new Rect(0, 120, 100, 20), "Place Maximale");
		GUI.Label(new Rect(100, 120, 100, 20), Convert.ToString(joueur.placeMax));

		GUILayout.EndArea();
		
		// affichage du pot, de la plante et de la lampe
		GUILayout.BeginArea(new Rect(550, 600 - placardHauteur, 200, placardHauteur + pot.texturePot.height));

		// ---------> la distance lampe-plante se gère dans la partie taille d'entretien
		if (planteCourante != null) {

			value = GUI.VerticalSlider(new Rect(180, 40, 10, (placardHauteur - planteCourante.texturePlante.height)), value, 0, (placardHauteur - planteCourante.texturePlante.height - joueur.lampe.textureLampe.height));
		}

		GUI.DrawTexture(new Rect(positionLampe.x, positionLampe.y, joueur.lampe.textureLampe.width, joueur.lampe.textureLampe.height), joueur.lampe.textureLampe, ScaleMode.ScaleToFit, true);

		if (planteCourante != null) {

			GUI.DrawTexture(new Rect(positionPlante.x + ((pot.texturePot.width - planteCourante.texturePlante.width) / 2), (placardHauteur - planteCourante.texturePlante.height), planteCourante.texturePlante.width, planteCourante.texturePlante.height), planteCourante.texturePlante, ScaleMode.ScaleToFit, true);
		}

		GUI.DrawTexture(new Rect(positionPlante.x, placardHauteur, pot.texturePot.width, pot.texturePot.height), pot.texturePot, ScaleMode.ScaleToFit, true);

		GUILayout.EndArea();
		
		if (planteCourante != null) {

			// ici commence l'affichage de la plante
			GUILayout.BeginArea(new Rect(800, 400, 500, 500));

			GUILayout.Label("Plante actuelle :" + planteCourante.nom);
			GUILayout.Label("Etat : ");

			if (planteCourante.listeEtat.Count > 0) {

				int i = 1;

				foreach (Etat etat in planteCourante.listeEtat) {

					if ((etat.limiteJour - etat.jour) >= 0 && etat.jour != -1) {

						GUILayout.Label("- " + etat.getNom() + ", jour restant avant conséquence : " + (etat.limiteJour - etat.jour));
					}

					else {

						GUILayout.Label("- " + etat.getNom());
					}

					i++;
				}
			}

			else {

				GUILayout.Label("Aucun");
			}
			
			GUILayout.Label("Prix plante");
			GUILayout.Label(Convert.ToString(planteCourante.prix));

			GUILayout.EndArea();
		}
		
		// changer de pot ----> il faut que ça se gère dans la partie matériel de Stock
		GUILayout.BeginArea(new Rect(300, 400, 150, 400));

		if (GUILayout.Button("Changer de pot")) {

			if (isChanger)
				isChanger = false;

			else
				isChanger = true;                        
		}
		
		if (isChanger) {

			int j = 1;

			foreach (Pot p in joueur.pots) {

				if ((p.Equals(pot))) {

					continue;
				}

				GUILayout.Label("Pot " + (j));

				foreach (Plante pl in p.listePlante) {

					GUILayout.Label(pl.nom);
				}
				
				if (GUILayout.Button("Changer !")) {

					pot = p;

					if (p.listePlante.Count > 0) {

						planteCourante = p.listePlante[0];
					}

					else {

						planteCourante = null;
					}

					break;
				}

				j++;
			}
		}
		
		GUILayout.EndArea();
		
		// ajouter une graine -----> il faut que ça se gère dans la partie produits de Stock
		GUILayout.BeginArea(new Rect(750, 200, 150, 400));

		// if (GUILayout.Button("Planter une graine")) { }
		// Debug.Log(pot.listePlante.Count + " ===== " + pot.nombrePlante + " =========== " + joueur.graines.Count);
		if (joueur.graines.Count > 0 && pot.listePlante.Count < pot.nombrePlante) {

			if (GUILayout.Button("Planter une graine")) {

				if (isGraine)
					isGraine = false;

				else
					isGraine = true;
			}
			
			if (isGraine) {

				GUILayout.Label("Graine ");

				foreach (Plante p in joueur.graines) {

					GUILayout.Label("- " + (p.nom));
					
					if (GUILayout.Button("Planter !")) {

						pot.listePlante.Add(p);
						joueur.graines.Remove(p);
						joueur.placeLibre -= 1;
						Debug.Log(p.niveauEau);
						planteCourante = p;
						planteCourante.definirTexture();
						joueur.plantes.Add(p);
						break;
					}
				}
			}
		}

		GUILayout.EndArea();

		// les données globales du joueur
		GUILayout.BeginArea(new Rect(Screen.width - 500, 10, 250, 600));

		GUILayout.Label("Equipement de " + joueur.pseudo);

		if (joueur.lampe != null) {

			GUILayout.Label("Lampe d'une puissance de : " + joueur.lampe.puissance);
		}

		if (joueur.arrosoir != null) {

			string q = "";

			switch (joueur.arrosoir.qualite) {

			case Qualitee.mauvaise: q = "Arrosoir de mauvaise qualité";
				break;
			case Qualitee.normale: q = "Arrosoir de qualité normale";
				break;
			case Qualitee.moyenne: q = "Arrosoir de qualité moyenne";
				break;
			case Qualitee.bonne: q = "Arrosoir de bonne qualité";
				break;
			case Qualitee.pro: q = "Arrosoir de pro";
				break;
			}

			GUILayout.Label(q);
			GUILayout.Label("Nombre d'utilisation(s) restante(s) : " + Convert.ToString(joueur.arrosoir.nombreUtilisation));
		}

		int nombrePlante = 0;

		foreach (Pot p in joueur.pots) {

			nombrePlante = nombrePlante + p.listePlante.Count;
		}
		
		GUILayout.Label("Vous avez " + nombrePlante + " plante(s) : ");
		
		foreach (Pot p in joueur.pots) {

			foreach (Plante pl in p.listePlante) {

				GUILayout.Label(" - " + pl.nom);
			}
		}

		/* GUILayout.Label("Vous avez un placard : " + placard.largeur + "X" + placard.hauteur);
        GUILayout.Label("Soit un volume de : " + volume);*/

		GUILayout.EndArea();
	}
}