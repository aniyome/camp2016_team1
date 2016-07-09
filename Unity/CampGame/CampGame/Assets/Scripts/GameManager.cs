using UnityEngine;
using System.Collections;
using Leap;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

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

	// scene time
	private float time = 0;

  void Start () {
  }

  void Update () {
    // 手の動きを検知
    checkMotion();
    // play画面に遷移
    if (handsCount > 0 && Application.loadedLevelName == "opening") {
      // SceneManager.LoadScene ("kuma_scene", LoadSceneMode.Single);
      SceneManager.LoadScene ("main_scene", LoadSceneMode.Single);
    }

    // TimeCount
    time += Time.deltaTime;

    if (handsCount > 0 && Application.loadedLevelName == "game_over") {
      if (time > 5) {
        SceneManager.LoadScene ("main_scene", LoadSceneMode.Single);
      }
    }
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
}