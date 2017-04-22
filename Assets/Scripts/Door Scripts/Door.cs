using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public static Door instance;

	Animator animator;
	int gemCount;

	[HideInInspector]
	public bool isOpen;

	void Awake() {
		if (instance == null)
			instance = this;

		animator = GetComponent<Animator> ();
	}

	void Start() {
		gemCount = GameObject.FindGameObjectsWithTag ("Gem").Length;
	}

	public void DecrementGems() {
		gemCount--;

		if (gemCount <= 0) {
			Open ();
		}
	}

	void Open() {
		animator.Play ("Open");
		isOpen = true;
	}
}
