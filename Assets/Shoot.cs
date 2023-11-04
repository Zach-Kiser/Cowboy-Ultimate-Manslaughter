using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem.XR;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject FiringPoint;
    public AudioSource audioFile;
    public GameObject leftController;
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
        if (Input.GetButtonDown("Fire1") && bullets > 0 && canShoot)
        {
            // Prints distance. It's pretty strict right now. We can change it later.
            Debug.Log(Vector3.Distance(leftController.transform.position, transform.position));

            if (Vector3.Distance(leftController.transform.position, transform.position) < 0.2)
            {
                StartCoroutine(Fire(bullets));
            }

            else
            {
                StartCoroutine(Fire(1));
            }
        }
    }

    IEnumerator Fire(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            canShoot = false;
            GameObject newBullet = Instantiate(bullet, FiringPoint.transform.position, FiringPoint.transform.rotation);
            newBullet.SetActive(true);
            newBullet.tag = "Bullet";
            newBullet.GetComponent<Rigidbody>().AddForce(FiringPoint.transform.forward * 1500);
            audioFile.Play();
            bullets--;
            
            yield return new WaitForSeconds(0.5f);
            canShoot = true;
        }
    }
}
