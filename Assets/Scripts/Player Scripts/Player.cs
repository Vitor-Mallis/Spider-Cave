using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	Rigidbody2D rigidBody;
	Animator animator;

	float moveForce = 20f, jumpForce = 700f, maxVelocity = 5f;

	bool grounded = true;

	void Awake() {
		rigidBody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
	}

	void FixedUpdate() {
		MoveKeyboard();

		if (Input.GetKey (KeyCode.Space)) {
			Jump ();
		}
	}

	void MoveKeyboard() {
		float forceX = 0;
		float velocity = Mathf.Abs(rigidBody.velocity.x);
		Vector3 scale;

		float direction = Input.GetAxisRaw ("Horizontal");

		if (direction > 0) {
			scale = transform.localScale;
			scale.x = 1f;
			transform.localScale = scale;

			if (velocity < maxVelocity) {
				forceX += moveForce;
			}

			if (!grounded) {
				if (rigidBody.velocity.x < 0)
					forceX += moveForce * 1.5f;
			}

			animator.SetBool ("Walk", true);
		} else if (direction < 0) {
			scale = transform.localScale;
			scale.x = -1f;
			transform.localScale = scale;

			if (velocity < maxVelocity) {
				forceX -= moveForce;
			}

			if (!grounded) {
				if (rigidBody.velocity.x > 0)
					forceX -= moveForce * 1.5f;
			}

			animator.SetBool ("Walk", true);
		} else {
			animator.SetBool ("Walk", false);
		}

		rigidBody.AddForce (new Vector2 (forceX, 0));
	}

	void Jump() {
		if (grounded) {
			grounded = false;

			rigidBody.AddForce (new Vector2 (0, jumpForce));
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Ground")
			grounded = true;
		else if (collision.gameObject.tag == "Spider") {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == "Spider") {
			Destroy (gameObject);
		} else if (collision.tag == "Gem" || collision.tag == "Collectible") {
			Destroy (collision.gameObject);
			Door.instance.DecrementGems ();
		} else if (collision.tag == "Door") {
			if (Door.instance.isOpen)
				Debug.Log ("Level Finished!");
		} else if (collision.tag == "Bouncer") {
			if (grounded)
				grounded = false;

			Vector2 temp = rigidBody.velocity;
			temp.y = 0;
			rigidBody.velocity = temp;

			rigidBody.AddForce(new Vector2 (0, jumpForce * 1.2f));
			collision.GetComponent<Bouncer> ().Bounce();
		}
	}
}
