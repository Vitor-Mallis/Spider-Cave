using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour {

	Animator animator;

	void Awake() {
		animator = GetComponent<Animator> ();
	}

	public void Bounce() {
		StartCoroutine (PlayBounce ());
	}

	IEnumerator PlayBounce() {
		animator.SetTrigger ("Bounce");
		yield return new WaitForSeconds (.7f);
		animator.SetTrigger ("Bounce");
	}
}
