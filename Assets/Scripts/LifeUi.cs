using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nInterfaces;
using UnityEngine.UI;

public class LifeUi : MonoBehaviour
{
	public GameObject _parentObj;
	private Spaceship _parent;
	private float _size;
	private float _lifePercentage;
	private Vector3 _baseScale;
	private Image _image;
	private Text _text;

	void Start() {
		_lifePercentage = 1;
		_baseScale = new Vector3 (transform.localScale.x, transform.localScale.y, transform.localScale.z);
		_image = GetComponent<Image> ();
		_image.color = Color.green;
		_size = _parentObj.GetComponent<Renderer> ().bounds.size.z;
		_text = GetComponentInChildren<Text> ();

		LifeChanged ();
	}

	void Update() {
		Vector3 pos = _parentObj.transform.position;
		pos.z += _size;
		transform.position = pos;
	}

	public void LifeChanged() {
		if (_parent.life > 0)
			_lifePercentage = (float)_parent.life / (float)_parent.maxLife;
		else
			_lifePercentage = 0.0f;

		_text.text = _parent.life.ToString();

		if (_lifePercentage <= 0.25f)
			_image.color = Color.red;
		else if (_lifePercentage <= 0.5f)
			_image.color = new Color(255, 165, 0);
		else if (_lifePercentage <= 0.75f)
			_image.color = Color.yellow;

		transform.localScale = new Vector3(_baseScale.x, _baseScale.y * _lifePercentage,  _baseScale.z);
	}

	public void SetParent(GameObject parentObj) {
		_parentObj = parentObj;
		_parent = _parentObj.GetComponent<Spaceship> ();
	}
}
