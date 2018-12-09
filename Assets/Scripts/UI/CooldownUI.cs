using System;
using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
	public Ability ability { get { return _ability; } set { _ability = value; } }

	private Ability _ability;
	private Image _reloadImage;

	void Awake() {
		_reloadImage = GetComponentsInChildren<Image> () [1];
	}

	void Start() {

	}

	void Update() {
		_reloadImage.fillAmount = 1.0f - Math.Min(_ability.elapsedTime / _ability.cooldown, 1.0f);
	}
}

