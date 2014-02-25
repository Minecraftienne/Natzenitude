using UnityEngine;
using System.Collections;

public class MiniJeuNatzenJump : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/* + d'infos sur http://pageperso.lif.univ-mrs.fr/~edouard.thiel/ens/igra/projet-doodlejump.pdf
	 * 
	 * personnage dirigeable seulement en X (se dirige avec les touches gauche et droite)
	 * sauts automatiques
	 * parallax scrolling vertical
	 * chrono en haut à gauche qui commence à 30 secondes
	 * conditions de victoires : chrono à 0 + joueur en vie
	 * collisions uniquement en phase de chute -> pour rebondir, acquérir un bonus, tomber (monstre) ou perdre
	 * 
	 * 6 types de plateformes
	 * Vert : Plateformes ﬁxes, les plus frequentes au debut du jeu.
	 * Bleu clair : Plateformes faisant des aller-retours horizontaux a vitesse constante.
	 * Bleu gris : Plateformes faisant des aller-retours verticaux a vitesse constante.
	 * Marron : Ces plateformes se cassent si le personnage saute dessus.
	 * Jaune : Apres quelques secondes, ces plateformes deviennent rouge, puis explosent.
	 * Blanc : Ces plateformes disparaissent apres que le personnage ait saute dessus.
	 * 
	 * FACULTATIF ou à rajouter en mise à jour
	 * bonus
	 * monstres
	 * */
}
