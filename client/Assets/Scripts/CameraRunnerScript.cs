using UnityEngine;
using System.Collections;

public class CameraRunnerScript : MonoBehaviour {

    public Transform startPosition;
    public GameObject[] characters;

    Transform player;

    void Start() 
    {
        int currentSkin = PlayerPrefs.GetInt("MapSkin", 0);
        if (currentSkin == 1)
        {
            GetComponent<Camera>().backgroundColor = Color.white;
        }
        var playerChar = GetPlayerCharacter();
        
        var playerObj = (GameObject)Instantiate(playerChar, startPosition.position, Quaternion.identity);
        player = playerObj.transform;
        
    }

    public void UpdateCurrentCharacterSkin(){
        //var player = GameObject.FindGameObjectsWithTag("Player")[0];
        var pos = player.position;
        Destroy(player.gameObject);

        var playerObj = (GameObject)Instantiate(GetPlayerCharacter(), pos, Quaternion.identity);
        player = playerObj.transform;
    }

	void Update () {

        //TODO make camera position 20% relative to character
        //var extra = Screen.height*0.25;
        if (Screen.height * 1.25 > Screen.width)  // portrait workaround
        {
            transform.position = new Vector3(player.position.x + 2.5f, 0, -10);
        }
        else if ( Screen.height * 1.35 > Screen.width)  // portrait workaround
        {
            transform.position = new Vector3(player.position.x + 3.2f, 0, -10);
        }
        else if (Screen.height * 1.55 > Screen.width)  // portrait workaround
        {
            transform.position = new Vector3(player.position.x + 4f, 0, -10);
        }
        else
        {
            transform.position = new Vector3(player.position.x + 5, 0, -10);
        }
	
	}

    GameObject GetPlayerCharacter()
    {
        return characters[PlayerPrefs.GetInt("SelectedCharacter", 0)];
    }
}
