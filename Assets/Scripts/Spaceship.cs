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

	private Ability[] _abilities;
	private Ability _onGoingAbility;

	protected LifeUi _LifeUI;

	protected void Awake() {
		GameObject lifeUIObj = Instantiate(lifeUIPrefab);
		_LifeUI = lifeUIObj.GetComponent<LifeUi>();
		_LifeUI.SetParent(this.gameObject);

		_speed = _baseSpeed;
	}

	protected void Start() {
		_abilities = new Ability[4];
		_abilities [0] = new Move(this);
		_abilities [1] = new Cargo(this);
		_abilities [2] = new Dash(this);
		_abilities [3] = new SelfRepulsion(this);

		// default ability
		_onGoingAbility = _abilities [0];
		_abilities [0].Use ();

		_LifeUI.transform.SetParent (GameController.lifeUICanvas.transform);

		_isDead = false;
	}

	protected void FixedUpdate() {
		if ((_onGoingAbility != null && !_onGoingAbility.OnGoing()) || _onGoingAbility == null) {
			if (_abilities [0].Use ()) {
				_onGoingAbility = _abilities [0];
			}

			if (Input.GetKeyDown(KeyCode.A)) {
				if (_abilities [1].Use ()) {
					_onGoingAbility = _abilities [1];
				}
			}
			if (Input.GetKeyDown(KeyCode.Space)) {
				if (_abilities [2].Use ()) {
					_onGoingAbility = _abilities [2];
				}
			}
			if (Input.GetKeyDown(KeyCode.E)) {
				if (_abilities [3].Use ()) {
					_onGoingAbility = _abilities [3];
				}
			}
			if (Input.GetKeyDown(KeyCode.Alpha4)) {
				if (_abilities [4].Use ()) {
					_onGoingAbility = _abilities [4];
				}
			}
		}

		if (_onGoingAbility != null)
			_onGoingAbility.Exec ();
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