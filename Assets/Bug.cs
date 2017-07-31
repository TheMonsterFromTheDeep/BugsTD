using UnityEngine;
using System.Collections;

public class Bug : MonoBehaviour {

	public float acceleration = 1;
	public float speed = 5;

	public Transform path;

	public int lifetime = 0;

	public Vector3 currentSpeed = new Vector3();

	public bool demo = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 delta = (path.position - transform.position);
		if (delta.sqrMagnitude > 0.1f) {
			transform.position += currentSpeed * Time.deltaTime;

			Vector3 wantedSpeed;

			if (acceleration / (2 * currentSpeed.magnitude) < currentSpeed.magnitude / delta.magnitude) {
				wantedSpeed = Vector3.zero;
			} else {
				wantedSpeed = delta.normalized * speed;
			}

			if (currentSpeed != wantedSpeed) {
				currentSpeed += (wantedSpeed - currentSpeed).normalized * acceleration * Time.deltaTime;
			}

			if (currentSpeed.sqrMagnitude > 0.1f) {
				transform.LookAt (transform.position + currentSpeed);
			}
		} else {
			transform.position = path.position;
			if (path.childCount > 0) {
				path = path.GetChild (0);
			} else {
				Destroy (gameObject);
				if(!demo) Stats.ChangeLife (-1);
			}
		}
		++lifetime;
	}
}
