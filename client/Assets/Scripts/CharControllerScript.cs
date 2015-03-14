using UnityEngine;
using System.Collections;

public class CharControllerScript : MonoBehaviour
{
  public static bool ForceJump = false;

  public AudioClip musicSound;
  public AudioClip jumpSound;
  public float maxSpeed = 10f;
  Animator anim;
  bool grounded = false;
  public Transform groundCheck;
  float groundRadius = 0.2f;
  public LayerMask whatIsGround;
  public float jumpForce = 700;
  private float currentMove = 0;

  private float progressiveSpeed = .8f;

  private bool beforeGameStarted = false;
  GameObject mainCamera;

  bool doubleJump = false;

  void Start()
  {
    mainCamera = GameObject.Find("Main Camera");
    anim = GetComponent<Animator>();
    //audio.Play(musicSound, 0.3f);
    CheckAudioMute();

    //Invoke("PlayMusic", .2f);

  }

  public void PlayMusic()
  {
    //if (PlayerPrefs.GetInt("Sound") == 1)
    {
      mainCamera.GetComponent<AudioSource>().clip = musicSound;
      mainCamera.GetComponent<AudioSource>().volume = 0.2f;
      mainCamera.GetComponent<AudioSource>().Play();
    }
  }
  void CheckAudioMute()
  {
    if (Time.timeScale == 0f || PlayerPrefs.GetInt("Sound") == 0)
    {
      mainCamera.GetComponent<AudioSource>().mute = true;
    }
    else
    {
      mainCamera.GetComponent<AudioSource>().mute = false;
    }
  }

  void FixedUpdate()
  {
    grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
    anim.SetBool("Ground", grounded);

    anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

    currentMove = progressiveSpeed;

    anim.SetFloat("Speed", Mathf.Abs(currentMove));

    GetComponent<Rigidbody2D>().velocity = new Vector2(currentMove * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
  }

  void Update()
  {
    if (!mainCamera.GetComponent<AudioSource>().isPlaying && beforeGameStarted != HUDScript.GameStarted)
    {
      //Debug.Log("play music");
      PlayMusic();
      beforeGameStarted = HUDScript.GameStarted;
    }


    if (HUDScript.GameStarted)
    {
      progressiveSpeed += Time.deltaTime / 400;
    }

    CheckAudioMute();

    if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.JoystickButton7)))
    {
      if (HUDScript.GameStarted)
      {
        if (Time.timeScale == 1.0f)
        {
          Time.timeScale = 0.0f;
          return;
        }
        else
        {
          Time.timeScale = 1.0f;
        }
      }
    }

    if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.JoystickButton3))
    {
      Time.timeScale = 1.0f;
      HUDScript.GameStarted = true;
      Application.LoadLevel(Application.loadedLevel);
      return;
    }

    if (Input.GetKeyDown(KeyCode.JoystickButton6) && Time.timeScale == 0f)
    {
      Time.timeScale = 1.0f;
      HUDScript.GameStarted = false;
      Application.LoadLevel(Application.loadedLevel);
      return;
    }

    if (Time.timeScale != 1.0f)
    {
      return;
    }

    bool touchedScreen = false;
    foreach (var T in Input.touches)
    {
      //var P = T.position; // touch pos
      if (T.phase == TouchPhase.Began)
      {
        var buttonSize = Screen.height * 0.2f;
        var buttonPos = new Rect(buttonSize / 4, buttonSize / 4, buttonSize, buttonSize);
        if (buttonPos.Contains(T.position))
        {
          touchedScreen = true;
        }
      }
    }

    bool pressedGamepad = false;
    if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.JoystickButton2)
        || Input.GetKeyDown(KeyCode.JoystickButton4) || Input.GetKeyDown(KeyCode.JoystickButton5))
    {
      pressedGamepad = true;
    }

    bool clickedMouse = false;
    if (Input.GetMouseButtonDown(0))
    {
      var buttonSize = Screen.height * 0.2f;
      var buttonPos = new Rect(buttonSize / 4, buttonSize / 4, buttonSize, buttonSize);
      if (!buttonPos.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
      {
        clickedMouse = true;
      }
    }

    // if pressed any button/click/touch
    if ((grounded || !doubleJump) && (Input.GetKeyDown(KeyCode.Space) || clickedMouse || touchedScreen || pressedGamepad || ForceJump) && HUDScript.GameStarted)
    {
      Jump();

      if (!grounded)
      {
        doubleJump = true;
      }
    }
    if (grounded)
    {
      doubleJump = false;
    }
  }

  void Jump()
  {
    PlaySound(jumpSound);
    anim.SetBool("Ground", false);
    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
    ForceJump = false;
  }

  void PlaySound(AudioClip clip, float volume = 1f)
  {
    if (PlayerPrefs.GetInt("Sound") == 1)
    {
      mainCamera.GetComponent<AudioSource>().PlayOneShot(clip, volume);
    }
  }

  void OnApplicationPause(bool pauseStatus)
  {
    if (pauseStatus && HUDScript.GameStarted)
    {
      Time.timeScale = 0.0f;
    }
  }

  void OnGUI()
  {
    //GUI.Box(new Rect(0, 0, 250, 50), "progressiveSpeed: " + progressiveSpeed.ToString());
  }


}
