using UnityEngine;
using System.Collections;

public class TextureScroller : MonoBehaviour
{
  float speed = 0.05f;
  float pos = 0;
  GameObject player;

  void Start()
  {
    //player = GameObject.FindGameObjectsWithTag("Player")[0];
    if (PlayerPrefs.GetInt("MapSkin", 0) != 0)
    {
      //Destroy(this.gameObject);
    }
    else
    {
      //this.transform.position = new Vector3(0.06f, -4.5f, 10f);
    }
  }

  void Update()
  {
    if (PlayerPrefs.GetInt("MapSkin", 0) != 0)
    {
      GetComponent<Renderer>().enabled = false;
    }
    else
    {
      GetComponent<Renderer>().enabled = true;
    }

    player = GameObject.FindGameObjectsWithTag("Player")[0];
    if (player != null)
    {
      var rigidBody2d = player.GetComponent<Rigidbody2D>();
      if (rigidBody2d != null)
      {
        if (rigidBody2d.velocity.x != 0 && !Game.Paused)
        {
          pos += speed * Time.deltaTime * 2;
          if (pos > 1.0f)
          {
            pos -= 1.0f;
          }
          GetComponent<Renderer>().material.mainTextureOffset = new Vector2(pos, 0);
          //renderer.material.mainTextureOffset = new Vector2((Time.time * speed) % 1, 0f);
        }
      }
    }
  }
}
