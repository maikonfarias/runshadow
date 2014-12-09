using UnityEngine;
using System.Collections;

public class SelectSkinScript : MonoBehaviour {
    public bool realTimeUpdate = false;
    public Sprite[] skins;
    void Start () 
    {
          UpdateSkin();
    }

    void UpdateSkin()
    {
        int currentSkin = PlayerPrefs.GetInt("MapSkin", 0);
        if (skins.GetLength(0) > currentSkin)
        {
            GetComponent<SpriteRenderer>().sprite = skins[currentSkin];
        }
    }
	
	// Update is called once per frame
    void Update () 
    {
        if (realTimeUpdate)
        {
            UpdateSkin();
        }
    }
}
