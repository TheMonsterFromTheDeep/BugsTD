using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject bug;
	public Transform path;

	float nextTime;

	public float rate;
	public int count;

	// Use this for initialization
	void Start () {
		
	}

	public void Begin(float rate_, int count_) {
		rate = rate_;
		count = count_;
		nextTime = Time.time + rate + Random.Range (0, 1);
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= nextTime && count > 0) {
			--count;

			GameObject newObject = Instantiate (bug);
			newObject.transform.position = path.position;
			newObject.GetComponent<Bug> ().path = path.GetChild(0);

			nextTime = Time.time + rate + Random.Range (0, 1);
		}
	}
}
