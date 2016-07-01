using UnityEngine;
using UnityEngine.UI; //uGUIにアクセス
using System.Collections;

public class TitleController : MonoBehaviour {

  public GameObject opening;
  public GameObject textObject;
  public float fadetime = 2;
  private Image image;
  private Text text;
  private float time;
  private float nextTime;

  void Start () {
    image = GetComponent<Image>();
    text = textObject.GetComponent<Text>();
    time = 0;
  }

  void Update () {
    if (opening == null) {
      fadeIn();
    }
    if (time > fadetime + 1) {
      // 文字を点滅
      if (nextTime < Time.time) {
        nextTime = Time.time;
        var color = text.color;
        if (color.a == 0.0f) {
          color.a = 1.0f;
          text.color = color;
        } else {
          color.a = 0.0f;
          text.color = color;
        }
        nextTime += 0.8f;
      }
    }
  }

  void fadeIn() {
    time += Time.deltaTime;
    float a = time / fadetime;
    var color = image.color;
    color.a = a;
    image.color = color;
  }
}
