using UnityEngine;

public class SelfRepulsion : Ability {

	private GameObject repulsionPrefab;

	public SelfRepulsion(Spaceship user) : base(user) {
		_cooldown = 1.0f;
		_cooldownTimer.SetResetTime (_cooldown);
		repulsionPrefab = Resources.Load ("Prefab/RepulsionPrefab") as GameObject;
	}

	override public void Exec () {
		if (_onGoing) {
			GameObject repuObj = UnityEngine.Object.Instantiate(repulsionPrefab, 
				_user.transform.position, new Quaternion(), _user.transform);
			repuObj.GetComponent<Repulsion> ().user = _user;

			_onGoing = false;
		}
	}

	override protected void InternalUse () {
		_onGoing = true;
	}
}


