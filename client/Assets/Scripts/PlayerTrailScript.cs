using UnityEngine;
using System.Collections;

public class PlayerTrailScript : MonoBehaviour {

    public GameObject renderClone;
    Vector3 delayPos = new Vector3(0, 0, 0);

    void Start () 
    {        
          PlayerTrail();
    }
	
    void PlayerTrail()
    {
        var spriteImage = GetComponent<SpriteRenderer>().sprite;

        if (delayPos != new Vector3(0, 0, 0))
        {
            var newObj = (GameObject)Instantiate(renderClone, delayPos, transform.rotation);
            newObj.GetComponent<SpriteRenderer>().sprite = spriteImage;
        }
        
        Invoke("GetPlayerPos", 0.03f);
        Invoke("PlayerTrail", 0.05f);
       
    }

    void GetPlayerPos()
    {
        delayPos = transform.position;
    }
}
