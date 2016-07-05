using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {

  // 発射時エフェクト
  public GameObject effect;

  // 自動削除されるまでの時間
  public float destroyTime = 1;

  public float coefficient;   // 空気抵抗係数
  public Vector3 velocity;    // 風速

  void OnTriggerStay(Collider col) {
    if (col.GetComponent<Rigidbody>() == null) {
      return;
    }
    // 相対速度計算
    var relativeVelocity = velocity - col.GetComponent<Rigidbody>().velocity;
    // 空気抵抗を与える
    col.GetComponent<Rigidbody>().AddForce(coefficient * relativeVelocity);
  }
}