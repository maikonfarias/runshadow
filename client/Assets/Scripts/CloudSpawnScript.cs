using UnityEngine;
using System.Collections;

public class CloudSpawnScript : MonoBehaviour {
    public GameObject[] obj;
    void Start () 
    {
        /*if (PlayerPrefs.GetInt("MapSkin", 0) != 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instantiate(obj[Random.Range(0, obj.GetLength(0))], transform.position, Quaternion.identity);
        }*/
        Instantiate(obj[Random.Range(0, obj.GetLength(0))], transform.position, Quaternion.identity);
    }
}
