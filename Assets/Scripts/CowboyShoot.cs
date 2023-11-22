using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;
using static UnityEngine.Random;

public class CowBoyShoot : MonoBehaviour
{
    //public InputActionReference triggerInputActionReference;
    public GameObject bullet;
    public GameObject FiringPoint;
    public AudioSource audioFile;
    //public GameObject leftController;
    //public GameObject muzzleFlash;
    //public GameObject cylinder;
    //public GameObject clinder2;

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

        //float triggerValue = triggerInputActionReference.action.ReadValue<float>();

        // TODO: Fix bug with Fan the Hammer. Currently, in the Alley Scene, I gave the player 100 bullets.
        // However, with our current implementation of Fan the Hammer, that just shoots out 100 bullets no
        // matter what. We need some kind of system to check if the player has left the original "Fan the
        // Hammer" distance AND if they have stop holding the trigger (fire1).
        if (canShoot && Random.value < .01)
        {
            StartCoroutine(Fire(1));
        }
    }

    IEnumerator Fire(int amount)
    {
        // while (Input.GetButton("Fire1") && amount > 0)
        // {
        canShoot = false;

        GameObject newBullet = Instantiate(bullet, FiringPoint.transform.position, FiringPoint.transform.rotation);
        newBullet.SetActive(true);
        // make the bullets massive so you can see them
        newBullet.transform.localScale += new Vector3(3f, 3f, 3f);
        newBullet.tag = "EnemyBullet";
        newBullet.GetComponent<Rigidbody>().AddForce(FiringPoint.transform.forward * 1000);

        audioFile.Play();
        bullets--;

        //cylinder.transform.RotateAround(center.transform.position, Vector3.right, 10f);

        // Couldn't get rotation to work, so my current solution is to just
        // show and hide a slightly rotated cylinder
        /*if (cylinder.activeSelf)
        {
            cylinder.SetActive(false);
            clinder2.SetActive(true);
        }
        else
        {
            cylinder.SetActive(true);
            clinder2.SetActive(false);
        }*/

        /*muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        muzzleFlash.SetActive(false);
        */


        yield return new WaitForSeconds(Random.value * 10);
        Destroy(newBullet);
        canShoot = true;
        // }
    }
}

