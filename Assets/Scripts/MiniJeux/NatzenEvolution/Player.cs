using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private ManagerNatzenEvolution _manager;

	private int _speed; // Vitesse de déplacement du pot de fleur
	private int _waterDropSmall = 40; // Correspond au point que donne la goutte suivant la taille de la goutte
	private int _waterDropMiddle = 70;
	private int _waterDropBig = 100;

	// Use this for initialization
	void Start () {
		_manager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ManagerNatzenEvolution>();
		_speed = 8;
	}
	
	// Update is called once per frame
	void Update () {
		Move();
	}

	void Move ()
	{
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Translate(new Vector2(-_speed, 0) * Time.deltaTime);
		}
		else if(Input.GetKey(KeyCode.RightArrow))
		{
			transform.Translate(new Vector2(_speed, 0) * Time.deltaTime);
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		switch(other.GetComponent<Collider2D>().name)
		{
			case "GoutteDeauT1":
				_manager.score += _waterDropSmall;
				Destroy(other.gameObject);
				break;

			case "GoutteDeauT2":
				_manager.score += _waterDropMiddle;
				Destroy(other.gameObject);
				break;

			case "GoutteDeauT3":
				_manager.score += _waterDropBig;
				Destroy(other.gameObject);
				break;
		}
	}
}
