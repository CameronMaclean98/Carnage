using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temple_Animations : MonoBehaviour
{
    public float speedx;
    public float speedy;
    public float speedz;
    public int directionx;
    public int directiony;
    public int directionz;
    public float speedMult;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        speedMult = 100;
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.deltaTime;
        transform.Rotate(speedx * directionx * time * speedMult, speedy * directiony * time * speedMult, speedz * directionz * time* speedMult);
    }
}
