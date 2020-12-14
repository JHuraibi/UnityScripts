using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class collectObjects : MonoBehaviour {

	// Use this for initialization
	int score;
    public AudioClip pickupSound;
	void Start () {

		score = 0;
		
	}
	// Update is called once per frame
	void OnControllerColliderHit(ControllerColliderHit hit)
	{ 
		if (hit.collider.gameObject.tag == "pick_me")
		{
			string label = hit.collider.gameObject.tag;
			print ("collision with "+ label);
			score = score + 1;
			if (score >= 4) SceneManager.LoadScene("scene2");
			print ("score" + score);
			Destroy (hit.collider.gameObject);
            gameObject.GetComponent<AudioSource>().clip= pickupSound;
			gameObject.GetComponent<AudioSource>().Play();	
		}
	}
	void Update () {

	}



}
