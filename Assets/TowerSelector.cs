using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelector : MonoBehaviour {

	Color enabled, unenabled;
	bool toggle = false;

	public int index = 0;
	public Placer placer;

	Image image;

	// Use this for initialization
	void Start () {
		enabled = new Color (0.7f, 0.7f, 0.7f);
		unenabled = new Color (1f, 1f, 1f);

		image = GetComponent<Image> ();
		image.color = unenabled;
	}

	public void Click() {
		toggle = !toggle;
		image.color = toggle ? enabled : unenabled;

		if (toggle) {
			placer.Select (this);
		} else {
			placer.Disable ();
		}
	}

	public void Disable() {
		toggle = false;
		image.color = unenabled;
	}
}
