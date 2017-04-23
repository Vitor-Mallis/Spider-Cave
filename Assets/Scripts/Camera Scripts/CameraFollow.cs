using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	[SerializeField]
	GameObject[] boundaries;

	float height, width;

	void Awake() {
		height = Camera.main.orthographicSize;
		width = height * Screen.width / Screen.height;
	}

	void Update() {
		if (Player.instance.transform.position.x > boundaries [0].transform.position.x + width &&
			Player.instance.transform.position.x < boundaries [1].transform.position.x - width) {

			Vector3 temp = transform.position;
			temp.x = Player.instance.GetPositionX ();
			transform.position = temp;
		}

		//if (Player.instance.GetPositionY () >= transform.position.y) {
			Vector3 tempY = transform.position;
			tempY.y = Player.instance.GetPositionY ();
			if (tempY.y < 0)
				tempY.y = 0;
			transform.position = tempY;
		//}
	
	}
}
