using UnityEngine;
using System.Collections;

public class CloudSpawnScript : MonoBehaviour
{
  public GameObject[] obj;
  void Start()
  {
    Instantiate(obj[Random.Range(0, obj.GetLength(0))], transform.position, Quaternion.identity);
  }
}
