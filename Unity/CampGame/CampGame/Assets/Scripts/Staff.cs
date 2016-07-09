using UnityEngine;
using System.Collections;

public class Staff : MonoBehaviour {

  public MovieTexture movie;
  public GameObject ending;

  void Update() {
    if (ending == null) {
      movie.Play();
    }
  }
}