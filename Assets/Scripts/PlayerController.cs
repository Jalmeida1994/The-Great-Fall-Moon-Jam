using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private float dirX;
    private float fadeOutTime = 2f;
    private float moveSpeed = 6.5f;
    public bool gameOver = false;
    public float score = 0.0f;
    public float timeLeft = 15.0f;
    public float fuelLeft = 15.0f;
    public Animator fadeAnimator;
    public Button restartButton;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI timeLeftText;
    public TextMeshProUGUI plusTimeText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI gameOverScoreText;
    public TextMeshProUGUI gameOverHiScoreText;
    private Color originalColor;
    private Color originalColorGO;
    public FuelBar fuelBar;
    public AudioClip fuelSound;
    public AudioClip oxigenSound;
     public AudioClip collisionSound;
    private AudioSource playerAudio;
    private CameraRotation cameraRotationScript;


    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        playerRb = GetComponent<Rigidbody>();
        timeLeft = 15.0f;
        fuelLeft = 15.0f;
        score = 0.0f;
        highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        fuelBar.SetMaxFuel(fuelLeft);
        SetTimeLeftText();
        originalColor = plusTimeText.color;
        originalColorGO = scoreText.color;
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            gameOverFunction();
        }
        if (!gameOver)
        {
            //score functions
            score += Time.deltaTime;
            setScoreText();

            //highscore functions
            if (Mathf.RoundToInt(score) > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", Mathf.RoundToInt(score));
                highScoreText.text = Mathf.RoundToInt(score).ToString();
            }

            //timer left functions
            SetTimeLeftText();

            //movement
            dirX = Input.acceleration.x * moveSpeed * Time.deltaTime;
            if (Input.acceleration.x >= 0 && transform.position.x < 7.5f)
            {
                transform.Translate(dirX, 0, 0);
            }
            else if (Input.acceleration.x <= 0 && transform.position.x > -7.5f)
            {
                transform.Translate(dirX, 0, 0);
            }
            if ((Input.GetKeyDown(KeyCode.LeftArrow)))
            {
                playerRb.AddForce(Vector3.right * moveSpeed * -5);
            }
            if ((Input.GetKeyDown(KeyCode.RightArrow)))
            {
                playerRb.AddForce(Vector3.right * moveSpeed * 5);
            }


        }
    }

    private void gameOverFunction()
    {
        gameOver = true;
        fadeAnimator.SetTrigger("FadeOut");
        Debug.Log("Game Over");

        gameOverText.text = "GAME OVER";
        restartText.text = "touch anywhere to restart";
        gameOverScoreText.text = scoreText.text;
        gameOverHiScoreText.text = highScoreText.text;
        FadeIn();
        restartButton.gameObject.SetActive(true);
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInRoutine());
    }

    private IEnumerator FadeInRoutine()
    {
        for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
        {
            gameOverText.color = Color.Lerp(Color.clear, originalColorGO, Mathf.Min(1, t / fadeOutTime));
            restartText.color = Color.Lerp(Color.clear, originalColorGO, Mathf.Min(1, t / fadeOutTime));
            gameOverScoreText.color = Color.Lerp(Color.clear, originalColorGO, Mathf.Min(1, t / fadeOutTime));
            gameOverHiScoreText.color = Color.Lerp(Color.clear, originalColor, Mathf.Min(1, t / fadeOutTime));

            yield return null;
        }
    }

    void setScoreText()
    {
        scoreText.text = Mathf.RoundToInt(score).ToString();
    }

    void SetTimeLeftText()
    {
        timeLeftText.text = Mathf.CeilToInt(timeLeft).ToString();
    }

    void SetPlusTimeText()
    {
        plusTimeText.text = "+10";
        plusTimeText.color = originalColor;
        FadeOut();

    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
        {
            plusTimeText.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / fadeOutTime));
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {   
            playerAudio.PlayOneShot(collisionSound, 0.7f);
            gameOverFunction();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("O2") && gameOver == false)
        {
            timeLeft += 10.0f;
            SetPlusTimeText();
            playerAudio.PlayOneShot(oxigenSound, 0.7f);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Fuel") && gameOver == false)
        {
            fuelLeft += 3.0f;
            fuelBar.SetFuel(fuelLeft);
            playerAudio.PlayOneShot(fuelSound, 0.7f);
            Destroy(other.gameObject);
        }
    }
}