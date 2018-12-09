using System;
using UnityEngine;
using nInterfaces;

public class LinearProjectile : AbilityMonoBehaviour
{
	private int _damage;
	public float _speed;
	private Vector3 _initialPos;
	public float _maxDistTravel;
	protected Vector3 _direction;

	public Vector3 direction { get { return _direction; } set { _direction = value; _direction.Normalize (); } }

	protected void Awake() {
		_speed = 15.0f;
		_damage = 10;
		_initialPos = transform.position;
		_maxDistTravel = 15.0f;
		_direction = transform.up;
	}

	public void Start() {

	}

	public void Update() {
		float distTravelled = Vector3.Distance (_initialPos, transform.position);
		if(distTravelled > _maxDistTravel) {
			Destroy (this.transform.parent.gameObject);
		}

		Move();
	}

	public void Move() {
		transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
	}

	public void OnTriggerEnter(Collider other) {
		if (other != null) {
			GameObject otherGameObject = other.gameObject;
			IDamageable damageable = otherGameObject.GetComponent<IDamageable> ();
			if (damageable != null) {
				damageable.TakeDamage (_damage, _user);
			}
		}
		Destroy (this.transform.parent.gameObject); // destruction whatever is hit
	}
}

