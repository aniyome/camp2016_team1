using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

	// ----- プロパティ ------ //

	// ゲームプレイヤーオブジェクト
	public GameObject Player;

	// 通常の敵のスピード
	public float MovementSpeed;

	// プレイヤーに近づくスピード
	public float ApproachSpeed;

	// 敵の曲がる速さ
	public float RotationSmooth = 10;

	// ランダム移動の範囲
	public float RandomPositionLevel = 15;

	// 次の目標を決める範囲
	public float ChangeTargetSqrDistance = 5;

	// プレイヤーに近づいてくる設定距離
	public float ApproachPlayerDistance = 5;

	// 目標地点の位置
	private Vector3 TargetPosition;

	// プレイヤーの位置情報
	private Vector3 PlayerPosition;

	// プレイヤーと敵の距離
	private float PlayerDistance;

	// 現在の移動速度
	private float NowSpeed;

	// ----- Public関数 ------ //

	// オブジェクト生成時に呼び出される処理
	public void Start() {
		// ランダム移動のために、ランダムな位置を取得する
		TargetPosition = GetRandomPosition ();
		// 設定した移動スピードを現在のスピードにセット
		NowSpeed = MovementSpeed;
	}

	// 常に呼び出される処理
	public void Update() {

		// 自身とゲームプレイヤーとの距離取得
		PlayerDistance = Vector3.Distance(Player.transform.position, transform.position);

		// 主人公との距離が近ければ、主人公に向かっていく
		if (PlayerDistance < ApproachPlayerDistance) {
			// 近づいてくる
			ApproachMove ();
		} else {
			// ランダム移動
			RamdomMove();
		}
	}

	// ----- 以下はPrivate関数 ----- //

	// ゲームプレイヤーへ近づいてくる
	private void ApproachMove() {
		// プレイヤーに近づくスピードを現在の移動速度に設定
		NowSpeed = ApproachSpeed;

		// 目標地点の方向を向く
		Quaternion targetRotation = Quaternion.LookRotation (Player.transform.position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * RotationSmooth);

		// 前方に進む
		transform.Translate (Vector3.forward * NowSpeed * Time.deltaTime);
	}

	// ランダム移動
	private void RamdomMove() {
		// 通常の敵のスピード現在の移動速度に設定
		NowSpeed = MovementSpeed;

		// ランダム移動のために、ランダムな位置を取得する
		float sqrDistanceToTarget = Vector3.SqrMagnitude (transform.position - TargetPosition);

		// ランダムな位置に
		if (sqrDistanceToTarget < ChangeTargetSqrDistance) {
			TargetPosition = GetRandomPosition ();
		} 

		// 目標地点の方向を向く
		Quaternion targetRotation = Quaternion.LookRotation (TargetPosition - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * RotationSmooth);

		// 前方に進む
		transform.Translate (Vector3.forward * NowSpeed * Time.deltaTime);
	}

	// ランダムの位置ベクトル取得処理
	private Vector3 GetRandomPosition() {
		return new Vector3(Random.Range(-RandomPositionLevel, RandomPositionLevel), 0, Random.Range(-RandomPositionLevel, RandomPositionLevel));
	}

}
