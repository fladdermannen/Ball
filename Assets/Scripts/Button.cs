using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    public GameObject gameManager;

    private void OnMouseDown()
    {
        Debug.Log("New Game");
        gameManager.GetComponent<GameManager>().NewGame();
    }

}
