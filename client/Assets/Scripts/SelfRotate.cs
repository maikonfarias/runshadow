using UnityEngine;
using System.Collections;

public class SelfRotate : MonoBehaviour
{
  void Update()
  {
    if (!Game.Paused)
    {
      transform.Rotate(0, 0, -23f);
    }
  }
}
