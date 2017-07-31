using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MacroButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ToGame() {
		SceneManager.LoadScene ("game");
	}

	public void ToMenu() {
		SceneManager.LoadScene ("menu");
	}
}
