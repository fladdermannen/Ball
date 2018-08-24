using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public List<Transform> positions;
    public List<GameObject> balls;

    List<Transform> pathBall1 = new List<Transform>(); 
    List<Transform> pathBall2 = new List<Transform>();

    int originBall1;
    int originBall2;

    public float moveDelay = 1f;

    private void Start()
    { 

        //starting positions
        balls[0].transform.position = positions[1].position;
        balls[1].transform.position = positions[4].position;
        originBall1 = 1;
        originBall2 = 4;

        pathBall1 = RandomizePath(0, originBall1);
        pathBall2 = RandomizePath(1, originBall2);

        StartCoroutine(FollowPath(0, pathBall1));
        StartCoroutine(FollowPath(1, pathBall2));

    }

    private void Update()
    { 


    }



    IEnumerator FollowPath(int ball, List<Transform> pathpositions)
    {

        for (int i = 0; i < pathpositions.Count; i++)
        {
            balls[ball].transform.position = pathpositions[i].position;
            //Debug.Log("Moving " + ball);
            yield return new WaitForSeconds(moveDelay);
        }

        if (ball == 0)
        {
            pathBall1 = RandomizePath(ball, originBall1);
            StartCoroutine(FollowPath(ball, pathBall1));
        }
        else
        {
            pathBall2 = RandomizePath(ball, originBall2);
            StartCoroutine(FollowPath(ball, pathBall2));
        }
    }


    List<Transform> RandomizePath(int ball, int origin)
    {
        List<Transform> path = new List<Transform>();

        if (origin < 3)
            path = MakePath(ball, origin);
        else if (origin >= 3)
        {
            path = MakeReversePath(ball, origin);
        }

        return path;
    }

    List<Transform> MakePath(int ball, int origin)
    {
        List<Transform> path = new List<Transform>();
        int rn = Random.Range(0,3);

        for (int i = 0; i < 6; i++)
        {
            path.Add(positions[origin].GetChild(rn).GetChild(i).transform);
        }

        Path endpos = positions[origin].GetChild(rn).GetComponent<Path>();

        if (ball == 0)
        {
            originBall1 = endpos.end;
        }
        else
        {
            originBall2 = endpos.end;
        }

        return path;
    }
    
    List<Transform> MakeReversePath(int ball, int origin)
    {
        List<Transform> path = new List<Transform>();
        int rn = Random.Range(0, 3);
        path.Add(positions[rn].transform);

        Path endpos = positions[0].GetChild(0).GetComponent<Path>();

        switch (origin)
        {
            case 3:
                for (int i = 0; i < 6; i++)
                {
                    path.Add(positions[rn].GetChild(0).GetChild(i).transform);
                }
                endpos = positions[rn].GetChild(0).GetComponent<Path>();

                break;
            case 4:
                for (int i = 0; i < 6; i++)
                {
                    path.Add(positions[rn].GetChild(1).GetChild(i).transform);
                }
                endpos = positions[rn].GetChild(0).GetComponent<Path>();
                break;
            case 5:
                for (int i = 0; i < 6; i++)
                {
                    path.Add(positions[rn].GetChild(2).GetChild(i).transform);
                }
                endpos = positions[rn].GetChild(0).GetComponent<Path>();
                break;

        }

        if (ball == 0)
        {
            originBall1 = endpos.start;
        } else
        {
            originBall2 = endpos.start;
        }


        path.Reverse();
        return path;
    }
}
