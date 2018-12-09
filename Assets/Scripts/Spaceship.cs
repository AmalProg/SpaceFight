using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nInterfaces;

public class Spaceship : MonoBehaviour, IDamageable, IHealable {

	public Ability[] abilities { get { return _abilities; } }
	public int life { get { return _currentLife; } } 
	public int maxLife { get { return _maxLife; } } 
	public bool isDead { get { return _isDead; } }

	public GameObject lifeUIPrefab;

	public float _baseSpeed;
	public float _speed;
	public int _currentLife;
	public int _maxLife;
	private bool _isDead;

	private Ability[] _abilities;
	private Ability _onGoingAbility;
	private Ability _moveAbility;

	protected LifeUi _LifeUI;

	protected void Awake() {
		GameObject lifeUIObj = Instantiate(lifeUIPrefab);
		_LifeUI = lifeUIObj.GetComponent<LifeUi>();
		_LifeUI.SetParent(this.gameObject);

		_speed = _baseSpeed;
	}

	protected void Start() {
		_moveAbility = new Move(this);
		_moveAbility.Use ();
		_abilities = new Ability[4];
		_abilities [0] = new PrimaryFire(this);
		_abilities [1] = new Cargo(this);
		_abilities [2] = new Dash(this);
		_abilities [3] = new SelfRepulsion(this);

		_LifeUI.transform.SetParent (GameController.lifeUICanvas.transform);

		_isDead = false;
	}

	protected void FixedUpdate() {
		if ((_onGoingAbility != null && !_onGoingAbility.OnGoing()) || _onGoingAbility == null) {
			if (Input.GetButton("Mouse1")) {
				if (_abilities [0].Use ()) {
					_onGoingAbility = _abilities [0];
				}
			}
			if (Input.GetMouseButton(1)) {
				if (_abilities [1].Use ()) {
					_onGoingAbility = _abilities [1];
				}
			}
			if (Input.GetKey(KeyCode.Space)) {
				if (_abilities [2].Use ()) {
					_onGoingAbility = _abilities [2];
				}
			}
			if (Input.GetKey(KeyCode.E)) {
				if (_abilities [3].Use ()) {
					_onGoingAbility = _abilities [3];
				}
			}
		}

		if (_onGoingAbility != null) {
			_onGoingAbility.Exec ();

			if (!_onGoingAbility.OnGoing ())
				_moveAbility.Exec ();
		} else {
			_moveAbility.Exec ();
		}
	}

	public virtual void TakeDamage(int d, Spaceship caster) {
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

	public virtual void Kill (Spaceship caster) {
		_isDead = true;
	}

	void OnDestroy() {
		if(_LifeUI)
			Destroy (_LifeUI.gameObject);
	}
}