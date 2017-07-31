using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelRotate : MonoBehaviour, Shoot.Event {

	// Use this for initialization
	void Start () {
		transform.parent.GetComponent<Shoot> ().shootEvent = this;
	}

	public void OnShoot() {
		transform.localEulerAngles += new Vector3 (0, 10, 0);
	}
}
