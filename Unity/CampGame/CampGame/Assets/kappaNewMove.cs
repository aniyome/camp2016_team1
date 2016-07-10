using UnityEngine;
using System.Collections;

public class kappaNewMove : MonoBehaviour {
	private float speed = 1f;
	private float rotationSmooth = 1f;
	private Vector3 destination;
	private Vector3 targetPosition;
	private Vector3 startPosition;
	private bool m_yPlus = true;
	public float upRange = 10;
	public float timespan = 0.0f;
	public float time;
	// Use this for initialization
	void Start () {
		
		startPosition = transform.localPosition;
		destination = new Vector3(startPosition.x, (startPosition.y + upRange), startPosition.z);
	}

	// Update is called once per frame
	void Update () {
		timespan = timespan + Time.deltaTime;
		if (timespan >= time) {
		    if( m_yPlus ) {
     			transform.localPosition += new Vector3(0f, 4f*Time.deltaTime, 0f);
	    		if( transform.localPosition.y >= (startPosition.y + upRange) )
		    		m_yPlus = false;
    		} else {
	    		transform.localPosition -= new Vector3(0f, 40f*Time.deltaTime, 0f);
		    	if( transform.localPosition.y <= (startPosition.y) )
			    	m_yPlus = true;
	    	}
		}
	}
}
