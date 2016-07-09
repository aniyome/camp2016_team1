using UnityEngine;
using System.Collections;

public class Ending : MonoBehaviour {

  public MovieTexture movie;

  void Start () {
    movie.Play();
  }

  // Update is called once per frame
  void Update () {
    if (movie.isPlaying) {
    } else {
      Destroy(gameObject, 3);
    }
  }
}