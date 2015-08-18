using UnityEngine;
using System.Collections;

public class TextureScroller : MonoBehaviour
{
  public int mapSkin = 0;

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
    if (PlayerPrefs.GetInt("MapSkin", 0) != mapSkin)
    {
      GetComponent<Renderer>().enabled = false;
    }
    else
    {
      GetComponent<Renderer>().enabled = true;
    }

    float difference = speed * Time.deltaTime * 2;

    if (Game.Over)
    {
      if(speed > 0)
        speed -= Time.deltaTime / 20;
      if (speed < 0)
        speed = 0;
    }

    player = GameObject.FindGameObjectsWithTag("Player")[0];
    if (player != null)
    {
      var rigidBody2d = player.GetComponent<Rigidbody2D>();
      if (rigidBody2d != null)
      {
        if (rigidBody2d.velocity.x != 0 && !Game.Paused)
        {
          pos += difference;
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
