using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject FiringPoint;
    public AudioSource audioFile;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    public void Fire()
    {
        int bullets = 1;
        if (Vector3.Distance(GameObject.Find("Right Controller").transform.position, GameObject.Find("Left Controller").transform.position) < .04)
        {
            bullets = 3;
        }
        for (int i = 0; i < bullets; i++)
        {
            GameObject newBullet = Instantiate(bullet, FiringPoint.transform.position, FiringPoint.transform.rotation);
            newBullet.SetActive(true);
            newBullet.tag = "Bullet";
            newBullet.GetComponent<Rigidbody>().AddForce(FiringPoint.transform.forward * 1500);
        }
        audioFile.Play();
    }
}
