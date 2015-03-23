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
    CheckAudioMute();
  }

  void FixedUpdate()
  {
    UpdatePhysics();
  }

  void Update()
  {
    if (!mainCamera.GetComponent<AudioSource>().isPlaying && beforeGameStarted != Game.Started)
    {
      PlayMusic();
      beforeGameStarted = Game.Started;
    }

    if (Game.Started)
    {
      progressiveSpeed += Time.deltaTime / 400;
    }

    CheckAudioMute();
    ProccessKeys();

    if (!Game.Paused)
    {
      if ((grounded || !doubleJump) && (TouchedTheScreen() || ForceJump) && Game.Started)
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
  }

  void UpdatePhysics()
  {
    grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
    anim.SetBool("Ground", grounded);
    anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
    currentMove = progressiveSpeed;
    anim.SetFloat("Speed", Mathf.Abs(currentMove));
    GetComponent<Rigidbody2D>().velocity = new Vector2(currentMove * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
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

  public void PlayMusic()
  {
    mainCamera.GetComponent<AudioSource>().clip = musicSound;
    mainCamera.GetComponent<AudioSource>().volume = 0.2f;
    mainCamera.GetComponent<AudioSource>().Play();
  }

  void CheckAudioMute()
  {
    if (Game.Paused || PlayerPrefs.GetInt("Sound") == 0)
    {
      mainCamera.GetComponent<AudioSource>().mute = true;
    }
    else
    {
      mainCamera.GetComponent<AudioSource>().mute = false;
    }
  }

  void OnApplicationPause(bool pauseStatus)
  {
    if (pauseStatus && Game.Started)
    {
      Game.Paused = true;
    }
  }

  bool TouchedTheScreen()
  {
    foreach (var T in Input.touches)
    {
      //var P = T.position; // touch pos
      if (T.phase == TouchPhase.Began)
      {
        var buttonSize = Screen.height * 0.2f;
        var buttonPos = new Rect(buttonSize / 4, buttonSize / 4, buttonSize, buttonSize);
        if (buttonPos.Contains(T.position))
        {
          //touch
          return true;
        }
      }
    }

    if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.JoystickButton2)
        || Input.GetKeyDown(KeyCode.JoystickButton4) || Input.GetKeyDown(KeyCode.JoystickButton5))
    {
      //joystick
      return true;
    }

    if (Input.GetMouseButtonDown(0))
    {
      var buttonSize = Screen.height * 0.2f;
      var buttonPos = new Rect(buttonSize / 4, buttonSize / 4, buttonSize, buttonSize);
      if (!buttonPos.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
      {
        //clicked
        return true;
      }
    }
    if (Input.GetKeyDown(KeyCode.Space))
    {
      //keyboard
      return true;
    }
    return false;
  }

  void ProccessKeys()
  {
    if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.JoystickButton7)))
    {
      if (Game.Started)
      {
        Game.Paused = !Game.Paused;
      }
    }

    if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.JoystickButton3))
    {
      Game.Paused = false;
      Game.Started = true;
      Application.LoadLevel(Application.loadedLevel);
      return;
    }

    if (Input.GetKeyDown(KeyCode.JoystickButton6) && Game.Paused)
    {
      Game.Paused = false;
      Game.Started = false;
      Application.LoadLevel(Application.loadedLevel);
      return;
    }
  }
}
