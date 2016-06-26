using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterStatus : MonoBehaviour {

	// 最大HP
	public float MaxHP = 100;

	// 最大MP
	public float MaxMP = 100;

	// HP
	private float HP;

	// MP
	private float MP;

	// HPバー
	private Slider slider;

	// HPバーの値
	private float sliderValue = 1;

	// Use this for initialization
	void Start () {
		// MaxHPを現在のHPに設定
		HP = MaxHP;

		// HPバーのオブジェクト取得
		slider = GetComponentInChildren<Slider>();
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

		Debug.Log ("残りゲージは" + slider.value);
		Debug.Log ("残りHPは" + HP);

		// HPが無くなった場合の処理
		if (HP <= 0) {
			Destroy(gameObject);
		}
	}
}
