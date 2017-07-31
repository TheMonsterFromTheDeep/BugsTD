using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public float mouseThreshold = 0.3f;
	public float multiplier = 1;
	public float scrollMultiplier = 1;
	public float minimumY = 5;
	public float maximumY = 50;

	public float minX = -16;
	public float maxX = 16;
	public float minZ = -60;
	public float maxZ = 40;

	// Use this for initialization
	void Start () {
	
	}

	Vector2 currentSpeed = new Vector2();

	void Update () {
		if ((gameObject.transform.position.y > minimumY || Input.mouseScrollDelta.y < 0) &&
			(gameObject.transform.position.y < maximumY || Input.mouseScrollDelta.y > 0)) {
			gameObject.transform.position -= new Vector3 (0, Input.mouseScrollDelta.y * scrollMultiplier * Time.deltaTime, 0);
		}

		Vector3 mouseComp = Camera.main.ScreenToViewportPoint (Input.mousePosition);
		mouseComp -= new Vector3 (0.5f, 0.5f, 0);

		float totalMult = (multiplier * gameObject.transform.position.y) / (minimumY * mouseThreshold);

		Vector2 mouseSpeed = new Vector2();
		if (mouseComp.y > mouseThreshold) {
			mouseSpeed.y = (mouseComp.y - mouseThreshold)* totalMult;
		} else if (mouseComp.y < -mouseThreshold) {
			mouseSpeed.y = (mouseComp.y + mouseThreshold)* totalMult;
		}
		if (mouseComp.x > mouseThreshold) {
			mouseSpeed.x = (mouseComp.x - mouseThreshold) * totalMult;
		} else if (mouseComp.x < -mouseThreshold) {
			mouseSpeed.x = (mouseComp.x + mouseThreshold) * totalMult;
		}

		if (!Input.GetMouseButton (2))
			mouseSpeed = new Vector2(0, 0);

		currentSpeed += (mouseSpeed - currentSpeed) * 0.2f;

		gameObject.transform.localPosition += new Vector3 (currentSpeed.x, 0, currentSpeed.y) * Time.deltaTime;

		float x = gameObject.transform.localPosition.x;
		float z = gameObject.transform.localPosition.z;

		float aminx = minX * (maximumY / transform.position.y);
		float aminz = minZ;// * (maximumY / transform.position.y);
		float amaxx = maxX * (maximumY / transform.position.y);
		float amaxz = maxZ * (maximumY / transform.position.y);

		if (x < aminx)
			x = aminx;
		if (x > amaxx)
			x = amaxx;
		if (z < aminz)
			z = aminz;
		if (z > amaxz)
			z = amaxz;
		gameObject.transform.localPosition = new Vector3 (x, gameObject.transform.localPosition.y, z);
	}
}
