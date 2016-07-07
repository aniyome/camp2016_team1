using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour {

	// 最大HP
	public float MaxHP = 100;

	// 最大MP
	public float MaxMP = 100;

	// HP
	public float HP;

	// MP
	public float MP;

	// 弾数
	public float maxBulletCount = 50.0f;

	// 弾数
	public float bulletCount = 50.0f;

	// Use this for initialization
	void Start () {
		// MaxHPを現在のHPに設定
		HP = MaxHP;
		// MaxMPを現在のMPに設定
		MP = MaxMP;
	}
	
	// Update is called once per frame
	void Update () {

	}

	// ダメージ計算処理
	public void Damage (float damage) {
		// HP減算処理
		HP = HP - damage;
		// HPが無くなった場合の処理
		if (HP <= 0) {
			// ゲームオーバー
			SceneManager.LoadScene ("game_over");
		}
	}
}
