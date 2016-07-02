using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterStatus : MonoBehaviour {

	// 最大HP
	public float MaxHP = 100;

	// 最大MP
	public float MaxMP = 100;

	// 攻撃力
	public float Attack = 10;

	// 倒した時のスコア
	public float Score = 10;

	// HP
	private float HP;

	// MP
	private float MP;

	// HPバー
	private Slider slider;

	// HPバーの値
	private float sliderValue = 1;

	// キャンパスコントローラ
	private GameObject CanvasController;

	// Use this for initialization
	void Start () {
		// MaxHPを現在のHPに設定
		HP = MaxHP;

		// HPバーのオブジェクト取得
		slider = GetComponentInChildren<Slider>();

		// キャンパスコントローラ
		CanvasController = GameObject.Find("Canvas");
	}
	
	// Update is called once per frame
	void Update () {

	}

	// ダメージ計算処理
	public void Damage (float damage) {
		// HP減算処理
		HP = HP - damage;

		// HPバーの値減少処理
		slider.value = HP / MaxHP;

		// HPが無くなった場合の処理
		if (HP <= 0) {
			CanvasController.SendMessage("addScore" , Score);
			Destroy(gameObject);
		}
	}

	// 接触判定(接触オブジェクト)
	void OnTriggerEnter (Collider other) {

		// EnemyならDamage
		if (other.tag == "Player") {
			// (関数名, 値)
			other.SendMessage("Damage", Attack);
			CanvasController.SendMessage ("monitorFlash");
			Destroy(gameObject);
		}
	}
}
