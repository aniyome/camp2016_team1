using UnityEngine;
using System.Collections;

public class kappaTateMove : MonoBehaviour {
	private float speed = 1f;
	private float rotationSmooth = 1f;
	private Vector3 destination;
	private Vector3 targetPosition;
	private Vector3 startPosition;
	private bool m_yPlus = true;
	// Use this for initialization
	void Start () {
		startPosition = transform.localPosition;
		destination = new Vector3(startPosition.x, (startPosition.y + 10), startPosition.z);
	}
	
	// Update is called once per frame
	void Update () {
		if( m_yPlus ) {
			transform.localPosition += new Vector3(0f, 4f*Time.deltaTime, 0f);
			if( transform.localPosition.y >= (startPosition.y + 10) )
				m_yPlus = false;
		} else {
			transform.localPosition -= new Vector3(0f, 40f*Time.deltaTime, 0f);
			if( transform.localPosition.y <= (startPosition.y) )
				m_yPlus = true;
		}
	}
}
