using System;
using System.Timers;
using UnityEngine;

public class Dash : Ability {

	private Timer _effectTimer;
	private Vector3 _userDirection;

	public Dash(Spaceship user) : base(user) {
		_cooldown = 2.0f;
		_cooldownTimer.SetResetTime (_cooldown);
		_cooldownTimer.AddTime (_cooldown);

		_effectTimer = new Timer (0.2f);
	}

	override public void Exec () {
		if (_onGoing) {
			if (_effectTimer.Check ()) {
				_onGoing = false;
				_user._speed = _user._baseSpeed;
			} else {
				float elapsedTime = Time.deltaTime;
				float verTranslation = _userDirection.z * _user._speed * elapsedTime;
				float horTranslation = _userDirection.x * _user._speed * elapsedTime;

				_user.transform.Translate (horTranslation, 0, verTranslation);
			}
		}
	}

	override protected void InternalUse () {
		if (!_onGoing)
			_effectTimer.Reset ();
		
		_onGoing = true;
		_user._speed = _user._baseSpeed * 5;
		_userDirection = _user.transform.forward;
	}
}

public class Move : Ability {

	public Move(Spaceship user) : base(user) {
		_cooldown = 0.0f;
		_cooldownTimer.SetResetTime (_cooldown);
	}

	override public void Exec () {
		if (_onGoing) {
			float elapsedTime = Time.deltaTime;
			float verTranslation = Input.GetAxis ("Vertical") * _user._speed * elapsedTime;
			float horTranslation = Input.GetAxis("Horizontal") * _user._speed * elapsedTime;

			_user.transform.Translate (horTranslation, 0, verTranslation);
		}
	}

	override protected void InternalUse () {
		_onGoing = true;
	}
}