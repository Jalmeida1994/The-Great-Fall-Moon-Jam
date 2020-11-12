using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {
    public TextMeshProUGUI highScoreText;

    // Start is called before the first frame update
    void Start () {
        highScoreText.text = PlayerPrefs.GetInt ("HighScore", 0).ToString ();
    }

    // Update is called once per frame
    void Update () {
        if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
            SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
        }
    }
}