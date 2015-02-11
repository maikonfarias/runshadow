using UnityEngine;
using System.Collections;

public class CharControllerScript : MonoBehaviour
{    
    public AudioClip musicSound;
    public AudioClip jumpSound;
    public float maxSpeed = 10f;
    bool facingRight = true;
    Animator anim;
    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float jumpForce = 700;
    private float currentMove = 0;

    private float progressiveSpeed = .8f;

    private bool beforeGameStarted = false;

    bool doubleJump = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        //audio.Play(musicSound, 0.3f);
        CheckAudioMute();

        //Invoke("PlayMusic", .2f);
        
    }

    public void PlayMusic()
    {
        //if (PlayerPrefs.GetInt("Sound") == 1)
        {
            audio.clip = musicSound;
            audio.volume = 0.2f;
            audio.Play();
        }
    }
    void CheckAudioMute()
    {
        if (Time.timeScale == 0f || PlayerPrefs.GetInt("Sound") == 0)
        {
            audio.mute = true;
        }
        else
        {
            audio.mute = false;
        }
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);

        anim.SetFloat("vSpeed", rigidbody2D.velocity.y);
        
        currentMove = progressiveSpeed;        

        anim.SetFloat("Speed", Mathf.Abs(currentMove));

        rigidbody2D.velocity = new Vector2(currentMove * maxSpeed, rigidbody2D.velocity.y);

        if (currentMove > 0 && !facingRight)
            Flip();
        else if (currentMove < 0 && facingRight)
            Flip();

    }

    void Update()
    {
        if (!audio.isPlaying && beforeGameStarted != HUDScript.GameStarted)
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
                if(buttonPos.Contains(T.position)){
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
        if ((grounded || !doubleJump) && (Input.GetKeyDown(KeyCode.Space) || clickedMouse || touchedScreen || pressedGamepad) && HUDScript.GameStarted)
        {
            PlaySound(jumpSound);
            anim.SetBool("Ground", false);
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
            rigidbody2D.AddForce(new Vector2(0, jumpForce));

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

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void PlaySound(AudioClip clip, float volume = 1f)
    {
        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            audio.PlayOneShot(clip, volume);
        }
    }

    void OnApplicationPause(bool pauseStatus) {
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
