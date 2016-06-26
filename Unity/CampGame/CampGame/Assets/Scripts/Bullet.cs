using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

  // Bulletの攻撃力
  public float damage = 3;
  // 自動削除されるまでの時間
  public float destroyTime = 1;

  void Start () {
    // Bullet削除
    Destroy(gameObject, destroyTime);
  }

  void Update () {
    // debug();
  }

  // 接触判定(接触オブジェクト)
  void OnTriggerEnter (Collider other) {
    // Bullet削除
    Destroy(gameObject);

    // EnemyならDamage
    if (other.tag == "Enemy") {
        // (関数名, 値)
        other.SendMessage("Damage", damage);
        Debug.Log("Enemy");
    }
  }

  void debug () {
    Debug.Log("log");
  }

  // バックアップ
// function hit() {
//     var playerPosition = player.transform.position;
//     var forward = transform.TransformDirection (Vector3.forward);
//     var hit : RaycastHit;
//     var distance = 2;
//     // 当たり判定(開始地点、方向、hit情報、最大距離、衝突を無視するかどうか)
//     if (Physics.Raycast(playerPosition, forward, hit, distance)){
//         print('hit!!' + hit.transform.gameObject.name);
//         // Enemyと接触
//         if (hit.collider.tag == 'Enemy') {
//             print('Enemyと接触');
//             Destroy(hit.collider.gameObject);
//             // TODO: 音楽
//             // audio.Play();
//             // TODO: score
//             // hit_score += 1;
//         }
//     } else {
//         print("no hit");
//     }
// }
}

