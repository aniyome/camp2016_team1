using UnityEngine;
using System.Collections;

public class tamagoClossMove : MonoBehaviour {

	private Vector3 _dir;
	private float _speed;

	// Use this for initialization
	void Start () {
		float x = Random.Range(-3, 3);
		float y = Random.Range(-3, 3);
		gameObject.transform.localPosition = new Vector3(x, y, 0);
	}
	
	// Update is called once per frame
	void Update () {
		//var x = 5 * Mathf.Sin(Time.time);
		//var z = 5 * Mathf.Cos(Time.time);
		//transform.position = new Vector3(x, 0, z);
		gameObject.transform.Translate(_dir * _speed);
	}
}
