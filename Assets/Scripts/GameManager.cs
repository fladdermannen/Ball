using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject juggler;
    public GameObject ballPrefab;
    public GameObject scoreObject;
    public GameObject highScoreObject;
    public GameObject button;
    [Range(1,6)]
    public int ballAmount = 3;
    public float spawnDelay = 1.9f;

    List<GameObject> balls = new List<GameObject>();
    List<Color> ballColors = new List<Color>();
    GameObject droppedBall;

    public Collider2D jugglerCollider0;
    public Collider2D jugglerCollider1;
    public Collider2D jugglerCollider2;
    public Collider2D jugglerCollider3;
    public Collider2D jugglerCollider4;
    public Collider2D jugglerCollider5;

    Coroutine highlight;
    Coroutine newGame;
    int score = 0;
    int highscore;


    // Use this for initialization
    void Start()
    {

        NewGame();

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator NewBall(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject newBall = Instantiate(ballPrefab);
            newBall.GetComponentInChildren<BallController>().gameManager = this;
            int rn = Random.Range(0, ballColors.Count);
            newBall.GetComponent<SpriteRenderer>().color = ballColors[rn];
            Color colorToRemove = ballColors[rn];
            ballColors.Remove(colorToRemove);
            balls.Add(newBall);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public bool CheckCrash(GameObject ball)
    {
       


        LayerMask mask = LayerMask.GetMask("Juggler");

        RaycastHit2D hit = Physics2D.Raycast(ball.transform.position, Vector2.down, Mathf.Infinity, mask);
        if (hit.collider != null)
        {
            //Debug.Log("Collision: " + hit.collider.gameObject.name);
            AddPoint();
            return false;
        }
        else
        {
            GameOver(ball);
            return true;
        }
    }

    public void AddPoint()
    {
        score++;
        scoreObject.GetComponent<ScoreController>().SetPoints(score);

        if (score % 5 == 0)
        {
            Debug.Log("Accelerating.");
            foreach (GameObject ball in balls)
            {
                Accelerate(ball);
            }
        }
    }

    void GameOver(GameObject dropBall)
    {
        //stop spawning new balls
        StopCoroutine(newGame);

        //save dropped ball for highlighting
        balls.Remove(dropBall);
        droppedBall = dropBall;
        highlight = StartCoroutine(HighlightDroppedBall(droppedBall));

        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
        
        if (score > highscore)
        {
            highscore = score;
            highScoreObject.GetComponent<ScoreController>().SetPoints(highscore);
        } 
        score = 0;
        scoreObject.GetComponent<ScoreController>().SetPoints(score);
        button.SetActive(true);

        juggler.GetComponent<JugglerController>().inputLock = true;
        Debug.Log("Game Over");
    }

    public void NewGame()
    {
        if(highlight != null)
            StopCoroutine(highlight);
        button.SetActive(false);
        Destroy(droppedBall);

        ballColors.Clear();
        ballColors.Add(Color.magenta);
        ballColors.Add(Color.blue);
        ballColors.Add(Color.red);
        ballColors.Add(Color.green);
        ballColors.Add(Color.yellow);
        ballColors.Add(Color.cyan);

        juggler.GetComponent<JugglerController>().ResetJuggler();
        newGame = StartCoroutine(NewBall(ballAmount));
    }

    public void Accelerate(GameObject ball)
    {
        if (ball != null) 
            ball.GetComponent<BallController>().moveDelay -= 0.1f;
    }

    IEnumerator HighlightDroppedBall(GameObject ball)
    {
        bool show = true;

        for (int i = 0; i < 100; i++)
        {
            ball.SetActive(show);
            show = !show;
            yield return new WaitForSeconds(0.5f);
        }
    }
    
}
