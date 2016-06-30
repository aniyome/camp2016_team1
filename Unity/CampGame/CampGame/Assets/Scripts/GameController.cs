using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	// スコアラベルオブジェクト
	public UnityEngine.UI.Text ScoreLabel;

	// プレイヤーHPラベルオブジェクト
	public UnityEngine.UI.Text PlayerHpLabel;

	// 制限時間ラベルオブジェクト
	public UnityEngine.UI.Text TimerLabel;

	// プレイヤーオブジェクト
	public GameObject Player;

	// 制限時間
	public float TimeLimit;

	// スコア
	private float Score;

	// プレイヤーのHP
	private float PlayerHP;

	// 残り時間
	private float RemainingTime;

	// Use this for initialization
	void Awake () {
		// 初期スコアセット
		Score = 0;
		ScoreLabel.text = "Score:" + Score.ToString();
		// プレイヤーの初期HP取得
		PlayerHP = Player.GetComponent<PlayerStatus>().MaxHP;
		PlayerHpLabel.text = "HP:" + PlayerHP.ToString ();
		// 残り時間セット
		RemainingTime = TimeLimit;
		TimerLabel.text = "Time:" + ((int)RemainingTime).ToString();
	}
	
	// Update is called once per frame
	void Update () {
		// スコアを表示
		ScoreLabel.text = "Score:" + Score.ToString();

		// プレイヤーのHPを表示
		PlayerHpLabel.text = "HP:" +  Player.GetComponent<PlayerStatus>().HP;

		// 制限時間を表示
		RemainingTime -= Time.deltaTime;
		TimerLabel.text = "Time:" + ((int)RemainingTime).ToString();
	}

	// スコア加算処理
	public void addScore(float point) {
		Score += point;
	}
}
