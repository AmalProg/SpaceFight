using System;
using UnityEngine;
using nInterfaces;

public class Explosion : AbilityMonoBehaviour {

	public int damage { get { return _damage; } set { _damage = value; } } 
	public float maxRadius { get { return _maxRadius; } set { _maxRadius = value; transform.GetChild (0).localScale = new Vector3 (_maxRadius / 5f, _maxRadius / 5f, _maxRadius / 5f); } } 
	public float explosionForce { get { return _explosionForce; }  set { _explosionForce = value; } } 
	public float explosionTime { get { return _explosionTime; }  
		set { 
			_explosionTime = value; _explosionTimer.SetResetTime(_explosionTime); 
			_particleSystem.Pause ();
			ParticleSystem.MainModule particleSystemMain = _particleSystem.main;
			particleSystemMain.duration = _explosionTime;
			ParticleSystem.MinMaxCurve lifetimeCurve = new ParticleSystem.MinMaxCurve (_explosionTime, _explosionTime * 1.2f);
			lifetimeCurve.mode = ParticleSystemCurveMode.TwoConstants;
			particleSystemMain.startLifetime = lifetimeCurve;
			particleSystemMain.startSpeed = 0.5f / _explosionTime * 4f;
			_particleSystem.Play ();
		}} 

	private int _damage;
	private float _explosionForce;
	private float _actualRadius;
	private float _maxRadius;
	private Timer _explosionTimer;
	private float _explosionTime;
	private ParticleSystem _particleSystem;

	protected void Awake() {
		_damage = 30;
		_explosionForce = 30f;
		_maxRadius = 5f;
		_explosionTime = 0.2f;
		_explosionTimer = new Timer (_explosionTime);
		_particleSystem = GetComponentInChildren<ParticleSystem> ();
	}

	public void Start() {
		maxRadius = _maxRadius;
		explosionTime = _explosionTime;
	}

	public void FixedUpdate() {
		if (!_explosionTimer.Check ()) {
			_actualRadius = _maxRadius * _explosionTimer.getElapsedTime () / _explosionTime;
			transform.localScale = new Vector3 (_actualRadius, _actualRadius, _actualRadius);
		}
		else if (_particleSystem.isStopped) {
			Destroy (this.gameObject);
		}
	}

	public void OnTriggerEnter(Collider other) {
		if(other != null && !_explosionTimer.Check()) {
			Debug.Log (other);
			GameObject otherGO = other.gameObject;
			Rigidbody rb = otherGO.GetComponent<Rigidbody> ();
			if (rb) {
				Vector3 forceDir = otherGO.transform.position - transform.position;
				float force = (_maxRadius - forceDir.magnitude) / _maxRadius * _explosionForce;
				rb.AddForceAtPosition(force * forceDir, transform.position, ForceMode.VelocityChange);
			}

			IDamageable entity = otherGO.GetComponent<IDamageable> ();
			if (entity != null) {
				entity.TakeDamage (_damage, _user);
			}
		}
	}
}