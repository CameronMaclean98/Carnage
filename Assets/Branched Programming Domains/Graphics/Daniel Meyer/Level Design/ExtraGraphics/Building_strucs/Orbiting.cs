using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbiting : MonoBehaviour
{
    public GameObject sphere;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        OrbitAround();
    }

    void OrbitAround()
    {
        transform.RotateAround(sphere.transform.position, Vector3.forward, speed * Time.deltaTime);
    }
}
