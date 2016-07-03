using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasController : MonoBehaviour {

	// スコアラベルオブジェクト
	public UnityEngine.UI.Text ScoreLabel;

	// 制限時間ラベルオブジェクト
	public UnityEngine.UI.Text TimerLabel;

	// ダメージエフェクト
	public UnityEngine.UI.Image DamageEffect;

	// プレイヤーオブジェクト
	public GameObject Player;

	// メインカメラオブジェクト
	public GameObject MainCamera;

	// ハートマークのHPバー
	public GameObject HealthBar;

	// 寿司を倒せのテキスト
	public GameObject MissionText;

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
//		ScoreLabel.text = "Score:" + Score.ToString();
		// プレイヤーの初期HP取得
		PlayerHP = Player.GetComponent<PlayerStatus>().MaxHP;
		HealthBar.GetComponent<IconProgressBar>().CurrentValue = PlayerHP;
		// 残り時間セット
		RemainingTime = TimeLimit;
		TimerLabel.text = "Time:" + ((int)RemainingTime).ToString();
		// 一定時間後にMissionTextを消去
		Destroy(MissionText, 5.0f);
	}
	
	// Update is called once per frame
	void Update () {
		// スコアを表示
//		ScoreLabel.text = "Score:" + Score.ToString();

		// プレイヤーのHPを表示
		HealthBar.GetComponent<IconProgressBar>().CurrentValue = Player.GetComponent<PlayerStatus>().HP;

		// 制限時間を表示
		RemainingTime -= Time.deltaTime;
		if (RemainingTime < 0) {
			// 0秒以下は表示しない
			RemainingTime = 0;
		}
		TimerLabel.text = "Time:" + ((int)RemainingTime).ToString();

		// 制限時間が0になった時にゲーム終了処理
		if (RemainingTime == 0) {
			// TODO ゲーム終了処理
		}
	}

	// スコア加算処理
	public void addScore(float point) {
		Score += point;
	}

	// ダメージエフェクト処理
	IEnumerator monitorFlash(){
		DamageEffect.enabled = true;
		yield return new WaitForSeconds(0.1f);		// 処理を待機.
		iTween.ShakePosition (MainCamera, iTween.Hash("x",0.8f,"y",0.8f,"time",0.8f));
		DamageEffect.enabled = false;
	}
}
