using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{

	public GameObject gameManager;
	public GameManager gameManagerClass;
	public Player playerColor;
    // Start is called before the first frame update
    void Start()
    {
    	gameManager = GameObject.Find("GameManager");
    	gameManagerClass = gameManager.GetComponent<GameManager>();
    	playerColor = gameManagerClass.activePlayer;

    	this.GetComponent<ColorObject>().SetColor(playerColor, false);   
    } 
}