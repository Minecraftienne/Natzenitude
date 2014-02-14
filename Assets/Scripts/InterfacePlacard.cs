using UnityEngine;
using System.Collections;

public class InterfacePlacard : MonoBehaviour {

	#region Attributs 
	public GUISkin myGUISkin;

	private  static int _day; // Jour en jeu.
	public float _timer; // Chrono pour comptabiliser les jours, échelle du temps 24 secondes = 24 heures.
	private Menu _currentMenu = Menu.placard; // Menu courante du jeu
	private bool _pauseTime; // Met le jeu en pause (stoppe uniquement les jours !!!)

	private Vector3 _scale;
	private float _originalWidth = Screen.width; // Taille native horizontale
	private float _originalHeight = Screen.height; // Taille native verticale
	#endregion
	#region Proprietes
	public enum Menu
	{
		placard, // Placard
		noteBook, // Carnet de note
		servicing, // Entretien
		stock, // Stock
		store, // Boutique
		options, // Option
		help // Aide

	}
	#endregion

	#region Methodes

	void Start () {

	}
	

	void Update () {
		if(!_pauseTime) // Si pauseTime est faux (jeux pas en pause)
			_timer += Time.deltaTime; // Incrémente timer du temps qui passe
	


		if(_timer >= 24) // Si timer est supérieur ou égal à 24 secondes , jours augmente de 1 , timer retombe a 0
		{
			_day += 1;
			_timer = 0;
		}

	}
	

	void OnGUI ()
	{
		GUI.skin = myGUISkin; // Utilise un GUI personalisé


		_scale.x = Screen.width/_originalWidth; // Calcule la taille horizontale
		_scale.y = Screen.height/_originalHeight; // Calcule la taille verticale
		_scale.z = 1;

		Matrix4x4 currentMatrix = GUI.matrix; // Sauvegarde la/le matrix courant(e)

		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, _scale); // Matrix de substitution, Uniquement la taille est modifié.

		switch(_currentMenu) // Switch la page courante et affiche la bonne page
		{
			case Menu.placard: PlacardMenu(); break; // Lance la Methode PlacardMenu, c'est le Menu de base
			case Menu.noteBook: NoteBookMenu(); break; // Lance la Methode NoteBookMenu, le carnet de note
			case Menu.servicing: ServicingMenu(); break; // Lance la Methode ServicingMenu, le menu Entretien
			case Menu.stock: StockMenu(); break; // Lance la Methode StockMenu, le menu des stocks
		//	case Menu.store: StoreMenu(); break;
			case Menu.options: OptionsMenu(); break; // Lance la Methode OptionsMenu, le menu options
			case Menu.help: HelpMenu(); break; // Lance la Methode HelpMenu, le menu d'aide

		}
		GUI.matrix = currentMatrix;
	}

	void PlacardMenu ()
	{
		GUIStyle pauseStyle = myGUISkin.GetStyle("Pause"); // Récupère le custom style du GUISkin nommé Pause
		GUIStyle playStyle = myGUISkin.GetStyle("Lecture"); // Récupère le custom style du GUISkin nommé Lecture
		GUIStyle stepForwardStyle = myGUISkin.GetStyle("Accelerer"); // Récupère le custom style du GUISkin nommé Accelerer

		GUI.Box(new Rect(0, 0, _originalWidth, 50), "Barre d'evenement defilante"); // Barre d'évènement défilant, placé en haut de l'écran
		#region Play, Pause, Accelerer, Jours et Placard
		GUI.BeginGroup(new Rect(Screen.width / 2 - 150, Screen.height / 3 - 130, 300, 500),""); // Créer un groupe de GUI

		if(_day <= 1) // Si _day est inférieur ou égal à 1 Jour sans "s", sinon avec
			GUI.Label(new Rect(20, 10, 100, 60), "Jour :" + _day); // Nombre de jours, placé au centre en dessous de la barre défilante
		else
			GUI.Label(new Rect(20, 10, 100, 60), "Jours :" + _day); // Nombre de jours, placé au centre en dessous de la barre défilante

		_pauseTime = GUI.Toggle(new Rect(70, 0, 40, 40), _pauseTime, "", pauseStyle); // Si j'appuis sur pause, le timer s'arrette, les jours augmentent plus
		
		if(GUI.Button(new Rect(120, 0, 40, 40), "", playStyle)) // Si j'appuis sur Lecture et que le jeu est en pause alors le jeu reprends, les jours augmentent
		{
			if(_pauseTime)
				_pauseTime = false;
		}

		if(GUI.Button(new Rect(170, 0, 40, 40), "", stepForwardStyle)) // Si j'appuis sur accelerer _day augmente de 1
		   _day++;

		GUI.Box(new Rect(10, 50, 290, 400), "Placard");
		GUI.EndGroup(); // Fin du groupe
		#endregion
		#region Menu de Gauche
		GUI.BeginGroup(new Rect(Screen.width / 4 - 50, Screen.height / 2 - 150, 150, 300), ""); // Créer un groupe de GUI

		if(GUI.Button(new Rect(20, 0, 100, 40), "Carnet de note")) // Si j'appuis sur Carnet de note, la page courante change pour le carnet
			_currentMenu = Menu.noteBook;

		if(GUI.Button(new Rect(20, 50, 100, 40), "Entretien")) // Si j'appuis sur Entrentien, la page courante change pour l'entretien
			_currentMenu = Menu.servicing;

		if(GUI.Button(new Rect(20, 100, 100, 40), "Stock")) // Si j'appuis sur Stock, la page courante change pour le stock
			_currentMenu = Menu.noteBook;

		//if(GUI.Button(new Rect(20, 150, 100, 40), "Boutique")) // Si j'appuis sur Boutique, la page courante change pour la boutique
			//_currentMenu = Menu.store;
			
		
		GUI.EndGroup(); // Fin du groupe
		#endregion
		#region Magasin, Aide et Options
		GUI.BeginGroup(new Rect((Screen.width - 350), (Screen.height / 2 - 195), 300, 500));
		GUI.Box(new Rect(0, 0, 300, 300), "Boutique");
		GUI.Label(new Rect(115, 310, 100, 50), "Argents : 0");
		if(GUI.Button(new Rect(0, 330, 100, 40), "Aide"))
			_currentMenu = Menu.help;
		if(GUI.Button(new Rect(200, 330, 100, 40), "Options"))
			_currentMenu = Menu.options;

		GUI.EndGroup();
		#endregion
	}

	void NoteBookMenu ()
	{
		GUI.BeginGroup(new Rect(Screen.width / 4 - 50, Screen.height / 2 - 150, 150, 300), ""); 
		
		/*if(GUI.Button(new Rect(20, 0, 100, 40), "Dernières infos")) 

		
		if(GUI.Button(new Rect(20, 50, 100, 40), "Objectifs en cours"))

		
		//if(GUI.Button(new Rect(20, 100, 100, 40), "Plan d'évolution")) 

		
		//if(GUI.Button(new Rect(20, 150, 100, 40), "Mes Notes"))*/
		
		
		
		GUI.EndGroup(); // Fin du groupe


	}

	void ServicingMenu ()
	{
		
	}

	void StockMenu ()
	{
		
	}

	void OptionsMenu ()
	{
		
	}

	void HelpMenu ()
	{

	}
	#endregion

}
