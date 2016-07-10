using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BossStatus : MonoBehaviour {
	// 最大HP
	public float MaxHP = 1000;

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

	// Use this ending start waiting
	private bool GameClearFlag = false;

	// キャンパスコントローラ
	private GameObject CanvasController;

	// Use this for initialization
	void Start () {
		// MaxHPを現在のHPに設定
		HP = MaxHP;

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

		// ダメージを受けた時にエフェクトを発生
		var obj = GameObject.Instantiate(DamageEffect, transform.position, Quaternion.identity);
		Destroy(obj, 0.5f);

		// HPが無くなった場合の処理
		if (HP <= 0) {
			// Ending
			var destroyObj = GameObject.Instantiate(DestroyEffect, transform.position, Quaternion.identity);
			Destroy(destroyObj, 10.0f);

			if (!GameClearFlag) {
				GameClearFlag = true;
				GameClear ();
			}
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

	private IEnumerator GameClear() {
		yield return new WaitForSeconds(10);
		SceneManager.LoadScene ("game_clear", LoadSceneMode.Single);
	}
}
