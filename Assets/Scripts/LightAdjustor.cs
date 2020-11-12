using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAdjustor : MonoBehaviour {
    public Light lightO2;
    public Light lightN;
    float startTime;

    //intensity variables
    public float intensitySpeed = 1.0f;
    public float maxIntensity = 10.0f;

    //color variables
    public float colorSpeed = 1.0f;
    public Color startColor;
    public Color endColor;

    // Start is called before the first frame update
    void Start () {
        lightO2 = GetComponent<Light> ();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update () {
        lightO2.intensity = Mathf.PingPong (Time.time * intensitySpeed, maxIntensity);

        float t = (Mathf.Sin (Time.time - startTime * colorSpeed));
        lightO2.color = Color.Lerp (startColor, endColor, t);
    }
}