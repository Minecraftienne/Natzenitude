using UnityEngine;
using System.Collections;

public class MiniJeuNatzenEvolution : MonoBehaviour {
	#region Attributs
	public SpriteRenderer[] plantEvolution; // Contient toute les textures de la plante
	public SpriteRenderer[] waterDrops; // Contient les différents types de gouttes d'eau
	public SpriteRenderer[] otherItems; // Contient les différents obstacle à éviter

	public Transform[] spawnItems; // Spawner

	public int nbEvolution; // Nombre de texture que contiendra le tableau d'évolution de la plante

	public SpriteRenderer flowerPot; // Pot de fleur
	public SpriteRenderer waterDrop; // Goutte d'eau

	public GUIText timeText;
	public GUIText scoreText;

	private int _score; // Score du joueur
	private int _speed; // Vitesse de déplacement du pot de fleur

	private int WaterDropSmall = 10;
	private int WaterDropMiddle = 20;
	private int WaterDropBig = 40;

	private float _time; // Chrono du jeu
	private float _spawnerTime; // Chrono du spawner
	private float _waitTimeForSpawn; // Temps d'attente pour spawn une goutte
	private bool _canSpawn = false;

	#endregion 

	#region Proprietes

	#endregion

	#region Methodes
	// Use this for initialization
	void Start () 
	{
		_score = 0;
		_speed = 8;
		_time = 30.0f;
		_spawnerTime = 0.0f;
		_waitTimeForSpawn = 1.0f;

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

		if(!_canSpawn && _time > 0)
			_spawnerTime += Time.deltaTime;

		if(_spawnerTime >= _waitTimeForSpawn)
		{
			StartCoroutine("SpawningObject");
		}

		if(_time <= 0)
		{
			_time = 0;
		}

		if(_time > 15 && _time <= 20)
		{
			_waitTimeForSpawn = .6f;
		}
		else if(_time > 10 && _time <= 15)
		{
			_waitTimeForSpawn = .3f;
		}
		else if(_time > 0 && _time <= 10)
		{
			_waitTimeForSpawn = .1f;
		}

	}

	void DisplayText ()
	{
		scoreText.text = "Score : " + _score;
		timeText.text = "Temps : " + Mathf.Floor(_time);

		_time -= Time.deltaTime;

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

	IEnumerator SpawningObject ()
	{
		_canSpawn = true;
		_spawnerTime = 0;

		int rdmPos = Random.Range(1, 5);
		Transform location = null;

		if(rdmPos == 1)
		{
			location = spawnItems[0];
		}
		else if(rdmPos == 2)
		{
			location = spawnItems[1];
		}
		else if(rdmPos == 3)
		{
			location = spawnItems[2];
		}
		else if(rdmPos == 4)
		{
			location = spawnItems[3];
		}

		var clone = Instantiate(waterDrop, location.position, location.rotation) as SpriteRenderer;
		clone.name = "GoutteDeau";
		clone.sortingOrder = -1;


		yield return new WaitForSeconds(.5f);

		_canSpawn = false;
	}
	void OnTriggerEnter2D (Collider2D other)
	{
		if(other.collider2D.tag == "Player")
			Destroy(other.gameObject);
		
	}
	#endregion 
}
