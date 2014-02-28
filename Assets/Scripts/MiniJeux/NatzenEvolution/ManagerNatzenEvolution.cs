using UnityEngine;
using System.Collections;

public class ManagerNatzenEvolution : MonoBehaviour {
	#region Attributs
	//public SpriteRenderer[] plantEvolution; // Contient toute les textures de la plante
	public SpriteRenderer[] waterDrops; // Contient les différents types de gouttes d'eau
	//public SpriteRenderer[] otherItems; // Contient les différents obstacle à éviter

	public Transform[] spawnItems; // Spawner

	public int nbEvolution; // Nombre de texture que contiendra le tableau d'évolution de la plante
	public int score; // Score du joueur !!! NE PAS MODIFIER DANS L'INSPECTOR !!!

	public SpriteRenderer flowerPot; // Pot de fleur
	//public SpriteRenderer waterDrop; // Goutte d'eau

	public GUIText timeText;
	public GUIText scoreText;

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
		score = 0;
		_time = 30.0f;
		_spawnerTime = 0.0f;
		_waitTimeForSpawn = 1.0f;

		InitPlant();

	}

	void InitPlant ()
	{
		nbEvolution = 10;
		//plantEvolution = new SpriteRenderer[nbEvolution];
		flowerPot = Instantiate(flowerPot, new Vector3(0, -2.5f, 0), Quaternion.Euler(0, 0, 0)) as SpriteRenderer;

	}

	// Update is called once per frame
	void Update () 
	{
		DisplayText();

		_time -= Time.deltaTime;

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

		#region Reduction du temps de spawn des gouttes
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
		#endregion

	}

	void DisplayText ()
	{
		scoreText.text = "Score : " + score;

		timeText.text = "Temps : " + Mathf.Floor(_time);

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

		int rdmSize = Random.Range(1, 4);
		SpawnWaterDrop(rdmSize, location);

		yield return new WaitForSeconds(.5f);

		_canSpawn = false;
	}

	void SpawnWaterDrop (int size, Transform location)
	{
		if(size == 1)
		{
			var clone = Instantiate(waterDrops[0], location.position, location.rotation) as SpriteRenderer;
			clone.name = "GoutteDeauT1";
			clone.sortingOrder = -1;
		}
		else if(size == 2)
		{
			var clone = Instantiate(waterDrops[1], location.position, location.rotation) as SpriteRenderer;
			clone.name = "GoutteDeauT2";
			clone.sortingOrder = -1;
		}
		else if(size == 3)
		{
			var clone = Instantiate(waterDrops[2], location.position, location.rotation) as SpriteRenderer;
			clone.name = "GoutteDeauT3";
			clone.sortingOrder = -1;
		}

	}

	#endregion 
}
