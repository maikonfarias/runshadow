using UnityEngine;
using System.Collections;

public class FadeScript : MonoBehaviour
{

  // Use this for initialization
  void Start()
  {
    Fade();
  }

  // Update is called once per frame
  void Update()
  {

  }

  void Fade()
  {
    var sprite = GetComponent<SpriteRenderer>();
    //sprite.material.color = new Color(sprite.material.color.r, sprite.material.color.g, sprite.material.color.b, sprite.material.color.a - 0.1f);
    sprite.material.color -= new Color(0, 0, 0, 0.1f);
    Invoke("Fade", 0.02f);
  }
}
