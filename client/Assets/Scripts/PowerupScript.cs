using UnityEngine;
using System.Collections;

public class PowerupScript : MonoBehaviour 
{
    public AudioClip coinSound;
    GameObject mainCamera;
    public GameObject renderClone;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") 
        {
            if (PlayerPrefs.GetInt("Sound") == 1)
            {                
                mainCamera.GetComponent<AudioSource>().PlayOneShot(coinSound,0.3f);
            }
            mainCamera.GetComponent<HUDScript>().AddRubies(1);

            var spriteImage = GetComponent<SpriteRenderer>().sprite;
            var newObj = (GameObject)Instantiate(renderClone, transform.position, Quaternion.identity);
            newObj.GetComponent<SpriteRenderer>().sprite = spriteImage;

            Destroy(this.gameObject);
        }
    }
}
