using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class LocationTrigger : MonoBehaviour
{
    public GameObject opponent; 
    public TextMeshProUGUI score;

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
        if (other.CompareTag("Bullet"))
        {
            string oldScore = score.text.Split("Score: ")[1];
            if (oldScore == "500")
            {
                SceneManager.LoadScene("Alley");
            }
            score.text = "Score: " + (int.Parse(oldScore) + 100);
            opponent.SetActive(false);
        }
    }
}
