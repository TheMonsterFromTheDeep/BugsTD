using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public float maxHealth = 10;

	private float health;

	// Use this for initialization
	void Start () {
		health = maxHealth;
	}

	public void Change(float delta) {
		health += delta;
		if (health > maxHealth) {
			health = maxHealth;
		}
		if (health <= 0) {
			Stats.ChangeCurrency (Mathf.Clamp ((Random.Range (-2, 2) + maxHealth / 10), 1, Mathf.Infinity));
			GameObject.Destroy (gameObject);
		}
	}
}
