using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour {

	public Spawner[] spawners;

	public float[] rates;
	public int[] counts;

	private int index = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void BeginWave() {
		foreach (Spawner s in spawners) {
			if (s.count > 0)
				return;
		}

		if (index < rates.Length) {
			foreach(Spawner s in spawners) {
				s.Begin (rates [index], counts [index]);
			}

			++index;
		} else {
			UnityEngine.SceneManagement.SceneManager.LoadScene ("menu");
		}
	}
}
