using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour {

	// スコアラベルオブジェクト
	public UnityEngine.UI.Text ScoreLabel;

	// 制限時間ラベルオブジェクト
	public UnityEngine.UI.Text TimerLabel;

	// 残り弾薬表示オブジェクト
	public UnityEngine.UI.Text BulletLabel;

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

	// Boss Score
	public float BossLimitScore;

	// 制限時間
	public static float TimeLimit = 300;

	// スコア
	public static float Score = 0;

	// プレイヤーのHP
	public static float PlayerHP;

	// 残り弾数
	public static float BulletCount;

	// Max弾数
	private float MaxBulletCount;

	// Bullet Slider
	private Slider BulletSlider;

	// boss flag
	private bool BossFlag = false;

	// Use this for initialization
	void Awake () {
		// 初期スコアセット
		ScoreLabel.text = "Score:" + Score.ToString();
		// プレイヤーの初期HP取得
		PlayerHP = Player.GetComponent<PlayerStatus>().MaxHP;
		HealthBar.GetComponent<IconProgressBar>().CurrentValue = PlayerHP;
		// 残り時間セット
		TimerLabel.text = "Time:" + ((int)TimeLimit).ToString();
		// 一定時間後にMissionTextを消去
		// Destroy(MissionText, 3.0f);
		Destroy(MissionText);
		// Destroy(transform.FindChild("BlackImage").gameObject, 3.0f);
		Destroy(transform.FindChild("BlackImage").gameObject);
		// 弾数を表示
		BulletSlider = GameObject.Find("BulletSlider").GetComponent<Slider>();
		BulletSlider.maxValue = Player.GetComponent<PlayerStatus>().maxBulletCount;
		BulletSlider.value = ((int)Player.GetComponent<PlayerStatus>().bulletCount);
	}
	
	// Update is called once per frame
	void Update () {
		// スコアを表示
		ScoreLabel.text = "Score:" + Score.ToString();

		// プレイヤーのHPを表示
		HealthBar.GetComponent<IconProgressBar>().CurrentValue = Player.GetComponent<PlayerStatus>().HP;

		// プレイヤーの弾数を表示
		BulletSlider.maxValue = Player.GetComponent<PlayerStatus>().maxBulletCount;
		BulletSlider.value = ((int)Player.GetComponent<PlayerStatus>().bulletCount);

		// 制限時間を表示
		TimeLimit -= Time.deltaTime;
		if (TimeLimit < 0) {
			// 0秒以下は表示しない
			TimeLimit = 0;
		}
		TimerLabel.text = "Time:" + ((int)TimeLimit).ToString();

		// 制限時間が0になった時にゲーム終了処理
		if (TimeLimit == 0) {
			// TODO ゲーム終了処理
		}
	}

	// スコア加算処理
	public void addScore(float point) {
		Score += point;
		if (Score >= BossLimitScore && !BossFlag) {
			BossFlag = true;
			SceneManager.LoadScene ("boss_scene", LoadSceneMode.Single);
		}
	}

	// ダメージエフェクト処理
	IEnumerator monitorFlash(){
		DamageEffect.enabled = true;
		yield return new WaitForSeconds(0.1f);		// 処理を待機.
		iTween.ShakePosition (MainCamera, iTween.Hash("x",0.8f,"y",0.8f,"time",0.8f));
		DamageEffect.enabled = false;
	}
}
