using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Placer : MonoBehaviour {

	public GameObject[] prefabs;
	public TowerSelector[] selectors;
	public float[] values;

	int currentIndex = 0;
	bool active = false;

	// Use this for initialization
	void Start () {
		foreach (Transform t in transform) {
			t.gameObject.SetActive (false);
		}
	}

	public void Select(int index) {
		transform.GetChild (currentIndex).gameObject.SetActive (false);
		currentIndex = index;
		transform.GetChild (currentIndex).gameObject.SetActive (true);
		active = true;
	}

	public void Select(TowerSelector source) {
		int index = 0;
		foreach (TowerSelector s in selectors) {
			if (source == s) {
				Select (index);
			} else {
				s.Disable ();
			}
			++index;
		}
	}

	public void Disable() {
		transform.GetChild (currentIndex).gameObject.SetActive (false);
		active = false;
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;

		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 1000, 1 << 9)) {
			Vector3 position = hit.point;
			transform.position = new Vector3 (Mathf.Round ((position.x - 2.5f) / 5) * 5 + 2.5f, 0, Mathf.Round ((position.z - 2.5f) / 5) * 5 + 2.5f);
		}

		if (active) {
			if (Input.GetMouseButtonDown (0) && !EventSystem.current.IsPointerOverGameObject()) {
				if (values [currentIndex] <= Stats.GetCurrency ()) {
					if (!Physics.Raycast (transform.position + new Vector3 (0, -5, 0), Vector3.up, Mathf.Infinity, 1 << 10)) {
						GameObject newBuilding = GameObject.Instantiate (prefabs [currentIndex]);
						newBuilding.transform.position = transform.position;
						Stats.ChangeCurrency (-values [currentIndex]);
					}
				}
			}
		}
	}
}
