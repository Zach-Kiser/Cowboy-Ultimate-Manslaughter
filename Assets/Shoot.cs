using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject FiringPoint;
    public AudioSource audioFile;
    private int bullets;
    private bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        // Made it where you only have 6 bullets.
        // However, this means there needs to be some sort
        // of reload functionality.
        bullets = 6;
        canShoot = true;
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
        // Kiser, what is this code for??

        // int bullets = 1;
        // if (Vector3.Distance(GameObject.Find("Right Controller").transform.position, GameObject.Find("Left Controller").transform.position) < .04)
        // {
        //     bullets = 3;
        // }

        // for (int i = 0; i < bullets; i++)

        if (bullets > 0 && canShoot)
        {
            GameObject newBullet = Instantiate(bullet, FiringPoint.transform.position, FiringPoint.transform.rotation);
            newBullet.SetActive(true);
            newBullet.tag = "Bullet";
            newBullet.GetComponent<Rigidbody>().AddForce(FiringPoint.transform.forward * 1500);
            audioFile.Play();
            StartCoroutine(fireDelay(newBullet));
            bullets--;
        }
    }

    // Function to deal with trigger spam.
    IEnumerator fireDelay(GameObject bullet)
    {
        canShoot = false;
        yield return new WaitForSeconds(0.75f);
        // Fixed infinite bullets being made.
        Destroy(bullet);
        canShoot = true;
    }
}
