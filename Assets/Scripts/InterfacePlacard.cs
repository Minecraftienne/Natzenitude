using UnityEngine;
using System.Collections;

public class InterfacePlacard : MonoBehaviour {

	#region Attributs 
	public GUISkin myGUISkin;

	private  static int _day; // Jour en jeu.

	public float _timer; // Chrono pour comptabiliser les jours, échelle du temps 24 secondes = 24 heures.

	private Menu _currentMenu = Menu.Placard; // Menu courante du jeu
	private bool _pauseTime; // Met le jeu en pause (stoppe uniquement les jours !!!)
	private Vector3 _scale;
	private float _originalWidth = 1280.0f; // Taille native horizontale
	private float _originalHeight = 800.0f; // Taille native verticale
	private Plante _plante; // Script plante
	#endregion
	#region Proprietes
	public enum Menu // Enumération des Menus Principaux
	{
		Placard, 
		Store, 
		MyNote,
		Settings,
		Size,
		Materials,
		Products,
		Helps,
		Notifications,
		Options, 
		Quit
	}

	#endregion

	#region Methodes

	void Start () {
		_plante = GameObject.Find("1 - Scripts middleground").GetComponent<Plante>();
	}
	

	void Update () {
		if(!_pauseTime) // Si pauseTime est faux (jeux pas en pause)
			_timer += Time.deltaTime; // Incrémente timer du temps qui passe
	
		if(_timer >= 24) // Si timer est supérieur ou égal à 24 secondes , jours augmente de 1 , timer retombe a 0
		{
			_day += 1;
			_timer = 0;
		}

		if(_currentMenu != Menu.Placard)
			_plante.enabled = false;

	}
	

	void OnGUI ()
	{
		GUI.skin = myGUISkin; // Utilise un GUI personalisé
		GUIStyle labelDay = myGUISkin.GetStyle("LabelJour");

		_scale.x = Screen.width/_originalWidth; // Calcule la taille horizontale
		_scale.y = Screen.height/_originalHeight; // Calcule la taille verticale
		_scale.z = 1;

		Matrix4x4 currentMatrix = GUI.matrix; // Sauvegarde la/le matrix courant(e)

		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, _scale); // Matrix de substitution, Uniquement la taille est modifié.

		switch(_currentMenu) // Switch la page courante et affiche la bonne page
		{
			case Menu.Placard: PlacardMenu(); break; // Lance la Methode PlacardMenu
			case Menu.Store: StoreMenu(); break; // Lance la Methode StoreMenu
			case Menu.MyNote: MyNote(); break;
		
		}

		if(_day <= 1) // Si _day est inférieur ou égal à 1 Jour sans "s", sinon avec
			GUI.Label(new Rect((50.0f/100) * _originalWidth - 150, (5.0f/100) * _originalHeight - 25, 300, 50), "Jour : " + _day, labelDay); 
		else
			GUI.Label(new Rect((50.0f/100) * _originalWidth - 150, (5.0f/100) * _originalHeight - 25, 300, 50), "Jours : " + _day, labelDay); 

		GUI.matrix = currentMatrix;
	}

	void PlacardMenu ()
	{
		#region Recuperation des customs style
		GUIStyle pauseStyle = myGUISkin.GetStyle("Pause"); // Récupère le custom style du GUISkin nommé Pause
		GUIStyle playStyle = myGUISkin.GetStyle("Lecture"); 
		GUIStyle stepForwardStyle = myGUISkin.GetStyle("Accelerer"); 
		GUIStyle storeButtonStyle = myGUISkin.GetStyle("Boutique");
		GUIStyle labelBold = myGUISkin.GetStyle("LabelBold");
		#endregion
		#region Play, Pause, Accelerer,
		GUI.BeginGroup(new Rect((52.0f/100) * _originalWidth, (92.0f/100) * _originalHeight, 220, 60)); // Créer un groupe de GUI

		_pauseTime = GUI.Toggle(new Rect(10, 0, 50, 50), _pauseTime, "", pauseStyle); // Si j'appuis sur pause, le timer s'arrette, les jours augmentent plus
		
		if(GUI.Button(new Rect(70, 0, 50, 50), "", playStyle)) // Si j'appuis sur Lecture et que le jeu est en pause alors le jeu reprends, les jours augmentent
		{
			if(_pauseTime)
				_pauseTime = false;
		}

		if(GUI.Button(new Rect(130, 0, 50, 50), "", stepForwardStyle)) // Si j'appuis sur accelerer _day augmente de 1
		   _day++;

		GUI.EndGroup(); // Fin du groupe
		#endregion


		if(GUI.Button(new Rect((94.0f/100) * _originalWidth - 65, (52.0f/100) * _originalHeight - 30, 130, 60), "", storeButtonStyle))
		{
			_currentMenu = Menu.Store;
		}
		/*if(GUI.Button(new Rect((5.0f/100) * _originalWidth - 60, (50.0f/100) * _originalHeight - 25, 120, 50),"Menu"))
		{
			_currentMenu = Menu.Main;
		}*/

		GUI.BeginGroup(new Rect(105, (21.0f/100) * _originalHeight, 450, 610), "");
		GUI.Label(new Rect(75, 7, 300, 50), "Carnet de note", labelBold);
		GUI.EndGroup();
	}

	void StoreMenu () 
	{

	}

	void MyNote ()
	{

	}
	#endregion
}
