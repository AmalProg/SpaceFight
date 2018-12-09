using System;
using UnityEngine;
using nInterfaces;

public class Repulsion : MonoBehaviour
{
	public Spaceship user { get { return _user; } set { _user = value; } }
	public float maxRadius { get { return _maxRadius; } set { _maxRadius = value; transform.GetChild (0).localScale = new Vector3 (_maxRadius / 5f, _maxRadius / 5f, _maxRadius / 5f); } } 
	public float repulsionForce { get { return _repulsionForce; }  set { _repulsionForce = value; } } 
	public float repulsionTime { get { return _repulsionTime; }  
		set { 
			_repulsionTime = value; _repulsionTimer.SetResetTime(_repulsionTime); 
			_particleSystem.Pause ();
			ParticleSystem.MainModule particleSystemMain = _particleSystem.main;
			particleSystemMain.duration = _repulsionTime;
			ParticleSystem.MinMaxCurve lifetimeCurve = new ParticleSystem.MinMaxCurve (_repulsionTime, _repulsionTime * 1.2f);
			lifetimeCurve.mode = ParticleSystemCurveMode.TwoConstants;
			particleSystemMain.startLifetime = lifetimeCurve;
			particleSystemMain.startSpeed = 0.5f / _repulsionTime * 4f;
			_particleSystem.Play ();
		}} 

	private Spaceship _user;
	private float _repulsionForce;
	private float _actualRadius;
	private float _maxRadius;
	private Timer _repulsionTimer;
	private float _repulsionTime;
	private ParticleSystem _particleSystem;

	protected void Awake() {
		_repulsionForce = 10f;
		_maxRadius = 5f;
		_repulsionTime = 0.75f;
		_repulsionTimer = new Timer (_repulsionTime);
		_particleSystem = GetComponentInChildren<ParticleSystem> ();
	}

	public void Start() {
		maxRadius = _maxRadius;
		repulsionTime = _repulsionTime;
	}

	public void FixedUpdate() {
		if (!_repulsionTimer.Check ()) {
			_actualRadius = _maxRadius * _repulsionTimer.getElapsedTime () / _repulsionTime;
			transform.localScale = new Vector3 (_actualRadius, _actualRadius, _actualRadius);
		}
		else if (_particleSystem.isStopped) {
			Destroy (this.gameObject);
		}
	}

	public void OnTriggerEnter(Collider other) {
		if (other != null && !_repulsionTimer.Check ()) {
			Debug.Log (other);
			GameObject otherGO = other.gameObject;
			Rigidbody rb = otherGO.GetComponent<Rigidbody> ();
			if (rb) {
				Vector3 forceDir = otherGO.transform.position - transform.position;
				float force = (_maxRadius - forceDir.magnitude) / _maxRadius * _repulsionForce;
				rb.AddForceAtPosition(force * forceDir, transform.position, ForceMode.VelocityChange);
			}
		}
	}
}

