using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

	// プレイヤー
	public GameObject player;

	// 移動スピード
	public float nomalSpeed;

	// プレイヤーに近づくスピード
	public float aproachSpeed;

	// 曲がる速さ
	public float rotationSmooth;

	// 次の目標を決める範囲
	public float changeTargetSqrDistance;

	// ベクトルレベル
	public float levelSize;

	// プレイヤーに近づいてくる設定距離
	public float approachPlayerDistance;

	// 目標地点の位置
	private Vector3 targetPosition;

	// プレイヤーの位置情報
	private Vector3 playerPosition;

	// 現在の移動速度
	private float speed;

	public void Start() {
		targetPosition = GetRandomPositionOnLevel();
		speed = nomalSpeed;
	}

	public void Update() {

		// 主人公との距離取得
		float playerDistance = Vector3.Distance(player.transform.position, transform.position);

		// 主人公との距離が近ければ、主人公に向かっていく
		if (playerDistance < approachPlayerDistance) {
			// 急速に近づく
			speed = aproachSpeed;

			// 目標地点の方向を向く
			Quaternion targetRotation = Quaternion.LookRotation (player.transform.position - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * rotationSmooth);

			// 前方に進む
			transform.Translate (Vector3.forward * speed * Time.deltaTime);
		} else {
			// 通常速度に戻る
			speed = nomalSpeed;

			// 目標地点との距離が小さければ、次のランダムな目標地点を設定する
			float sqrDistanceToTarget = Vector3.SqrMagnitude (transform.position - targetPosition);

			if (sqrDistanceToTarget < changeTargetSqrDistance) {
				targetPosition = GetRandomPositionOnLevel ();
			} 

			// 目標地点の方向を向く
			Quaternion targetRotation = Quaternion.LookRotation (targetPosition - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * rotationSmooth);

			// 前方に進む
			transform.Translate (Vector3.forward * speed * Time.deltaTime);
		}
	}

	// 新規ベクトルの取得
	public Vector3 GetRandomPositionOnLevel() {
		return new Vector3(Random.Range(-levelSize, levelSize), 0, Random.Range(-levelSize, levelSize));
	}

}
