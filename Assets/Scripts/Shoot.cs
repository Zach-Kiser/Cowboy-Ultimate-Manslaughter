using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

public class Shoot : MonoBehaviour
{
    public InputActionReference triggerInputActionReference;
    public GameObject bullet;
    public GameObject FiringPoint;
    public AudioSource audioFile;
    public GameObject leftController;
    public GameObject muzzleFlash;
    public GameObject rotator;

    public int bullets;
    private bool canShoot;

    void Start()
    {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Currently fan the hammer works if you just hold the trigger down.
        // Make it where if the hands are close enough, fan the hammer will engage.
        // It shouldn't engage in ANY other way.

        float triggerValue = triggerInputActionReference.action.ReadValue<float>();

        // Checks that the gun can fire currectly.
        if (triggerValue > 0.2 && bullets > 0 && canShoot)
        {
            // Checks if hands are close enough to do the "fan the hammer."
            if ((Vector3.Distance(leftController.transform.position, transform.position) < 0.2) && Input.GetButton("Fire1"))
            {
                StartCoroutine(Fire(bullets));
            }

            // Single shot
            else
            {
                StartCoroutine(Fire(1));
            }
        }

        //Temp Reload Function
        if (Input.GetButtonDown("Fire2"))
        {
            bullets = 6;
        }
    }

    IEnumerator Fire(int amount)
    {
        canShoot = false;

        // Creates Bullet, and sends it forward.
        GameObject newBullet = Instantiate(bullet, FiringPoint.transform.position, FiringPoint.transform.rotation);
        newBullet.SetActive(true);
        newBullet.tag = "Bullet";
        newBullet.GetComponent<Rigidbody>().AddForce(FiringPoint.transform.forward * 1500);

        audioFile.Play();
        bullets--;

        // Muzzleflash animation.
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        muzzleFlash.SetActive(false);

        // Cylinder rotation animation.
        for (int i = 0; i < 30; i++)
        {
            rotator.transform.Rotate(0f, 0f, 1.533f, Space.Self);
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.5f);
        Destroy(newBullet);
        canShoot = true;
    }
}
