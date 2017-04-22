using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderShooter : MonoBehaviour {

	[SerializeField]
	GameObject bullet;

	void Start() {
		StartCoroutine(Attack());
	}

	IEnumerator Attack() {
		yield return new WaitForSeconds(Random.Range(1.5f, 4f));

		Instantiate (bullet, new Vector3(transform.position.x, transform.position.y - 1, 0), Quaternion.identity);

		StartCoroutine (Attack ());
	}
}
