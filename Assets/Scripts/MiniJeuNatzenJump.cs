using UnityEngine;
using System.Collections;

public class MiniJeuNatzenJump : MonoBehaviour {

	#region Variables
	public SpriteRenderer plateformeBlanche;
	public SpriteRenderer natzen; // personnage

	public GUIText tempsTexte;

	private float _temps;

	private int _vitesse;
	#endregion

	#region Variables Saut
	// Enumération pour gérer l'état de Natzen
	private enum StateNatzen {

		IsGround, // Touche le sol
		IsWaitJump, // Doit sauter
		IsJump, // Saute, passe en avant-plan par rapport aux plateformes, sans collision
		IsUp // Est en l'air
	}

	[SerializeField]
	private float _forceJump = 1500f; // Force à appliquer à Natzen
	private Vector2 _vectorJump; // Vecteur de force à appliquer à Natzen
	private StateNatzen _state; // Etat de Natzen
	
	// Modifie ou récupère la valeur de la force à appliquée pour le saut
	public float ForceJump
	{
		get { return _forceJump; }

		set {
			if (value != _forceJump) {
				_forceJump = value;
				_vectorJump = CalcVectorUp(value);
			}
		}
	}
	
	// Calcul du vecteur de force à appliquer au mobile pour un saut.
	private static Vector2 CalcVectorUp(float forceUp)
	{
		return Vector2.up * forceUp;
	}
	#endregion

	// Use this for initialization
	void Start () {

		_temps = 30.0F;
		_vitesse = 10;

		_vectorJump = CalcVectorUp(_forceJump);

		InitNatzen();
		GenererPlateformes();
	}
	
	// Update is called once per frame
	void Update () {

		AfficherTemps();
		Deplacement();

		switch (_state) {

			case StateNatzen.IsGround:
				Debug.Log("Natzen au sol");
				if (Input.GetKeyDown(KeyCode.Space))
					_state = StateNatzen.IsWaitJump;
				break;
			case StateNatzen.IsWaitJump:
				Debug.Log("Natzen va sauter");
				break;
			case StateNatzen.IsJump:
				Debug.Log("Natzen saute");
				break;
			case StateNatzen.IsUp:
				Debug.Log("Natzen est en l'air");
				break;
		}
	}
	
	// Update moteur physique
	void FixedUpdate()
	{
		if (_state == StateNatzen.IsWaitJump)
		{
			rigidbody2D.AddForce(new Vector2(0f, ForceJump));
			_state = StateNatzen.IsJump;
		}
	}
	
	// Natzen entre en collision avec une plateforme
	void OnCollisionEnter2D(Collision2D collider)
	{
		if (collider.gameObject.tag == "Plateforme")
			_state = StateNatzen.IsGround;
	}
	
	// Natzen quitte le contact avec une plateforme
	void OnCollisionExit2D(Collision2D collider)
	{
		if (collider.gameObject.tag == "Plateforme")
			_state = StateNatzen.IsUp;
	}

	void InitNatzen ()
	{
		natzen = Instantiate(natzen, new Vector3(0, -2.5f, 0), Quaternion.Euler(0, 0, 0)) as SpriteRenderer;
	}

	#region Plateformes
	void GenererPlateformes () {

		// positions aléatoires en x
		// incrément aléatoire en y
	}
	#endregion

	#region Temps
	// affiche un chrono en haut à gauche qui commence à 30 secondes
	void AfficherTemps () {

		tempsTexte.text = "Temps : " + Mathf.Floor(_temps);
		_temps -= Time.deltaTime;
		
		if(_temps <= 0) {

			_temps = 0;
		}
	}
	#endregion

	#region Deplacement
	// déplacement avec les touches gauches et droites
	void Deplacement () {

		if (Input.GetKey(KeyCode.LeftArrow)) {

			natzen.transform.Translate(new Vector2(-_vitesse, 0) * Time.deltaTime);
		}

		else if(Input.GetKey(KeyCode.RightArrow)) {

			natzen.transform.Translate(new Vector2(_vitesse, 0) * Time.deltaTime);
		}
	}
	#endregion

	/* + d'infos sur http://pageperso.lif.univ-mrs.fr/~edouard.thiel/ens/igra/projet-doodlejump.pdf
	 * 
	 * sauts automatiques
	 * parallax scrolling vertical
	 * conditions de victoires : chrono à 0 + joueur en vie
	 * collisions uniquement en phase de chute -> pour rebondir, acquérir un bonus, tomber (monstre) ou perdre
	 * 
	 * 5 types de plateformes
	 * Bleu clair : Plateformes faisant des aller-retours horizontaux a vitesse constante.
	 * Bleu gris : Plateformes faisant des aller-retours verticaux a vitesse constante.
	 * Marron : Ces plateformes se cassent si le personnage saute dessus.
	 * Jaune : Apres quelques secondes, ces plateformes deviennent rouge, puis explosent.
	 * Blanc : Ces plateformes disparaissent apres que le personnage ait saute dessus.
	 * 
	 * FACULTATIF ou à rajouter en mise à jour
	 * bonus : ressort, fusée
	 * monstres
	 * */
}
