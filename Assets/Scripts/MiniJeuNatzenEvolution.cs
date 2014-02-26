using UnityEngine;
using System.Collections;

public class MiniJeuNatzenEvolution : MonoBehaviour {
	#region Attributs
	public SpriteRenderer[] plantEvolution; // Contient toute les textures de la plante
	public SpriteRenderer[] waterDrops; // Contient les différents types de gouttes d'eau
	public SpriteRenderer[] otherItems; // Contient les différents obstacle à éviter

	public Transform[] spawnItems;

	public int nbEvolution; // Nombre de texture que contiendra le tableau d'évolution de la plante

	public SpriteRenderer flowerPot; // Pot de fleur
	public SpriteRenderer waterDrop; // Goutte d'eau

	public GUIText timeText;
	public GUIText scoreText;

	private int _score;
	private int _speed;

	private int WaterDropSmall = 10;
	private int WaterDropMiddle = 20;
	private int WaterDropBig = 40;

	private float _time;

	#endregion 

	#region Proprietes

	#endregion

	#region Methodes
	// Use this for initialization
	void Start () 
	{
		_score = 0;
		_speed = 5;
		_time = 30.0f;

		InitPlant();
		//InitWaterDrop();
	}

	void InitPlant ()
	{
		nbEvolution = 10;
		plantEvolution = new SpriteRenderer[nbEvolution];
		flowerPot = Instantiate(flowerPot, new Vector3(0, -2.5f, 0), Quaternion.Euler(0, 0, 0)) as SpriteRenderer;
	}

	// Update is called once per frame
	void Update () 
	{
		DisplayText();
		Move();



	}

	void DisplayText ()
	{
		scoreText.text = "Score : " + _score;
		timeText.text = "Temps : " + Mathf.Floor(_time);

		_time -= Time.deltaTime;

		if(_time <= 0)
		{
			_time = 0;
		}
	}

	void Move ()
	{
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			flowerPot.transform.Translate(new Vector2(-_speed, 0) * Time.deltaTime);
		}
		else if(Input.GetKey(KeyCode.RightArrow))
		{
			flowerPot.transform.Translate(new Vector2(_speed, 0) * Time.deltaTime);
		}
	}
	#endregion 
}
