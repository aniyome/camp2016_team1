using UnityEngine;
using System.Collections;

public class AmaebiAnimation : MonoBehaviour {

	// 通常の敵のスピード
	public float MovementSpeed = 14;

	// 移動の向きを変える時間
	public float MovementIntervalTime = 5;

	// 乱数範囲
	public float RandomRange = 2;

	// 自身の現在位置
	private Vector3 NowPosition;

	// 目標地点の位置
	private Vector3 TargetPosition;

	// 現在の移動速度
	private float NowSpeed;

	// Use this for initialization
	void Start () {
		// 設定した移動スピードを現在のスピードにセット
		NowSpeed = MovementSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		// ランダム移動
		RamdomMove();
	}

	private void RamdomMove() {
		// 通常の敵のスピード現在の移動速度に設定
		NowSpeed = MovementSpeed;

		// 移動先のランダム位置を取得
		NowPosition = this.transform.position;

		this.transform.position = new Vector3 ((NowPosition.x + Random.Range(-RandomRange, RandomRange)), 0, (NowPosition.z + Random.Range(-RandomRange, RandomRange)));

//		float distance = Vector3.Distance (transform.position, TargetPosition);
//
//		// 目標地点の方向を向く
//		Quaternion targetRotation = Quaternion.LookRotation (TargetPosition - transform.position);
//		transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * 10);
//
//		// 前方に進む
//		transform.Translate (Vector3.forward * NowSpeed * Time.deltaTime);
	}

}
