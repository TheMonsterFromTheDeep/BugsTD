using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

	public interface Event {
		void OnShoot ();
	}

	public class DefaultEvent : Event {
		public void OnShoot() { }
	}

	public float range = 10;
	public float rate = 1;

	// Visual effect (mostly..) - moves projectile in front of gun when it spawns in
	public float forwardOffset = 0;

	private float lastFire;

	private float projectileVelocity;

	public GameObject projectile;

	public Event shootEvent = new DefaultEvent();

	bool targetClosest = false; //or closest to goal

	// Use this for initialization
	void Start () {
		lastFire = 0;
		projectileVelocity = projectile.GetComponent<Projectile> ().speed;
	}
	
	// Update is called once per frame
	void Update () {
		Collider[] targets = Physics.OverlapSphere (transform.position, range, 1 << 8);

		if (targets.Length == 0)
			return;

		if (Time.time < lastFire + rate)
			return;

		GameObject closest = null;

		if (targetClosest) {
			float distance = 0;

			foreach (Collider t in targets) {
				if (!t.CompareTag ("Projectile")) {
					closest = t.gameObject;
					distance = (t.transform.position - transform.position).sqrMagnitude;
					break;
				}
			}

			if (closest == null)
				return;

			foreach (Collider t in targets) {
				if (!t.CompareTag ("Projectile") && (t.transform.position - transform.position).sqrMagnitude < distance) {
					closest = t.gameObject;
					distance = (t.transform.position - transform.position).sqrMagnitude;
				}
			}
		} else {
			int lifetime = 0;

			foreach (Collider t in targets) {
				if (!t.CompareTag ("Projectile")) {
					closest = t.gameObject;
					lifetime = t.transform.parent.GetComponent<Bug> ().lifetime;//(t.transform.position - transform.position).sqrMagnitude;
					break;
				}
			}

			if (closest == null)
				return;

			foreach (Collider t in targets) {
				if (!t.CompareTag ("Projectile") && t.transform.parent.GetComponent<Bug> ().lifetime > lifetime) {// && (t.transform.position - transform.position).sqrMagnitude < distance) {
					closest = t.gameObject;
					lifetime = t.transform.parent.GetComponent<Bug> ().lifetime;
				}
			}
		}

		GameObject target = closest.transform.parent.gameObject;
		Vector3 velocity = target.GetComponent<Bug> ().currentSpeed;

		Vector3 position = target.transform.position;
		// Project position forward a little bit so projectile will actually impact
		position = ((position - transform.position).magnitude / projectileVelocity) * velocity + position;
			
		transform.eulerAngles = new Vector3 (-90, Mathf.Rad2Deg * Mathf.Atan2 (
			position.x - transform.position.x,
			position.z - transform.position.z), 
			0);

		GameObject proj = GameObject.Instantiate (projectile);
		proj.transform.position = transform.position;
		proj.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
		proj.transform.position += proj.transform.forward * forwardOffset;
		proj.GetComponent<Projectile>().targetY = position.y;

		lastFire = Time.time;

		shootEvent.OnShoot ();
	}
}
