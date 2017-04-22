using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWalker : MonoBehaviour {

	Rigidbody2D rigidBody;
	float speed = 2f;
	bool grounded;

	[SerializeField]
	GameObject startPos, endPos;

	void Awake() {
		rigidBody = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate() {
		Move ();
		CheckCollision ();
	}

	void Move() {
		Vector2 temp = rigidBody.velocity;
		temp.x = transform.localScale.x * speed;
		rigidBody.velocity = temp;
	}

	void CheckCollision() {
		grounded = Physics2D.Linecast(startPos.transform.position, endPos.transform.position, 1 << LayerMask.NameToLayer("Ground"));

		if (!grounded) {
			Vector3 temp = transform.localScale;
			temp.x = -temp.x;
			transform.localScale = temp;
		}
	}
}
