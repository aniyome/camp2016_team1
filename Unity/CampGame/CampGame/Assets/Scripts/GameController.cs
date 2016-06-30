using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	// スコアラベルオブジェクト
	public UnityEngine.UI.Text ScoreLabel;

	// プレイヤーHPラベルオブジェクト
	public UnityEngine.UI.Text PlayerHpLabel;

	// プレイヤーオブジェクト
	public GameObject Player;

	// スコア
	private float Score;

	// プレイヤーのHP
	private float PlayerHP;

	// Use this for initialization
	void Awake () {
		// 初期スコアセット
		Score = 0;
		// プレイヤーの初期HP取得
		PlayerHP = Player.GetComponent<PlayerStatus>().MaxHP;
	}
	
	// Update is called once per frame
	void Update () {
		// スコアを表示
		ScoreLabel.text = "SCORE:" + Score.ToString();

		// プレイヤーのHPを表示
		PlayerHpLabel.text = "HP：" +  Player.GetComponent<PlayerStatus>().HP;
	}

	// スコア加算処理
	public void addScore(float point) {
		Score += point;
	}
}
