using UnityEngine;
using System.Collections;

public class Movie : MonoBehaviour {

  public MovieTexture movie;

//  public bool movieFinish = false;

 // Use this for initialization
    void Start () {
      movie.Play();
      movie.loop = true;
//      AudioSource aud = GetComponent<AudioSource>();
//      aud.clip = aud.audioClip.Play();
//      aud.Play();
    }

    // Update is called once per frame
    void Update () {
      if (movie.isPlaying) {
      } else {
        // TODO: ここを削除じゃなくてフラグにして渡す
  //    Destroy(gameObject);
  //    movieFinish = true;
  //    Debug.Log(movieFinish);
      }
   }
}