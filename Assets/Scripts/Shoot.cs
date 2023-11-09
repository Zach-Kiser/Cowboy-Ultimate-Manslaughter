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
    public int bullets;
    private bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {

        float triggerValue = triggerInputActionReference.action.ReadValue<float>();

        // TODO: Fix bug with Fan the Hammer. Currently, in the Alley Scene, I gave the player 100 bullets.
        // However, with our current implementation of Fan the Hammer, that just shoots out 100 bullets no
        // matter what. We need some kind of system to check if the player has left the original "Fan the
        // Hammer" distance AND if they have stop holding the trigger (fire1).
        if (triggerValue > 0.2 && bullets > 0 && canShoot)
        {
            if ((Vector3.Distance(leftController.transform.position, transform.position) < 0.2) && Input.GetButton("Fire1"))
            {
                StartCoroutine(Fire(bullets));
            }

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
        // while (Input.GetButton("Fire1") && amount > 0)
        // {
        canShoot = false;

        GameObject newBullet = Instantiate(bullet, FiringPoint.transform.position, FiringPoint.transform.rotation);
        newBullet.SetActive(true);
        newBullet.tag = "Bullet";
        newBullet.GetComponent<Rigidbody>().AddForce(FiringPoint.transform.forward * 1500);

        audioFile.Play();
        bullets--;

        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        muzzleFlash.SetActive(false);


        yield return new WaitForSeconds(0.5f);
        Destroy(newBullet);
        canShoot = true;
        // }
    }
}
