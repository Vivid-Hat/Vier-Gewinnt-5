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
    	gameManager = GameObject.Find("GameManager");                           //Findet den GameManager als GameObject und setzt die Variable gameManager
    	gameManagerClass = gameManager.GetComponent<GameManager>();             //Nutzt die gameManager Variable um die Komponente GameManager der Variablen gameManagerClass zuzuordnen
    	playerColor = gameManagerClass.activePlayer;                            //Aktive Spielerfarbe wird der Variablen playerColor zugeordnet

    	this.GetComponent<ColorObject>().SetColor(playerColor, false);          //Setzt die Farbe des Chips auf die Spielerfarbe. Keine highlight color = false
    } 
}