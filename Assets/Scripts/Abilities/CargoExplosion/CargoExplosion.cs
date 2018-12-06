using System;
using UnityEngine;

public class CargoExplosion : MonoBehaviour
{
	public Spaceship user { get { return _user; } set { _user = value; } }

	private Spaceship _user;
	private GameObject explosionPrefab;
	private Timer _explosionTimer;

	void Awake() {
		explosionPrefab = Resources.Load ("Prefab/ExplosionPrefab") as GameObject;
	}

	void Start() {
		_explosionTimer = new Timer (1.5f);
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

