using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingStuff : MonoBehaviour {
    public float speedAsteroid = 0.5f;
    private PlayerController playerControllerScript;
    private float destroyBound = -30;

    // Start is called before the first frame update
    void Start () {
        playerControllerScript = GameObject.Find ("Player").GetComponent<PlayerController> ();
    }

    // Update is called once per frame
    void Update () {
        if (playerControllerScript.gameOver == false) {
            if (gameObject.CompareTag ("Asteroid")) {
                transform.Translate (Vector3.back * Time.deltaTime * speedAsteroid, Space.World);
            } else if (gameObject.CompareTag ("O2")) {
                float bottleSpeed = Random.Range (0.5f, 2.3f);
                transform.Translate (Vector3.back * Time.deltaTime * bottleSpeed, Space.World);

            } else if (gameObject.CompareTag ("Fuel")) {
                float bottleSpeed = Random.Range (0.5f, 2.3f);
                transform.Translate (Vector3.back * Time.deltaTime * bottleSpeed, Space.World);

            }
        }
        if (transform.position.z < destroyBound) {
            Destroy (gameObject);
        }
    }

}