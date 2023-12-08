using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageTrigger : MonoBehaviour
{
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            if (heart3.activeSelf)
            {
                heart3.SetActive(false);
            }
            else if (heart2.activeSelf)
            {
                heart2.SetActive(false);
            }
            else
            {
                heart1.SetActive(false);
                SceneManager.LoadScene("Alley");
            }


        }
    }
}
