using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class NextLevel : MonoBehaviour
{
    public string nextLevelName;
    private bool nameValid = true;

    private void Start()
    {

        if (nextLevelName == "")
        {
            Debug.Log("No Level Name Was Given");
            nameValid = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (nameValid)
        {
            Debug.Log("COLLIDED");
            SceneManager.LoadScene(nextLevelName);
        }
    }
}
