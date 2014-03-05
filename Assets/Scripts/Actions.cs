using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/* Les actions servent à remplir et servir aux missions, succès et objectifs
 * pour l'instant seul arroser a été implémenté et fonctionne a merveille
 * le gain d'eau peut etre modifié comme la perte d'eau peut l'etre aussi
 * */
public class Actions {
	
	public static void arroser(Plante plante) {
		
		Comportement.joueur.arrosoir.nombreUtilisation -= 1;

		if (Comportement.joueur.arrosoir.nombreUtilisation <= 0) {

			Comportement.joueur.arrosoir = null;
		}

		plante.niveauEau += 50;
	}
	
	public static void remplacerLampe() {

	}
	
	public static void remplacerVentilateur() {

	}
	
	public static void remplacerPot() {

	}
	
	public static void remplacerPlacard() {

	}
	
	public static void remplacerPlante() {

	}
}

public class Arrosoir {

	public int nombreUtilisation;
	public Qualitee qualite;
	
	public Arrosoir(Qualitee qualite) {
		
		this.qualite = qualite;

		if (qualite == Qualitee.mauvaise) {
			nombreUtilisation = 5;
		}

		else if (qualite == Qualitee.normale) {
			nombreUtilisation = 10;
		}

		else if (qualite == Qualitee.moyenne) {
			nombreUtilisation = 20;
		}

		else if (qualite == Qualitee.bonne) {
			nombreUtilisation = 30;
		}

		else if (qualite == Qualitee.pro) {
			nombreUtilisation = 50;
		}
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

/* UN ETAT ET UN INCIDENT UTILISENT TOUS LES DEUX TRIG ET CONDITION
 * ILS ONT AUSSI TOUS LES 2 UN MIN ET UN MAX MAIS QU'ILS UTILISENT DIFFEREMENT
 * INCIDENT VA UTILISER MIN ET MAX POUR L'EFFET RANDOM ET ETAT VA LES UTILISER POUR LES CONDITIONS ET SOLUTION
 * */
public abstract class Ulti {
	
	public abstract void trig();
	public abstract void condition();
	
	protected int min;
	protected int max;
	public bool isCondition;
	protected string nom;
	
	public Ulti(string nom, int min, int max) {

		this.min = min;
		this.max = max;
		this.nom = nom;
		this.isCondition = false;
	}
	
	public string getNom() {
		return this.nom;
	}
}

public abstract class Etat : Ulti {
	
	public int jour;
	public string couleur;
	public int limiteJour;
	
	public Etat(string nom, int min, int max) : base (nom, min, max) {

	}
	
	public override void trig() {

		condition();
	}
	
	public abstract override void condition();
	public abstract void solution(List<Etat> liste, Etat etat);
}


public class EtatArrosage : Etat {
	
	public EtatArrosage(string nom, int min, int max) : base(nom, min, max) {

	}

	public override void condition() {
		
		if (Comportement.planteCourante.niveauEau < min) {
			isCondition = true;
			
			if (jour >= limiteJour) {

				couleur = "jaune";
			}
		}

		if (Comportement.planteCourante.niveauEau > max) {
			isCondition = true;

			if (jour >= limiteJour) {

				couleur = "noire";
			}
		}
	}
	
	public override void solution(List<Etat> liste, Etat etat) {

		if (Comportement.planteCourante.niveauEau <= max && Comportement.planteCourante.niveauEau >= min) {

			if (liste.Contains(etat)) {

				isCondition = false;
				jour = 0;
				couleur = "verte";
				liste.Remove(etat);
			}
		}
	}
}

public class EtatEngrais : Etat {

	public EtatEngrais(string nom, int min, int max) : base(nom, min, max) {

	}
	
	public override void condition() {

	}
	
	public override void solution(List<Etat> liste, Etat etat) {

	}
}

public class EtatLampe : Etat {

	public EtatLampe(string nom, int min, int max) : base(nom, min, max) {

		jour = -1;
	}
	
	public override void condition() {

		if (Comportement.distance < min) {

			isCondition = true;
			couleur = "noire";
		}

		if (Comportement.distance > max) {

			isCondition = true;
			Comportement.planteCourante.croissance = false;
		}
	}
	
	public override void solution(List<Etat> liste, Etat etat) {

		if (Comportement.distance <= max && Comportement.distance >= min) {

			if (liste.Contains(etat)) {

				isCondition = false;
				Comportement.planteCourante.croissance = true;
				couleur = "verte";
				liste.Remove(etat);
			}
		}
	}
}

public abstract class Incident : Ulti {
	
	public bool isRandom;

	public Incident(string nom, int min, int max) : base(nom, min, max) {

		isRandom = false;
	}
	
	public override void trig() {

		int random = UnityEngine.Random.Range(min, max);

		if (random == 1) {

			isRandom = true;
		}
	}
	
	public abstract override void condition();

	public abstract void solution(List<Incident> liste, Incident incident);
}

public class IncidentInsecte : Incident {

	public IncidentInsecte(string nom, int min, int max) : base(nom, min, max) {

	}
	
	public override void condition() {

	}
	
	public override void solution(List<Incident> liste, Incident incident) {

	}
}

/* ultiManage sert à gérer les incidents et les états, la partie incident est encore à compléter, pour la partie état,
 * la base est bien fondé, seule des ajouts suffiront pour ajouter un état
 * */
public class  UltiManage {

	EtatArrosage etatArrosage;
	EtatEngrais etatEngrais;
	EtatLampe etatLampe;
	IncidentInsecte incidentInsecte;
	List<Etat> listeEtat;
	List<Incident> listeIncident;
	
	public UltiManage() {       

		etatArrosage = new EtatArrosage("Etat arrosage", 20, 100);
		etatEngrais = new EtatEngrais("Etat engrais", 1, 11); 
		etatLampe = new EtatLampe("Etat lampe", 40, 100); 
		incidentInsecte = new IncidentInsecte("Incident insecte", 1, 26);
		listeEtat = new List<Etat>();
		listeIncident = new List<Incident>();
	}
	
	public void incident() {

		if (Comportement.placard != null)
			listeIncident = Comportement.placard.listeIncident;
		
		incidentInsecte.trig();
		
		if (incidentInsecte.isCondition && incidentInsecte.isRandom) {

			if (!listeIncident.Contains(incidentInsecte)) {

				listeIncident.Add(incidentInsecte);
			}
		}
	}
	
	public void etat() {

		if(Comportement.planteCourante != null)
			listeEtat = Comportement.planteCourante.listeEtat;
		
		etatArrosage.trig();
		etatEngrais.trig();
		etatLampe.trig();

		if (etatArrosage.isCondition) {

			if (!listeEtat.Contains(etatArrosage)) {

				listeEtat.Add(etatArrosage);
			}
		}
		
		if (etatEngrais.isCondition) {

			if (!listeEtat.Contains(etatEngrais)) {

				listeEtat.Add(etatEngrais);		
			}	
		}
		
		if (etatLampe.isCondition) {

			if (!listeEtat.Contains(etatLampe)) {

				listeEtat.Add(etatLampe);	
			}		
		}
	}
}