using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/* le joueur a un seul arrosoir
 * une seule lampe
 * peut avoir plusieurs plantes et graines
 * de l'argent
 * placelibre sert dans le changement de pot, pour savoir si on peut planter une graine ou pas
 * placeMax sert a savoir si on peut planter en faisant la comparaison avec placelibre
 * ensuite on fait juste une init de ca
 * */

public class Joueur : MonoBehaviour {

	public string pseudo;
	public Arrosoir arrosoir;
	public Lampe lampe;
	public List<Plante> plantes;
	public List<Plante> graines;
	public List<Pot> pots;
	public int argent;
	public int placeLibre;
	public int placeMax;
	public Taille taille;
	
	public Joueur (string pseudo, int argent) {

		this.pseudo = pseudo;
		this.argent = argent;
		taille = Taille.petit;
		arrosoir = new Arrosoir(Qualitee.normale);
		lampe = new Lampe(250);
		Plante plante = new Plante("Fraisier");
		plantes = new List<Plante>();
		graines = new List<Plante>();
		plantes.Add(plante);
		Pot pot = new Pot(taille);
		pot.ajouterPlante(plante);
		pots = new List<Pot>();
		pots.Add(pot);
		placeLibre = 0;
		placeMax = (int)taille;
	}
}

/* le placard est un objet avec une dimension et une liste d'incident, qui ne touche que le placard mais qui peut se répercuter 
 * sur les autres objets
 * Incident pas encore développé
 * */
public class Placard : Objet {
	
	public List<Incident> listeIncident;
	
	public Placard(float largeur, float hauteur): base (largeur, hauteur) {

		listeIncident = new List<Incident>();
	}
}

public enum Taille {

	petit = 1,
	moyen = 2,
	grand = 3
}

/* un pot a une liste de plantes bien à lui, il peut accueillir entre 1-3 plantes
 * avec placelibre de joueur, cela permet de savoir si on peut planter dans ce pot précisément
 * on lui affecte également une textutre
 * */
public class Pot {
	
	public Texture2D texturePot;
	public int nombrePlante;
	public List<Plante> listePlante;

	public Pot(Taille taille) {

		nombrePlante = (int)taille;
		listePlante = new List<Plante>();
		texturePot = Resources.Load("Textures/pot") as Texture2D;
	}
	
	public void ajouterPlante(Plante plante) {

		if (listePlante.Count < nombrePlante) {

			listePlante.Add(plante);
		}
	}
}

// la lampe a une puissance et une texture
public class Lampe {

	public int puissance;
	public Texture2D textureLampe;
	
	public Lampe(int puissance) {

		this.puissance = puissance;
		textureLampe = Resources.Load("Textures/neon") as Texture2D;
	}
}

/* la plante peut avoir plusieurs états, à une croissance vrai/faux, un nombre de jours, un prix, un niveau d'eau
 * les autres attributs devraient servir pour plus tard, sauf si on change le CDC ou si on en a réellement pas besoin
 * */
public class Plante  {
	
	public Terre terre;
	public int feuille;
	public Objet racine;
	public double[] temperatureIdeale;
	public double[] temperatureCritique;
	public double[] temperatureMort;
	public double[] humiditeIdeale;
	public double[] humiditeCritique;
	public double[] humiditeMort;
	public int niveauEau;
	public string nom;
	public int prix;
	public int jour;
	public List<Etat> listeEtat;
	public Texture2D texturePlante;
	public bool croissance;
	
	private int nombreEtapeMin1 = 3;
	private int nombreEtapeMax1 = 13;
	
	private int nombreEtapeMin2 = 14;
	private int nombreEtapeMax2 = 20;
	
	private int nombreEtapeMin3 = 21;
	private int nombreEtapeMax3 = 27;
	
	private int nombreEtapeFinale = 28;
	
	int stadeActuel;
	int stade;
	
	public Plante(string nom) {

		croissance = true;
		terre = new Terre();
		feuille = 0;
		this.nom = nom;
		prix = 0;
		jour = 0;
		stade = 0;
		stadeActuel = 0;

		temperatureIdeale = new double[2] { 20.0, 40.0 };
		temperatureCritique = new double[2] { 10.0, 60.0 };
		temperatureMort = new double[2] { -100.0, 80.0 };
		
		humiditeIdeale = new double[2] { 40.0, 50.0 };
		humiditeCritique = new double[2] { 30.0, 60.0 };
		humiditeMort = new double[2] { 10.0, 80.0 };

		niveauEau = 100;
		listeEtat = new List<Etat>();
	}
	
	/* Suivant le stade de la plante on fait les comparaisons nécessaires pour lui attribuer la texture appropriée
     * quand c'est noir il faudrait changer pour que ça grandisse pas
     * */
	public void definirTexture() {

		string couleur = "verte";
		
		if (jour >= nombreEtapeMin1 && jour <= nombreEtapeMax1) {
			stade = 1;
		}

		else if (jour >= nombreEtapeMin2 && jour <= nombreEtapeMax2) {
			stade = 2;
		}

		else if (jour >= nombreEtapeMin3 && jour <= nombreEtapeMax3) {
			stade = 3;
		}

		else if (jour >= nombreEtapeFinale) {
			stade = 4;
		}

		else {
			stade = 0;
		}
		
		foreach (Etat e in listeEtat) {
			
			if (e.couleur != "verte") {

				if (e.jour == -1) {

					couleur = e.couleur;
					break;
				}

				else if (e.jour >= e.limiteJour) {

					couleur = e.couleur;
					break;
				}
			}

			else {
				e.couleur = "verte";
			}
		}
		
		string s;

		if(stade == 0)
			s = "Textures/planteverte0";
		else
			s = "Textures/plante" + couleur + stade;
		
		texturePlante = Resources.Load(s) as Texture2D;
	}
	
	public void updatePrix() {

		if (stadeActuel < stade) {

			prix = prix + 1000;
			stadeActuel++;
		}

		if (listeEtat.Count > 0) {

			prix = prix - 1;
		}
	}
}

public class Terre {

	public TypeTerre type;
	public Dictionary<Engrais, double> engrais;
	public Dictionary<Additif, double> additif;
	
	public Terre() {

		type = new TypeTerre();
		engrais = new Dictionary<Engrais, double>();
		additif = new Dictionary<Additif, double>();
	}
}

public class Engrais {

	string nom;
	Qualitee qualite;
	
	public Engrais(string nom, Qualitee qualite) {

		this.nom = nom;
		this.qualite = qualite;
	}
	
}

public class Additif {

	string nom;
	TypeAdditif type;
	
	public Additif(string nom) {

		this.nom = nom;
		type = new TypeAdditif();
	}
}

public class Objet {
	
	public float largeur;
	public float hauteur;
	
	public Objet(float largeur, float hauteur) {

		this.largeur = largeur;
		this.hauteur = hauteur;
	}
}

public enum TypeTerre {

	Boueux,
	Argileux,
	Compact,
	Aere
}

public enum Qualitee {

	pro,
	bonne,
	moyenne,
	normale,
	mauvaise
}

public enum TypeAdditif {

	reparateur,
	racinaire
}