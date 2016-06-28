// TODO: 弾数制限
// TODO: 発射速度調整
// TODO: アイテムで武器強化 or 武器変更
// TODO: 左手モーション作成
// TODO: Enemy当たり判定(SEも)
// TODO: ダメージ判定
// TODO: 銃口位置調整

using UnityEngine;
using System.Collections;
using Leap;

public class Motion : MonoBehaviour {

  // leap motion 呼び出し
  Controller controller = new Controller();
  // プレイヤー
  public GameObject player;
  // 弾オブジェクト
  public GameObject bullet;
  // 銃口
  public GameObject muzzle;
  // 弾速
  public float bulletSpeed = 3000;
  // 銃声
  public AudioClip shootSE;
  // 撃てる
  private bool isShooot;
  // 移動量
  private Vector3 velocity;
  // 前進速度
  public float forwardSpeed = 7.0f;
  // 後退速度
  public float backwardSpeed = 7.0f;
  // 旋回速度
  public float rotateSpeed = 50.0f;

  void Start() {
    // ジェスチャー有効化
    controller.EnableGesture(Gesture.GestureType.TYPECIRCLE);
    controller.EnableGesture(Gesture.GestureType.TYPEKEYTAP);
    controller.EnableGesture(Gesture.GestureType.TYPESCREENTAP);
    controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
  }

  void Update() {
    // debug
    // debug();
      isShooot = checkForShoot();
  }

  void FixedUpdate() {
    // 移動
    // move();
    // 撃つ
    if (isShooot) {
      shoot();
    } else {
    }
  }

  void shoot() {
    // var rightHand = GameObject.Find("CleanRobotFullRightHand(Clone)/palm");
    var rightHand = GameObject.Find("CleanRobotFullRightHand(Clone)/index/bone3");
    if (rightHand != null) {
      // 銃口の位置調整
      // muzzle.transform.position = rightHand.transform.position + new Vector3(0, 0, 5.5f);
      // 弾を発射(複製obj, obj位置, obj向き)
      var bulletCrone = GameObject.Instantiate(bullet, muzzle.transform.position, Quaternion.identity) as GameObject;
      // 銃口の前方にbulletSpeed分の力を加える
      bulletCrone.GetComponent<Rigidbody>().AddForce(rightHand.transform.forward * bulletSpeed);
      // 銃SE
      GetComponent<AudioSource>().PlayOneShot(shootSE, 0.1F);
    }
  }

  // バックアップ
  void move() {
    var frame = controller.Frame();
    // LeftHandのみ制御
    if (frame.Hands[0].IsLeft) {
      float z = frame.Hands[0].Direction.y * -1;
      velocity = new Vector3(0, 0, z);
      velocity = transform.TransformDirection(velocity);
      if (velocity.z > 1) {
        velocity *= forwardSpeed;
      } else if (velocity.z < -5) {
        velocity *= backwardSpeed;
      }
//      player.transform.localPosition += velocity * Time.fixedDeltaTime;
      transform.localPosition += velocity * Time.fixedDeltaTime;
    }

   // TODO: 腕が回転してしまう(未完成)
    if (frame.Hands[0].IsRight) {
      float x = frame.Hands[0].Direction.x;
      float z = frame.Hands[0].Direction.y;
//      Debug.Log(z);
//      transform.Rotate(-z * rotateSpeed, x * rotateSpeed, 0);
//       Debug.Log(transform.rotation.x);
        transform.Rotate(0, x * rotateSpeed, 0);
        transform.Rotate(-z * rotateSpeed, 0, 0);
    }
  }

  bool checkForShoot() {
    var frame = controller.Frame();
    var isIndex = false;
    var isThumb = false;
    foreach (var hand in frame.Hands) {
      // 指のタイプを検出
      foreach (var finger in hand.Fingers) {
        if (finger.IsExtended && hand.IsRight) {
          // INDEX確認
          if (finger.Type().Equals(frame.Fingers[0].Type())) {
            isIndex = true;
          }
          // THUMB確認
          if (finger.Type().Equals(frame.Fingers[1].Type())) {
            isThumb = true;
          }
        }
      }
    }
    return isIndex && isThumb && frame.Fingers.Extended().Count == 5;
  }

  void debug() {
    var frame = controller.Frame();
//    FingerCount = frame.Fingers.Count;
//    Debug.Log(frame.Pointables);

      //LeapMotionの縦距離
      Debug.Log( frame.Hands[0].PalmPosition);

      //LeapMotionからの高さ
      Debug.Log( frame.Hands[0].PalmNormal);

    // 伸びている指の数を検知
//    Debug.Log(frame.Fingers.Extended().Count);

//    //右手の人差し指の先を検知
//    var rightIndexBone3 = GameObject.Find("CleanRobotFullRightHand(Clone)/index/bone3");
//    Debug.Log(rightIndexBone3.transform.position);

    // 手の検知
//    foreach (var hand in frame.Hands) {
//      Debug.Log(string.Format("ID : {0} ポインタ : {1} 指: {2} ツール : {3}",
//      hand.Id, hand.Pointables.Count, hand.Fingers.Count, hand.Tools.Count));
//    }

    // 指の検知
//    foreach (var finger in frame.Fingers) {
//      Debug.Log(string.Format("ID : {0} 種類 : {1} 位置 : {2} 速度 : {3} 向き : {4}",
//      finger.Id, finger.Type(), finger.TipPosition, finger.TipVelocity, finger.Direction));
//    }

    // 指の関節情報を検知
//    foreach ( var finger in frame.Fingers ) {
//      // 指先の骨
//      var bone = finger.Bone(Bone.BoneType.TYPE_DISTAL);
//      Debug.Log(string.Format("種類 : {0} 中心 : {1} 上端 : {2} 下端 : {3}",
//      bone.Type, bone.Center, bone.PrevJoint, bone.NextJoint));
//    }
  }
}