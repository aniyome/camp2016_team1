// TODO: アイテムで武器強化 or 武器変更
// TODO: 左手モーション作成
// TODO: 全体的なSE
// TODO: シールド or HP回復 or 弾数ゲージ
// TODO: レーザーポインタ

using UnityEngine;
using System.Collections;
using Leap;

public class Motion : MonoBehaviour {

  // プレイヤー
  public GameObject player;
  // 弾オブジェクト
  public GameObject bullet;
  // バウンスオブジェクト
  public GameObject bounce;
  // 弾速
  public float bulletSpeed = 10000;
  // バウンス弾速
  public float bounceSpeed = 10000;
  // リロードスピード
  public float reloadSpeed = 0.05f;
  // 銃声
  public AudioClip shootSE;
  // 撃てる
  private bool isShooot;
  // 跳ね返せる
  private bool isBounce;
  // 移動量
  private Vector3 velocity;
  // 前進速度
  public float forwardSpeed = 7.0f;
  // 後退速度
  public float backwardSpeed = 7.0f;
  // 旋回速度
  public float rotateSpeed = 50.0f;
  // 発射間隔(秒)
  public float bulletTime = 0.1f;
  // バウンス発射間隔(秒)
  public float bounceTime = 1.0f;
  // 発射間隔用初期値
  private float nowTimeBullet = 0.0f;
  // バウンス発射間隔用初期値
  private float nowTimeBounce = 0.0f;

  // LeapMotion 呼び出し
  private Controller controller = new Controller();
  // 手の数
  private int handsCount;
  // 指の数
  private int handFingersCount;
  // 伸びている指の数
  private int fingersExtendedCount;
  // 親指(右)
  private bool rightFingerThumb = false;
  // 人差し指(右)
  private bool rightFingerIndex = false;
  private Leap.Finger rightFingerIndexObject;
  // 中指(右)
  private bool rightFingerMiddle = false;
  // 薬指(右)
  private bool rightFingerRing = false;
  // 小指(右)
  private bool rightFingerPinky = false;
  // 親指(左)
  private bool leftFingerThumb = false;
  // 人差し指(左)
  private bool leftFingerIndex = false;
  // 中指(左)
  private bool leftFingerMiddle = false;
  // 薬指(左)
  private bool leftFingerRing = false;
  // 小指(左)
  private bool leftFingerPinky = false;

  void Start() {
    // ジェスチャー有効化
    controller.EnableGesture(Gesture.GestureType.TYPECIRCLE);
    controller.EnableGesture(Gesture.GestureType.TYPEKEYTAP);
    controller.EnableGesture(Gesture.GestureType.TYPESCREENTAP);
    controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
  }

  void Update() {
    // 手の動きを検知
    checkMotion();
    // 弾発射
    isShooot = checkShoot();
    isBounce = checkBounce();
  }

  void FixedUpdate() {
    // 移動
    // move();
    // 撃つ
    if (isShooot && checkBulletCount()) {
      shoot();
    // max弾数を超えないようにする
    } else if (player.GetComponent<PlayerStatus>().bulletCount < player.GetComponent<PlayerStatus>().maxBulletCount) {
      // 弾チャージ
      player.GetComponent<PlayerStatus>().bulletCount += reloadSpeed;
      player.GetComponent<PlayerStatus>().bulletCount *= 1.020f;
    }
    // 跳ね返す
    if (isBounce) {
      bounceShoot();
    } else {
      // 弾チャージ？
    }
  }

  void shoot() {
    // 弾の発射間隔
    var bulletEnable = false;
    // Time.deltaTimeには1秒を分割した数値が入る
    nowTimeBullet += Time.deltaTime;
    if (nowTimeBullet > bulletTime) {
        nowTimeBullet = 0;
        bulletEnable = true;
    }
    // var rightHand = GameObject.Find("CleanRobotFullRightHand(Clone)/palm");
    var rightHand = GameObject.Find("CleanRobotFullRightHand(Clone)/index/bone1");
    if (rightHand != null && bulletEnable) {
      // 弾を一つ減らす
      player.GetComponent<PlayerStatus>().bulletCount -= 1;
      // TODO: 余裕があったら Handオブジェクトにタグをセット
      //rightHand.tag = "Player";

      // 弾を発射(複製obj, obj位置, obj向き)
      var bulletCrone = GameObject.Instantiate(bullet, rightHand.transform.position, Quaternion.identity) as GameObject;
      // 前方にbulletSpeed分の力を加える
      bulletCrone.GetComponent<Rigidbody>().AddForce(rightHand.transform.forward * bulletSpeed);
      // 銃SE
      GetComponent<AudioSource>().PlayOneShot(shootSE, 0.1F);
    }
  }

  // TODO: 未完成
  void bounceShoot() {
    // 弾の発射間隔
    var bounceEnable = false;
    // Time.deltaTimeには1秒を分割した数値が入る
    nowTimeBounce += Time.deltaTime;
    if (nowTimeBounce > bounceTime) {
        nowTimeBounce = 0;
        bounceEnable = true;
    }
    // var leftHand = GameObject.Find("CleanRobotFullLeftHand(Clone)/palm");
    var leftHand = GameObject.Find("CleanRobotFullLeftHand(Clone)/index/bone3");
    if (leftHand != null && bounceEnable) {
      // 弾を発射(複製obj, obj位置, obj向き)
      var bounceCrone = GameObject.Instantiate(bounce, leftHand.transform.position, Quaternion.identity) as GameObject;
      // 前方にbounceSpeed分の力を加える
      // bounceCrone.GetComponent<Rigidbody>().AddForce(leftHand.transform.forward * bounceSpeed);
        bounceCrone.GetComponent<Rigidbody>().AddForce(leftHand.transform.forward * bounceSpeed, ForceMode.Acceleration);
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

  bool checkShoot() {
//    return rightFingerThumb && rightFingerIndex && rightFingerMiddle && rightFingerRing && rightFingerPinky;
    return rightFingerThumb && rightFingerIndex;
  }

  // TODO: トリガーはジェスチャーにするか未定
  bool checkBounce() {
    return leftFingerThumb && leftFingerIndex;
  }

  bool checkBulletCount() {
    if (player.GetComponent<PlayerStatus>().bulletCount >= 1) {
      return true;
    }
    return false;
  }

  void checkMotion() {
    // 初期化
    // 手の数
    handsCount = 0;
    // 指の数
    handFingersCount = 0;
    // 伸びている指の数
    fingersExtendedCount = 0;
    // 親指(右)
    rightFingerThumb = false;
    // 人差し指(右)
    rightFingerIndex = false;
    // 中指(右)
    rightFingerMiddle = false;
    // 薬指(右)
    rightFingerRing = false;
    // 小指(右)
    rightFingerPinky = false;
    // 親指(左)
    leftFingerThumb = false;
    // 人差し指(左)
    leftFingerIndex = false;
    // 中指(左)
    leftFingerMiddle = false;
    // 薬指(左)
    leftFingerRing = false;
    // 小指(左)
    leftFingerPinky = false;

    var frame = controller.Frame();
    HandList hands = frame.Hands;
    handsCount = hands.Count;
    // 手の検知
    foreach (var hand in hands) {
      FingerList fingers = hand.Fingers;
      handFingersCount = hand.Fingers.Count;
      fingersExtendedCount = fingers.Extended().Count;
      // 指の検知
      foreach (var finger in fingers) {
        // 右の伸びている指を検知
        if (finger.IsExtended && hand.IsRight) {
          // 親指を検知
          if (finger.Type().Equals(fingers[0].Type())) {
            rightFingerThumb = true;
          }
          // 人差し指を検知
          if (finger.Type().Equals(fingers[1].Type())) {
            rightFingerIndex = true;
            rightFingerIndexObject = finger;
          }
          // 中指を検知
          if (finger.Type().Equals(fingers[2].Type())) {
            rightFingerMiddle = true;
          }
          // 薬指を検知
          if (finger.Type().Equals(fingers[3].Type())) {
            rightFingerRing = true;
          }
          // 小指を検知
          if (finger.Type().Equals(fingers[4].Type())) {
            rightFingerPinky = true;
          }
        }
        if (finger.IsExtended && hand.IsLeft) {
          // 親指を検知
          if (finger.Type().Equals(fingers[0].Type())) {
            leftFingerThumb = true;
          }
          // 人差し指を検知
          if (finger.Type().Equals(fingers[1].Type())) {
            leftFingerIndex = true;
         }
          // 中指を検知
          if (finger.Type().Equals(fingers[2].Type())) {
            leftFingerMiddle = true;
          }
          // 薬指を検知
          if (finger.Type().Equals(fingers[3].Type())) {
            leftFingerRing = true;
          }
          // 小指を検知
          if (finger.Type().Equals(fingers[4].Type())) {
            leftFingerPinky = true;
          }
        }
      }
    }
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