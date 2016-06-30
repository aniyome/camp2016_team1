using UnityEngine;
using UnityEngine.UI; //uGUIにアクセス
using System.Collections;

public class Fade : MonoBehaviour {
  public float waittime = 3;
  public float fadetime = 2;
  private Image image;
  private float time;
  private bool is_in = true;
  private bool is_out = false;

  void Start () {
    // 初期化
    image = GetComponent<Image>();
    var color = image.color;
    color.a = 0;
    time = 0;
  }

  void Update () {
    if (is_in) {
      fade_in();
    }
    if (time > fadetime) {
      StartCoroutine("sleep");
      is_in = false;
    }
    if (is_out) {
      fade_out();
    }
  }

  IEnumerator sleep() {
    yield return new WaitForSeconds(waittime);
    is_out = true;
  }

  void fade_in() {
    time += Time.deltaTime;//時間更新.今度は増えていく
    float a = time / fadetime;
    var color = image.color;
    color.a = a;
    image.color = color;
  }

  void fade_out() {
    time -= Time.deltaTime;//時間更新(徐々に減らす)
    float a = time / fadetime;//徐々に0に近づける
    var color = image.color;//取得したimageのcolorを取得
    color.a = a;//カラーのアルファ値(透明度合)を徐々に減らす
    image.color = color;//取得したImageに適応させる
  }
}