using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject juggler;
    public GameObject ballPrefab;

    public Collider2D jugglerCollider0;
    public Collider2D jugglerCollider1;
    public Collider2D jugglerCollider2;
    public Collider2D jugglerCollider3;
    public Collider2D jugglerCollider4;
    public Collider2D jugglerCollider5;


    // Use this for initialization
    void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void NewBall()
    {
        GameObject newBall = Instantiate(ballPrefab);
        newBall.GetComponentInChildren<BallController>().gameManager = this;
    }
}
