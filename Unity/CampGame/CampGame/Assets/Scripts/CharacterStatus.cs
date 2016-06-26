using UnityEngine;
using System.Collections;

public class CharacterStatus : MonoBehaviour {

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
		HP = MaxHP;
	}
	
	// Update is called once per frame
	void Update () {

	}

	// ダメージ加算処理
	public void Damage (float damage) {
		HP = HP - damage;
		// HPが無くなった場合の処理
		if (HP <= 0) {
			Destroy(gameObject);
		}
	}
}
