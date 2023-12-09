using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;
using TMPro;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    public InputActionReference triggerInputActionReference;
    public GameObject bullet;
    public GameObject FiringPoint;
    public AudioSource audioFile;
    public GameObject leftController;
    public GameObject muzzleFlash;
    public GameObject rotator;
    public TextMeshProUGUI textmeshPro;

    public int bullets;
    private bool canShoot;
    private bool canFan;
    public Slider coolDownSlider;


    void Start()
    {
        canShoot = true;
        canFan = true;
        textmeshPro.SetText("{}", bullets);
        coolDownSlider.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = triggerInputActionReference.action.ReadValue<float>();

        // Checks that the gun can fire currectly.
        if (triggerValue > 0.2 && bullets > 0 && canShoot)
        {
            // Checks if hands are close enough to do the "fan the hammer."
            if ((Vector3.Distance(leftController.transform.position, transform.position) < 0.2) && Input.GetButton("Fire1"))
            {
                if (canFan)
                    StartCoroutine(Fire(6, true));
            }

            // Single shot
            else
            {
                StartCoroutine(Fire(1, false));
            }
        }

        //Temp Reload Function
        if (Input.GetButtonDown("Fire2"))
        {
            bullets = 6;
            textmeshPro.SetText("{}", bullets);
        }
        if (!canFan)
        {
            coolDownSlider.value += Time.deltaTime * .1f;
        }
    }

    // Make hammer false if you want there to be a delay between shots
    IEnumerator Fire(int amount, bool hammer)
    {
        for (int i = 0; i < amount; i++)
        {
            canShoot = false;
            canFan = false;
            // Creates Bullet, and sends it forward.
            GameObject newBullet = Instantiate(bullet, FiringPoint.transform.position, FiringPoint.transform.rotation);
            newBullet.SetActive(true);
            newBullet.tag = "Bullet";
            newBullet.GetComponent<Rigidbody>().AddForce(FiringPoint.transform.forward * 1500);

            audioFile.Play();
            bullets--;
            textmeshPro.SetText("{}", bullets);

            // Muzzleflash animation.
            muzzleFlash.SetActive(true);
            yield return new WaitForSeconds(0.15f);
            muzzleFlash.SetActive(false);

            // Cylinder rotation animation.
            for (int j = 0; j < 30; j++)
            {
                rotator.transform.Rotate(0f, 0f, 1.533f, Space.Self);
                yield return new WaitForSeconds(0.01f);
            }
            if (hammer)
                yield return new WaitForSeconds(0.005f);
            else
                yield return new WaitForSeconds(2f);

            Destroy(newBullet);
        }
        // Create 10 second cooldown after fan the hammer
        if (hammer)
        {
            coolDownSlider.value = 0f;
            yield return new WaitForSeconds(10f);
        }
        canShoot = true;
        canFan = true;

    }
}
