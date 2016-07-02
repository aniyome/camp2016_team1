using UnityEngine;
using System.Collections;

public class BaseEnemyAnimation : MonoBehaviour {

	// 敵キャラを回転させ、元の位置に戻す
	public void EnemyPunchRotation() {
		iTween.PunchRotation(this.gameObject, iTween.Hash(
			"y", 90,
			"time", 0.5f)
		);
	}
		
	// 敵キャラを拡大し、元の位置に戻す
	public void EnemyPunchScale() {
		iTween.PunchScale(this.gameObject, iTween.Hash(
			"x", 60,
			"y", 60,
			"z", 60,
			"time", 1.0f)
		);
	}

	// 敵キャラの大きさを変化させる（使えるかも？）
	public void EnemyChangeScale() {
		iTween.ScaleTo (this.gameObject, iTween.Hash("x",100,"time",30.5f));
	}

	// 敵キャラを振動させる（その場に止まってしまう）
	public void EnemyShake() {
		//指定した大きさ・方向に揺れ
		iTween.ShakePosition (this.gameObject, iTween.Hash("x",0.2f,"time",0.9f));
	}

	// 敵キャラを回転させる（割とカオス）
	public void EnemyRotate() {
		iTween.RotateTo (this.gameObject, new Vector3 (0,540,0), 1f);
	}

	// 敵キャラを指定のポジションに移動させる
	public void EnemyMovePosition() {
		iTween.MoveTo (this.gameObject, iTween.Hash("y",3.5,"time", 20f));
	}

	// 敵キャラを指定のポジションに移動して、元の場所に戻す
	public void EnemyPunchPosition() {
		iTween.PunchPosition(this.gameObject,iTween.Hash("x",10,"z",10,"time",3));
	}
}
