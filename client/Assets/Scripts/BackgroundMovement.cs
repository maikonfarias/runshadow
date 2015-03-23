using UnityEngine;
using System.Collections;

public class BackgroundMovement : MonoBehaviour
{
  GameObject player;
  void Start()
  {
    player = GameObject.FindGameObjectsWithTag("Player")[0];
  }

  void Update()
  {
    player = GameObject.FindGameObjectsWithTag("Player")[0];
    if (player != null)
    {
      //this.transform.position = new Vector3(startPos.position.x + (mainCamera.transform.position.x*-0.0001f), 0, 0);
      if (player.GetComponent<Rigidbody2D>().velocity.x != 0 && !Game.Paused)
      {
        //this.transform.position += new Vector3(0.03f, 0, 0);
        this.transform.position += new Vector3(7.0f * Time.deltaTime, 0, 0);
      }
    }
  }
}
