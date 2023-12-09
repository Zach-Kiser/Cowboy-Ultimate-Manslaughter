using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;
using static UnityEngine.Random;

//  TODO: Someone should make the "cowboy" model itself face the player. 
//  Right now, the cowboy is just standing blankly.
public class CowBoyShoot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject FiringPoint;
    public AudioSource audioFile;
    public GameObject player;

    public int bullets;
    private bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
    }

    void Update()
    {
        if (canShoot && Random.value < .01)
        {
            StartCoroutine(Fire(1));
        }
    }

    IEnumerator Fire(int amount)
    {
        canShoot = false;

        FiringPoint.transform.LookAt(player.transform);
        GameObject newBullet = Instantiate(bullet, FiringPoint.transform.position, FiringPoint.transform.rotation);
        newBullet.SetActive(true);
        // make the bullets massive so you can see them
        newBullet.transform.localScale += new Vector3(3f, 3f, 3f);
        newBullet.tag = "EnemyBullet";
        newBullet.GetComponent<Rigidbody>().AddForce(FiringPoint.transform.forward * 1000);

        audioFile.Play();
        bullets--;


        yield return new WaitForSeconds(Random.value * 10);
        Destroy(newBullet);
        canShoot = true;
    }
}

