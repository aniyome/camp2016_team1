using UnityEngine;
using System.Collections;

public class UniMove : MonoBehaviour {

	private Vector3 _dir;
	private float _speed;

	// Use this for initialization
	void Start () {
		float x = Random.Range(-3, 3);
		float y = Random.Range(-3, 3);
		gameObject.transform.localPosition = new Vector3(x, y, 0);

		_speed = Random.Range(1, 9) / 10f;
		x = Random.Range(-3, 3);
		y = Random.Range(-3, 3);
		_dir = new Vector3(x, y, 0);
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Translate(_dir * _speed);
	}
}
