using UnityEngine;
using System.Collections;

public class NetaSushiAnimation : BaseEnemyAnimation {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		iTween.PunchPosition(this.gameObject, iTween.Hash(
			"y", 10,
			"time", 3.0f)
		);
	}
}
