using System;
using UnityEngine;

public class CargoExplosion : AbilityMonoBehaviour
{
	private GameObject explosionPrefab;
	private Timer _explosionTimer;
	private float _explosionTime;

	void Awake() {
		explosionPrefab = Resources.Load ("Prefab/ExplosionPrefab") as GameObject;
		_explosionTime = 1.5f;
	}

	void Start() {
		_explosionTimer = new Timer (_explosionTime);
	}

	void FixedUpdate() {
		if (_explosionTimer.Check ()) {
			Explode ();
		}
	}

	void OnCollisionEnter(Collision collision) {
		Spaceship user = collision.gameObject.GetComponent<Spaceship> ();
		if (user == null || user != _user) {
			Explode ();
		}
	}

	void Explode() {
		GameObject exploObj = UnityEngine.Object.Instantiate(explosionPrefab, transform.position, new Quaternion ());
		exploObj.GetComponent<Explosion> ().user = _user;
		Destroy (this.gameObject);
	}
}

