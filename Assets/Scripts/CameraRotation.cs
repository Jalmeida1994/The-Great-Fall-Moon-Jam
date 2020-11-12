using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    private float speed = 100f;
    private PlayerController playerControllerScript;
    private Touch theTouch;
    public FuelBar fuelBar;
    private float fuelLeft;
    private float rotationSpeed;
    private float rotationX;
    public ParticleSystem SteamLeft;
    public ParticleSystem SteamRight;
    public bool isTouching;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        fuelLeft = 15.0f;
    }
    void Update()
    {
        if (Input.touchCount > 0)
        {
            isTouching = true;
            theTouch = Input.GetTouch(0);
            speed = 20f;
            SteamLeft.Play();
            SteamRight.Play();
            fuelLeft -= Time.deltaTime;
            fuelBar.SetFuel(fuelLeft);

            if (theTouch.phase == TouchPhase.Ended)
            {
                isTouching = false;
                speed = 100f;
                SteamLeft.Stop();
                SteamRight.Stop();
            }
        }
        transform.RotateAround(this.transform.position, new Vector3(1, 0, 0), speed * Time.deltaTime);

        rotationX = Input.acceleration.x * rotationSpeed * Time.deltaTime;
        if (Input.acceleration.x >= 0 && transform.position.x < 7.5f)
        {
            //transform.RotateAround(dirX, 0, 0);
        }
        else if (Input.acceleration.x <= 0 && transform.position.x > -7.5f)
        {
            //transform.RotateAround(dirX, 0, 0);
        }
    }
}