using UnityEngine;
using UnityEngine.UI; //uGUIにアクセス
using System.Collections;

public class Fade : MonoBehaviour {
  public float waittime = 3;
  public float fadetime = 2;
  private Image image;
  private float time;
  private bool isIn = true;
  private bool isOut = false;

  void Start () {
    // 初期化
    image = GetComponent<Image>();
    time = 0;
  }

  void Update () {
    if (isIn) {
      fadeIn();
    }
    if (time > fadetime) {
      StartCoroutine("sleep");
      isIn = false;
    }
    if (isOut) {
      fadeOut();
      if (image.color.a < 0) {
        Destroy(gameObject);
      }
    }
  }

  IEnumerator sleep() {
    yield return new WaitForSeconds(waittime);
    isOut = true;
  }

  void fadeIn() {
    time += Time.deltaTime;
    float a = time / fadetime;
    var color = image.color;
    color.a = a;
    image.color = color;
  }

  void fadeOut() {
    time -= Time.deltaTime;
    float a = time / fadetime;
    var color = image.color;
    color.a = a;
    image.color = color;
  }
}