using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {
        
    public GameObject[] obj;
    public float spawnMin = 1f;
    public float spawnMax = 2f;
    public bool spawnAtStart = true;
    public bool spawnDuringGame = true;


    void Start () 
    {
        if (spawnAtStart)
        {
            Spawn();
        }
        else
        {
            Invoke("Spawn", Random.Range(spawnMin, spawnMax));
        }
    }	

    void Spawn () 
    {
        if ((HUDScript.GameStarted && spawnDuringGame) || (!HUDScript.GameStarted && spawnAtStart))
        {
            Instantiate(obj[Random.Range(0, obj.GetLength(0))], transform.position, Quaternion.identity);
        }
        Invoke("Spawn",  Random.Range(spawnMin, spawnMax));	
    }
}
