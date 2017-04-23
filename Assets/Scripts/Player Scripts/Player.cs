using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public static Player instance;

	Rigidbody2D rigidBody;
	Animator animator;

	float moveForce = 20f, jumpForce = 700f, maxVelocity = 5f;

	bool grounded = true;
	bool moveLeft, moveRight;

	void Awake() {
		if (instance == null)
			instance = this;

		rigidBody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
	}

	public void SetMoveLeft(bool left) {
		moveLeft = left;
		moveRight = !moveLeft;
	}

	public void StopMoving() {
		moveLeft = moveRight = false;
		animator.SetBool ("Walk", false);
	}

	public float GetPositionX() {
		return transform.position.x;
	}

	public float GetPositionY() {
		return transform.position.y;
	}

	void FixedUpdate() {
		#if UNITY_EDITOR
			MoveKeyboard();

			if (Input.GetKeyDown (KeyCode.Space)) {
				Jump ();
			}
		#else
			MoveJoystick();
		#endif
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
		
	void MoveJoystick() {
		float forceX = 0;
		float velocity = Mathf.Abs(rigidBody.velocity.x);
		Vector3 scale;

		if (moveLeft) {
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
		} else if(moveRight) {
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
		}

		rigidBody.AddForce (new Vector2 (forceX, 0));
	}

	public void Jump() {
		if (grounded) {
			grounded = false;

			rigidBody.AddForce (new Vector2 (0, jumpForce));
		}
	}

	public void Bounce() {
		if (grounded) {
			grounded = false;
		}
			Vector2 temp = rigidBody.velocity;
			temp.y = 0;
			rigidBody.velocity = temp;

			rigidBody.AddForce (new Vector2 (0, jumpForce * 1.5f));
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Ground") {
			if((collision.gameObject.transform.position.y + collision.gameObject.GetComponent<BoxCollider2D>().size.y / 2) < (transform.position.y + gameObject.GetComponent<BoxCollider2D>().size.y / 2))
				grounded = true;
		} else if (collision.gameObject.tag == "Spider") {
			GameplayController.instance.GameOver ();
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == "Spider") {
			GameplayController.instance.GameOver ();
			Destroy (gameObject);
		} else if (collision.tag == "Gem") {
			Destroy (collision.gameObject);
			Door.instance.DecrementGems ();
		} else if (collision.tag == "Door") {
			if (Door.instance.isOpen) {
				GameplayController.instance.GameOver ();
			}
		}
	}
}
