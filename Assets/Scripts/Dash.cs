using System;
using System.Timers;
using System.Threading;
using UnityEngine;

public class Dash : Ability {

	private Timer _effectTimer;
	private Vector2 _userDirection;

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
				float elapsedTime = Time.fixedDeltaTime;
				float verTranslation = _userDirection.y * _user._speed * elapsedTime;
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

		Vector3 mousePos = Input.mousePosition;
		Vector2 centerPos = new Vector2 (Screen.width / 2, Screen.height / 2);
		_userDirection = new Vector2(mousePos.x - centerPos.x, mousePos.y - centerPos.y);
		_userDirection.Normalize ();
	}
}

public class Move : Ability {

	public Move(Spaceship user) : base(user) {
		_cooldown = 0.0f;
		_cooldownTimer.SetResetTime (_cooldown);
	}

	override public void Exec () {
		if (_onGoing) {
			float elapsedTime = Time.fixedDeltaTime;
			float verTranslation = Input.GetAxis("Vertical") * _user._speed * elapsedTime;
			float horTranslation = Input.GetAxis("Horizontal") * _user._speed * elapsedTime;

			_user.transform.Translate (horTranslation, 0, verTranslation);
		}
	}

	override protected void InternalUse () {
		_onGoing = true;
	}
}