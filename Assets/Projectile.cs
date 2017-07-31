using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float speed = 20;

	public float minDamage = 1;
	public float maxDamage = 3;

	public float targetY;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * speed * Time.deltaTime;
		transform.position += new Vector3 (0, (targetY - transform.position.y) * 0.1f, 0);
	}

	void OnTriggerEnter(Collider collider) {
		collider.transform.parent.GetComponent<Health> ().Change (-Random.Range (minDamage, maxDamage));
		GameObject.Destroy (gameObject);
	}
}
