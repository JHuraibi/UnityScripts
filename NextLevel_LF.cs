using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class NextLevel_LF : MonoBehaviour
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
            SceneManager.LoadScene(nextLevelName);
        }
    }
}
