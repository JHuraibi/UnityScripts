using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class timer : MonoBehaviour {

	// Use this for initialization
	float time;
	void Start()
	{
		time = 0.0f;
		GameObject.Find("timerUI").GetComponent<Text>().text = "";

		GameObject.Find("userMessageUI").GetComponent<Text>().text = "";

	}
	void Update()
	{
		time = time + Time.deltaTime;
		int seconds = (int) (time%60);
		int minutes = (int) (time/60); 
		//print (minutes+":"+seconds);
		GameObject.Find("timerUI").GetComponent<Text>().text = minutes + ":"+ seconds;

		if (time > 118)
		{
			GameObject.Find("userMessageUI").GetComponent<Text>().text = "Time Almost Up.";

		}


		if (time > 120)
		{
			print ("TIME UP");
			SceneManager.LoadScene("indoor");
		}

	}
}
