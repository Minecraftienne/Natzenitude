using UnityEngine;
using System.Collections;

public class Natzen : MonoBehaviour {

	private bool isGrounded;

	// Update is called once per frame
	void Update () {

		Debug.Log(isGrounded);
		
		if (Input.GetKey(KeyCode.LeftArrow))
			transform.Translate(-Vector2.right * 8 * Time.deltaTime);
		else if (Input.GetKey(KeyCode.RightArrow))
			transform.Translate(Vector2.right * 8 * Time.deltaTime);
	}

	void FixedUpdate () {
		if (isGrounded == true) {

			rigidbody2D.AddForce (new Vector2(0f, 500));
			gameObject.layer = 9; //NatzenInAir Layer
		}

		if (rigidbody2D.velocity.y < 0){
			gameObject.layer = 8; //Natzen layer
		}
	}

	void OnCollisionEnter2D (Collision2D other) {

		if (other.gameObject.tag == "Plateforme")
			isGrounded = true;
	}

	void OnCollisionExit2D (Collision2D other) {

		if (other.gameObject.tag == "Plateforme")
			isGrounded = false;
	}
}
