using UnityEngine;
using System.Collections;

public class Item2 : MonoBehaviour {

  void Start () {
  }

  void Update () {
  }

  // 接触判定(接触オブジェクト)
  void OnTriggerEnter (Collider other) {
    if (other.tag == "Player") {
      Destroy(gameObject);
      other.GetComponent<PlayerStatus>().HP = other.GetComponent<PlayerStatus>().MaxHP;
    }
  }
}