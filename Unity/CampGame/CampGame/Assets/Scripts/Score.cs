using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class Score : MonoBehaviour {

	// 現在のスコア
	private float score = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Text>().text = "Score:" + score.ToString();
	}

	// スコア加算
	public void ScoreUp (float point) {
		score = point;
	}
}
