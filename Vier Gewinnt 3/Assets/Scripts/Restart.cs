using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
	public void LoadScene(string GameScene){			
		SceneManager.LoadScene("GameScene");			//L�dt die Szene neu 
	}
}
