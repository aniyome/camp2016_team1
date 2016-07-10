using UnityEngine;
using System.Collections;

public class BossComponentMove : MonoBehaviour {


	// ----- プロパティ ------ //

	// ゲームプレイヤーオブジェクト
	public GameObject Player;

	// 通常の敵のスピード
	public float MovementSpeed;

	// 敵の曲がる速さ
	public float RotationSmooth = 100;

	// プレイヤーに近づいてくる設定距離
	public float ApproachPlayerDistance = 16;

	// RamdomValue
	public float RandomApproachValue = 0;

	// 目標地点の位置
	private Vector3 TargetPosition;

	// プレイヤーの位置情報
	private Vector3 PlayerPosition;

	// プレイヤーと敵の距離
	private float PlayerDistance;

	// 現在の移動速度
	private float NowSpeed;

	// ApproashMoveFlag
	private bool ApproachMoveFlag = false;

	// ----- Public関数 ------ //

	// オブジェクト生成時に呼び出される処理
	public void Start() {
		// 設定した移動スピードを現在のスピードにセット
		NowSpeed = MovementSpeed;
	}

	// 常に呼び出される処理
	public void Update() {
		// lottery
		if ((Random.value + Random.value + Random.value) < RandomApproachValue) {
			ApproachMoveFlag = true;
		}

		// approach
		if (ApproachMoveFlag) {
			ApproachMove ();
		}
	}

	// ----- 以下はPrivate関数 ----- //

	// ゲームプレイヤーへ近づいてくる
	private void ApproachMove() {
		// 目標地点の方向を向く
		Quaternion targetRotation = Quaternion.LookRotation (Player.transform.position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * RotationSmooth);

		// 前方に進む
		transform.Translate (Vector3.forward * NowSpeed * Time.deltaTime);
	}
}
