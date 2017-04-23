using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public void OnPointerDown(PointerEventData data) {
		if (gameObject.name == "Left Arrow") {
			Player.instance.SetMoveLeft (true);
		} else if (gameObject.name == "Right Arrow") {
			Player.instance.SetMoveLeft (false);
		}
	}

	public void OnPointerUp(PointerEventData data) {
		Player.instance.StopMoving ();
	}
}
