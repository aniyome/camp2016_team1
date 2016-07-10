using UnityEngine;
using System.Collections;

public class tamagoClossMove : MonoBehaviour {



	// Use this for initialization
	void Start () {
		 
	}
	
	// Update is called once per frame
	void Update () {
		var x = 5 * Mathf.Sin(Time.time);
		var z = 5 * Mathf.Cos(Time.time);
		transform.position = new Vector3(x, 0, z);

	}
}
