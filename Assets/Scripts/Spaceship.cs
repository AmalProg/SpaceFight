using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nInterfaces;

public class Spaceship : MonoBehaviour, IDamageable, IHealable {

	public GameObject lifeUIPrefab; 

	public int life { get { return _currentLife; } } 
	public bool isDead { get { return _isDead; } }

	public float _speed;
	public int _currentLife;
	public int _maxLife;
	private bool _isDead;

	protected LifeUi _LifeUI;

	protected void Awake() {
		GameObject lifeUIObj = Instantiate(lifeUIPrefab);
		_LifeUI = lifeUIObj.GetComponent<LifeUi>();
		_LifeUI.SetParent(this.gameObject);
	}

	protected void Start() {
		_LifeUI.transform.SetParent (GameController.lifeUICanvas.transform);

		_isDead = false;
	}

	protected void Update() {
		move ();
	}

	private void move() {
		float elapsedTime = Time.deltaTime;
		float verTranslation = Input.GetAxis("Vertical") * _speed * elapsedTime;
		float horTranslation = Input.GetAxis("Horizontal") * _speed * elapsedTime;

		transform.Translate (horTranslation, 0, verTranslation);
	}

	public virtual void TakeDamage(int d, GameObject caster) {
		_currentLife -= d;

		if(_LifeUI)
			_LifeUI.LifeChanged(_currentLife, _maxLife);

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
			_LifeUI.LifeChanged (_currentLife, _maxLife);
	}

	public virtual void Kill (GameObject caster) {
		_isDead = true;
	}

	void OnDestroy() {
		if(_LifeUI)
			Destroy (_LifeUI.gameObject);
	}
}