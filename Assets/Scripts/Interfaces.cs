using UnityEngine;

namespace nInterfaces {
	public interface IDamageable {
		void TakeDamage(int d, Spaceship caster);
		void Kill(Spaceship caster);
	}

	public interface IHealable {
		void Heal(int h);
	}
}