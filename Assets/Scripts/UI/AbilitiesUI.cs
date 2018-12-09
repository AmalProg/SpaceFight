using System;
using UnityEngine;

public class AbilitiesUI : MonoBehaviour
{
	public Ability[] abilities { get { return _abilities; } set { _abilities = value; Init (); } }

	public GameObject _cooldownUIPrefab;

	private Ability[] _abilities;

	void Awake() {
	}

	void Start() {
	}

	void Update() {
	}

	void Init() {
		float startXPos = Screen.width * 1 / 8 + _cooldownUIPrefab.GetComponent<RectTransform>().sizeDelta.x;
		float width = Screen.width * 3 / 4;
		float spacing = width / _abilities.Length - 1;
		float yPos = Screen.height * 1 / 12;
		for (uint i = 0; i < _abilities.Length; i++) {
			GameObject cdUIObj = Instantiate (_cooldownUIPrefab, new Vector3 (startXPos + i * spacing, yPos), new Quaternion (), transform);
			CooldownUI cdUI = cdUIObj.GetComponent<CooldownUI> ();
			cdUI.ability = _abilities [i];
		}
	}
}

