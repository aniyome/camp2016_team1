using UnityEngine;
using System.Collections;

public class playerStatus : MonoBehaviour {

	// 最大HP
	public float MaxHP = 100;

	// 最大MP
	public float MaxMP = 100;

	// HP
	private float HP;

	// MP
	private float MP;

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
		Debug.Log ("playerの残りHPは" + HP);
		// HPが無くなった場合の処理
		if (HP <= 0) {
			// TODO ゲームオーバー処理
			Debug.Log("ゲームオーバー");
		}
	}
}
