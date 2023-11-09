using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            other.GetComponent<TrailRenderer>().enabled = false;
            StartCoroutine(startup());
        }
    }

    IEnumerator startup() {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainScene");
    }

    
}
