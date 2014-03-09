using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BoutiqueManage : MonoBehaviour {
		
		public Joueur joueur;
		public Boutique boutique;
		
	    /* on initialise le joueur, la boutique et on ajoute tous les objets dans la boutique
         * cette partie pourrait etre remplacé par une lecture d'objets */
		void Start() {

			joueur = Comportement.joueur;
			boutique = new Boutique();

			if (joueur != null)
				Debug.Log("joueur non null");
			
			boutique.listeArrosoir.Add(Qualitee.mauvaise, 10);
			boutique.listeArrosoir.Add(Qualitee.normale, 15);
			boutique.listeArrosoir.Add(Qualitee.moyenne, 25);
			boutique.listeArrosoir.Add(Qualitee.bonne, 40);
			boutique.listeArrosoir.Add(Qualitee.pro, 60);
			
			boutique.listeLampe.Add(250, 30);
			boutique.listeLampe.Add(500, 50);
			boutique.listeLampe.Add(750, 90);
			boutique.listeLampe.Add(1000, 150);
			
			boutique.listePlante.Add("Fraisier", 100);
			boutique.listePlante.Add("Panda", 400);
			boutique.listePlante.Add("Ail", 140);
			boutique.listePlante.Add("Rose", 80);
			boutique.listePlante.Add("Althéa", 310);
			boutique.listePlante.Add("Géranium", 220);
			
			boutique.listePot.Add(Taille.petit, 100);
			boutique.listePot.Add(Taille.moyen, 200);
			boutique.listePot.Add(Taille.grand, 400);
		}

		void Update () {

			if (joueur.argent >= 10000) {

				Aide.succesRichesse = true;
			}
		}
		
		void OnMouseDown() {

			//Menu.menuCurrent = "Boutique";
		}
		
		bool isArrosoir = false;
		bool isLampe = false;
		bool isGraine = false;
		bool isPlante = false;
		bool isPot = false;
		
		/* le GUI bete et méchant
         * avec des rétractations quand on clique 2 fois sur le meme bouton
         * il faut rajouter une scrollview aussi
          * */
		void OnGUI() {

			//if (Menu.menuCurrent == "Boutique") {
				
				GUILayout.BeginArea(new Rect(10, 10, 200, 600));
				
				GUILayout.Label("Argent : " + Convert.ToString(joueur.argent));
				GUILayout.Space(10.0f);
				
				if (GUILayout.Button("Changer la lampe", GUILayout.Width(150.0f), GUILayout.Height(20.0f))) {

					if (isLampe)
						isLampe = false;
					else
						isLampe = true;
				}
				
				if (isLampe) {

					GUILayout.Label("Lampe : ");
					string q;

					foreach (KeyValuePair<int, int> d in boutique.listeLampe) {

						q = "Puissance : " + d.Key + " Watts, prix = " + d.Value;

						GUILayout.Label(q);

						if (joueur.argent < d.Value) {

							GUI.enabled = false;
						}

						else {

							GUI.enabled = true;
						}
						
						if (GUILayout.Button("Acheter", GUILayout.Width(150.0f), GUILayout.Height(20.0f))) {

							joueur.argent -= d.Value;
							joueur.lampe = new Lampe(d.Key);
						}

						GUI.enabled = true;
					}
				}
				
				if (joueur.arrosoir != null) {

					if (GUILayout.Button("Changer d'arrosoir", GUILayout.Width(150.0f), GUILayout.Height(20.0f))) {

						if (isArrosoir)
							isArrosoir = false;
						else
							isArrosoir = true;
					}
				}

				else {

					if (GUILayout.Button("Acheter un arrosoir", GUILayout.Width(150.0f), GUILayout.Height(20.0f))) {
						if (isArrosoir)
							isArrosoir = false;
						else
							isArrosoir = true;
					}
				}
				
				if (isArrosoir) {

					GUILayout.Label("Arrosoir : ");
					string q;

					foreach (KeyValuePair<Qualitee, int> d in boutique.listeArrosoir) {

						q = "Qualitée : " + d.Key + ", prix = " + d.Value;
						GUILayout.Label(q);

						if (joueur.argent < d.Value) {

							GUI.enabled = false;
						}

						else {

							GUI.enabled = true;
						}
						
						if (GUILayout.Button("Acheter", GUILayout.Width(150.0f), GUILayout.Height(20.0f))) {

							joueur.argent -= d.Value;
							joueur.arrosoir = new Arrosoir(d.Key);
						}
						
						GUI.enabled = true;
					}
				}
				
				if (joueur.placeLibre > 0) {

					if (GUILayout.Button("Acheter des graines", GUILayout.Width(150.0f), GUILayout.Height(20.0f))) {
						
						if (isGraine)
							isGraine = false;
						else
							isGraine = true;
					}
					
					if (isGraine) {

						string q;

						foreach (KeyValuePair<string, int> d in boutique.listePlante) {

							q = "Nom : " + d.Key + ", prix = " + d.Value;
							GUILayout.Label(q);

							if (joueur.argent < d.Value) {

								GUI.enabled = false;
							}

							else {

								GUI.enabled = true;
							}
							
							if (GUILayout.Button("Acheter", GUILayout.Width(150.0f), GUILayout.Height(20.0f))) {

								joueur.argent -= d.Value;
								joueur.graines.Add(new Plante(d.Key));
							}
							
							GUI.enabled = true;
						}
					}
					
				}

				else {

					GUI.enabled = false;
					GUILayout.Button("Acheter des graines", GUILayout.Width(150.0f), GUILayout.Height(20.0f));
					GUILayout.Label("Pas de place pour d'autres plantes");
				}
				
				GUI.enabled = true;

				if (joueur.plantes.Count > 0) {

					GUI.enabled = true;
				}

				else {

					GUI.enabled = false;
				}
				
				if (GUILayout.Button("Vendre une plante", GUILayout.Width(150.0f), GUILayout.Height(20.0f))) {

					if (isPlante)
						isPlante = false;
					else
						isPlante = true;
				}

				GUI.enabled = true;
				
				if (isPlante) {

					string q;

					for (int i = 0; i < joueur.plantes.Count; i++) {

						q = "Nom : " + joueur.plantes[i].nom + ", prix = " + joueur.plantes[i].prix;
						GUILayout.Label(q);
						
						if (GUILayout.Button("Vendre", GUILayout.Width(150.0f), GUILayout.Height(20.0f))) {

							joueur.argent += joueur.plantes[i].prix;
							joueur.plantes.Remove(joueur.plantes[i]);
							Comportement.planteCourante = null;
							joueur.placeLibre += 1;
							
							if (joueur.plantes.Count <= 0) {

								isPlante = false;
							}
						}
						
						GUI.enabled = true;
					}
				}
				
				if (GUILayout.Button("Acheter un pot", GUILayout.Width(150.0f), GUILayout.Height(20.0f))) {

					if (isPot)
						isPot = false;
					else
						isPot = true;
				}
				
				if (isPot) {

					GUILayout.Label("Pot : ");
					string q;

					foreach (KeyValuePair<Taille, int> d in boutique.listePot) {

						q = "Taille : " + d.Key + ", prix = " + d.Value;
						GUILayout.Label(q);

						if (joueur.argent < d.Value) {

							GUI.enabled = false;
						}

						else {

							GUI.enabled = true;
						}
						
						if (GUILayout.Button("Acheter", GUILayout.Width(150.0f), GUILayout.Height(20.0f))) {

							joueur.argent -= d.Value;
							joueur.placeLibre += (int)(d.Key);
							joueur.placeMax += (int)(d.Key);
							joueur.pots.Add(new Pot(d.Key));
						}
						
						GUI.enabled = true;
					}	
				}

				GUILayout.EndArea();
			//}
		}
}

public class Boutique {
	
	public Dictionary<int, int> listeLampe;
	public Dictionary<Qualitee, int> listeArrosoir;
	public Dictionary<Taille, int> listePot;
	public Dictionary<string, int> listePlante;
	
	public Boutique() {
		
		listeLampe = new Dictionary<int, int>();
		listeArrosoir = new Dictionary<Qualitee, int>();
		listePot = new Dictionary<Taille, int>();
		listePlante = new Dictionary<string, int>();
	}
}
