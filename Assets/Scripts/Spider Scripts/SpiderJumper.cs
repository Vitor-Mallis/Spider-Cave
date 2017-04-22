using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderJumper : MonoBehaviour {

	float jumpForce;

	Rigidbody2D rigidBody;
	Animator animator;

	void Awake() {
		rigidBody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
	}

	void Start () {
		StartCoroutine (Attack ());
	}

	IEnumerator Attack() {
		yield return new WaitForSeconds (Random.Range (1.5f, 4f));

		jumpForce = Random.Range (400f, 700f);

		rigidBody.AddForce (new Vector2 (0, jumpForce));
		animator.SetBool ("Jump", true);

		StartCoroutine (Attack ());
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Ground")
			animator.SetBool ("Jump", false);
	}
}
