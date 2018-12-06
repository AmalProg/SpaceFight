using System;
using System.Timers;
using UnityEngine;

public abstract class Ability
{
	protected Spaceship _user;
	protected Rigidbody _userRB;
	protected bool _onGoing;
	protected bool _offCooldown;
	protected float _cooldown;
	protected Timer _cooldownTimer;

	protected Ability(Spaceship user) {
		_user = user;
		_userRB = user.GetComponent<Rigidbody> ();
		_offCooldown = true;
		_onGoing = false;
		_cooldownTimer = new Timer (_cooldown);
		_cooldownTimer.setAutoReset (true);
	}

	public bool Use() {
		_offCooldown = _cooldownTimer.Check ();
		if (_offCooldown) {
			InternalUse ();

			_offCooldown = false;
			return true;
		}
		return false;
	}

	public abstract void Exec ();

	public bool OnGoing() {
		return _onGoing;
	}

	protected abstract void InternalUse () ;
}

