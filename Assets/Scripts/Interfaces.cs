﻿using UnityEngine;

namespace nInterfaces {
	public interface IDamageable {
		void TakeDamage(int d, GameObject caster);
		void Kill(GameObject caster);
	}

	public interface IHealable {
		void Heal(int h);
	}
}