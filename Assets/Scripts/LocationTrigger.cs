using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.XR.CoreUtils;


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
            if (oldScore == "300")
            {
                SceneManager.LoadScene("Alley");
            }
            score.text = "Score: " + (int.Parse(oldScore) + 100);
            opponent.GetComponent<CowBoyShoot>().enabled = false;
            List<GameObject> allChildren = GetAllChildren(opponent);
            foreach (GameObject child in allChildren)
            {
                Rigidbody childRigidbody = child.GetComponent<Rigidbody>();
                if (childRigidbody != null)
                {
                    // Activate gravity for the GameObject with Rigidbody
                    childRigidbody.useGravity = true;
                }
            }

        }
    }

    List<GameObject> GetAllChildren(GameObject parent)
    {
        List<GameObject> childList = new List<GameObject>();

        foreach (Transform child in parent.transform)
        {
            // Add the immediate child
            childList.Add(child.gameObject);

            // Recursively add all children of the child
            List<GameObject> childrenOfChild = GetAllChildren(child.gameObject);
            childList.AddRange(childrenOfChild);
        }

        return childList;
    }

}
