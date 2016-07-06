using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

  public int maxBulletCount = 40;

  void Start () {
  }

  void Update () {
  }

  // 接触判定(接触オブジェクト)
  void OnTriggerEnter (Collider other) {
    if (other.tag == "Player") {
      Destroy(gameObject);
      other.GetComponent<PlayerStatus>().maxBulletCount += maxBulletCount;
    }
  }
}