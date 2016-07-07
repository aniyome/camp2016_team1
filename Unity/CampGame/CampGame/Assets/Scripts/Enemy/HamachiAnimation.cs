using UnityEngine;
using System.Collections;

public class HamachiAnimation : MonoBehaviour {

	// 重力値.
	const float GravityPower = 9.8f; 
	//　目的地についたとみなす停止距離.
	const float StoppingDistance = 2f;

	// 現在の移動速度.
	Vector3 velocity = Vector3.zero; 
	// キャラクターコントローラーのキャッシュ.
	CharacterController characterController; 
	// 到着したか（到着した true/到着していない false)
	public bool arrived = false; 

	// 向きを強制的に指示するか.
	bool forceRotate = false;

	// 強制的に向かせたい方向.
	Vector3 forceRotateDirection;

	// 目的地.
	public Vector3 destination; 

	// 移動速度.
	public float walkSpeed = 12f;

	// 回転速度.
	public float rotationSpeed = 360.0f;

	// 乱数生成範囲
	public float positionRandomRange = 10;

	// 現在地
	private Vector3 NowPosition;

	// Use this for initialization
	void Start () {
		// キャラクターコントローラ取得
		characterController = GetComponent<CharacterController>();
		// 目的地設定
		destination = GetRandomVector();
	}

	// Update is called once per frame
	void Update () {

		// 移動速度velocityを更新する
		if (characterController.isGrounded) {
			//　水平面での移動を考えるのでXZのみ扱う.
			Vector3 destinationXZ = destination; 
			destinationXZ.y = transform.localPosition.y;// 高さを目的地と現在地を同じにしておく.

			//********* ここからXZのみで考える. ********
			// 目的地までの距離と方角を求める.
			Vector3 direction = (destinationXZ - transform.localPosition).normalized;
			float distance = Vector3.Distance(transform.localPosition,destinationXZ);

			// 現在の速度を退避.
			Vector3 currentVelocity = velocity;

			//　目的地にちかづいたら到着..
			if (arrived || distance < StoppingDistance)
				arrived = true;


			// 移動速度を求める.
			if (arrived)
				velocity = Vector3.zero;
			else 
				velocity = direction * walkSpeed;


			// スムーズに補間.
			velocity = Vector3.Lerp(currentVelocity, velocity,Mathf.Min (Time.deltaTime * 5.0f ,1.0f));
			velocity.y = 0;


			if (!forceRotate) {
				// 向きを行きたい方向に向ける.
				if (velocity.magnitude > 0.1f && !arrived) { // 移動してなかったら向きは更新しない.
					Quaternion characterTargetRotation = Quaternion.LookRotation (direction);
					transform.rotation = Quaternion.RotateTowards (transform.rotation, characterTargetRotation, rotationSpeed * Time.deltaTime);
				}
			} else {
				// 強制向き指定.
				Quaternion characterTargetRotation = Quaternion.LookRotation(forceRotateDirection);
				transform.rotation = Quaternion.RotateTowards(transform.rotation,characterTargetRotation,rotationSpeed * Time.deltaTime);
			}

		}

		// 重力をかける
		GravityMove ();

		if (characterController.velocity.magnitude < 1f)
			arrived = true;

		// 強制的に向きを変えるを解除.
		if (forceRotate && Vector3.Dot(transform.forward,forceRotateDirection) > 0.99f)
			forceRotate = false;

		// 目的地についてたら、新しい目的地を設定
		if (arrived) {
			SetDestination();
		}

	}

	// 目的地を設定する.引数destinationは目的地.
	public void SetDestination() {
		arrived = false;
		this.destination = GetRandomVector();
	}

	// 指定した向きを向かせる.
	public void SetDirection(Vector3 direction) {
		forceRotateDirection = direction;
		forceRotateDirection.y = 0;
		forceRotateDirection.Normalize();
		forceRotate = true;
	}

	// 移動をやめる.
	public void StopMove() {
		destination = transform.position; // 現在地点を目的地にしてしまう.
	}

	// 目的地に到着したかを調べる. true　到着した/ false 到着していない.
	public bool Arrived() {
		return arrived;
	}

	public void GravityMove() {
		// 重力.
		velocity += Vector3.down * GravityPower * Time.deltaTime;

		// 接地していたら思いっきり地面に押し付ける.
		// (UnityのCharactorControllerの特性のため）
		Vector3 snapGround = Vector3.zero;
		if (characterController.isGrounded)
			snapGround = Vector3.down;

		// CharacterControllerを使って動かす.
		characterController.Move(velocity * Time.deltaTime+snapGround);
	}

	// ランダムな目的地生成
	private Vector3 GetRandomVector() {
		NowPosition = transform.localPosition;
		return new Vector3 ((NowPosition.x + Random.Range(-positionRandomRange, positionRandomRange)), 0, (NowPosition.z + Random.Range(-positionRandomRange, positionRandomRange)));
	}
}
