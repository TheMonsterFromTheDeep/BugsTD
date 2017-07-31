using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stats : MonoBehaviour {

	public Text currency;
	public Text life;

	private float _currency = 2000, _life = 25;

	private static Stats stats;

	// Use this for initialization
	void Start () {
		stats = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void ChangeCurrency(float delta) {
		stats._currency += delta;
		stats._currency = Mathf.Ceil (stats._currency);
		stats.currency.text = "" + stats._currency;
	}

	public static float GetCurrency() {
		return stats._currency;
	}

	public static void ChangeLife(float delta) {
		stats._life += delta;
		stats.life.text = "" + stats._life;
		if (stats._life <= 0) {
			SceneManager.LoadScene ("menu");
		}
	}
}
