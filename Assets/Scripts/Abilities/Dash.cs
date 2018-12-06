using System;
using System.Timers;
using UnityEngine;

public class Dash : Ability {

	private Timer _effectTimer;
	private Vector2 _userDirection;

	public Dash(Spaceship user) : base(user) {
		_cooldown = 2.0f;
		_cooldownTimer.SetResetTime (_cooldown);
		_cooldownTimer.AddTime (_cooldown);

		_effectTimer = new Timer (0.06f);
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

				_userRB.velocity = new Vector3 (0, 0, 0);
				_userRB.AddForce (horTranslation, 0, verTranslation, ForceMode.VelocityChange);
			}
		}
	}

	override protected void InternalUse () {
		if (!_onGoing)
			_effectTimer.Reset ();
		
		_onGoing = true;
		_user._speed = _user._baseSpeed * 50;

		_userDirection = Tools.getLookingDirection ();
		_userDirection.Normalize ();
	}
}