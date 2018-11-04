using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nInterfaces;

public class Spaceship : MonoBehaviour, IDamageable, IHealable {

	public GameObject lifeUIPrefab; 

	public int life { get { return _currentLife; } } 
	public int maxLife { get { return _maxLife; } } 
	public bool isDead { get { return _isDead; } }

	public float _baseSpeed;
	public float _speed;
	public int _currentLife;
	public int _maxLife;
	private bool _isDead;

	private Vector3 _previousPos;
	private float _hitBoxradius;

	private Ability[] _abilities;
	private Ability _onGoingAbility;

	protected LifeUi _LifeUI;

	protected void Awake() {
		GameObject lifeUIObj = Instantiate(lifeUIPrefab);
		_LifeUI = lifeUIObj.GetComponent<LifeUi>();
		_LifeUI.SetParent(this.gameObject);

		_speed = _baseSpeed;
		_hitBoxradius = 1;
	} 

	protected void Start() {
		_abilities = new Ability[4];
		_abilities [0] = new Move(this);
		_abilities [1] = new Dash(this);

		// default ability
		_onGoingAbility = _abilities [0];
		_abilities [0].Use ();

		_previousPos = transform.position;

		_LifeUI.transform.SetParent (GameController.lifeUICanvas.transform);

		_isDead = false;
	}

	protected void Update() {
		if (Input.GetButtonDown ("Jump")) {
			if (_abilities [1].Use ()) {
				_onGoingAbility = _abilities [1];
			}
		}

		if (_onGoingAbility != null) {
			if (!_onGoingAbility.OnGoing()) {
				if (_abilities [0].Use ()) {
					_onGoingAbility = _abilities [0];
				}
			}

			_onGoingAbility.Exec ();
		}

		collisionTest ();
		_previousPos = transform.position;
	}

	private void collisionTest() {
		RaycastHit hit1;
		int layerMask = 1 << 8; // "Map"
		Vector3 newPos = transform.position;
		Vector3 direction = newPos - _previousPos;

		if (Physics.Raycast (_previousPos, direction, out hit1, direction.magnitude + _hitBoxradius,
				layerMask, QueryTriggerInteraction.Collide)) {
			float lengthAB = ((direction * (1 + _hitBoxradius)) - (hit1.point - _previousPos)).magnitude;
			float angleA = Vector3.Angle (-direction, hit1.normal);
			float normalLength = Mathf.Sin (angleA) * lengthAB;
			print (normalLength);
			transform.position = newPos + hit1.normal * (normalLength);
		}
	}

	public virtual void TakeDamage(int d, GameObject caster) {
		_currentLife -= d;

		if(_LifeUI)
			_LifeUI.LifeChanged();

		if(_currentLife <= 0) {
			_currentLife = 0;

			Kill(caster);
		}
	}

	public virtual void Heal(int h) { 
		_currentLife += h; 

		if (_currentLife > _maxLife)
			_currentLife = _maxLife;
		
		if(_LifeUI)
			_LifeUI.LifeChanged ();
	}

	public virtual void Kill (GameObject caster) {
		_isDead = true;
	}

	void OnDestroy() {
		if(_LifeUI)
			Destroy (_LifeUI.gameObject);
	}
}