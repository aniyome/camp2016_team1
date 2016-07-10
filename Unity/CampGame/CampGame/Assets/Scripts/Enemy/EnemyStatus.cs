﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour {

	// 最大HP
	public float MaxHP = 100;

	// 最大MP
	public float MaxMP = 100;

	// 攻撃力
	public float Attack = 10;

	// 倒した時のスコア
	public float Score = 10;

	// ダメージを受けた時のエフェクト
	public GameObject DamageEffect;

	// 消滅する時のエフェクト
	public GameObject DestroyEffect;

	// HP
	private float HP;

	// MP
	private float MP;

	// キャンパスコントローラ
	private GameObject CanvasController;

	// HitPointSlider
	private Slider HitPointSlider;

  // se
  public AudioClip SE;

	// Use this for initialization
	void Start () {
		// MaxHPを現在のHPに設定
		HP = MaxHP;

		// キャンパスコントローラ
		CanvasController = GameObject.Find("Canvas");

		// HP Slider
		HitPointSlider = GameObject.Find("Slider").GetComponent<Slider>();
		HitPointSlider.maxValue = MaxHP;
		HitPointSlider.value = HP;
	}
	
	// Update is called once per frame
	void Update () {
		HitPointSlider.maxValue = MaxHP;
		HitPointSlider.value = HP;
	}

	// ダメージ計算処理
	public void Damage (float damage) {
		// HP減算処理
		HP = HP - damage;

		// ダメージを受けた時にエフェクトを発生
		var obj = GameObject.Instantiate(DamageEffect, transform.position, Quaternion.identity);
		Destroy(obj, 0.1f);

		// HPが無くなった場合の処理
		if (HP <= 0) {

    // 爆破音
//      GetComponent<AudioSource>().PlayOneShot(SE, 2.0F);

			CanvasController.SendMessage("addScore" , Score);
			var destroyObj = GameObject.Instantiate(DestroyEffect, transform.position, Quaternion.identity);
			Destroy(destroyObj, 0.5f);
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
//			Destroy(this.gameObject);
		}
	}
}
