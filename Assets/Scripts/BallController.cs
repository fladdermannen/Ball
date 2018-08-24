using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    [HideInInspector]
    public GameManager gameManager;

    public Transform positions;

    List<Transform> path = new List<Transform>();

    int origin;

    public float moveDelay = 0.2f;

    private void Start()
    {
        origin = 1;
        transform.position = positions.GetChild(1).position;

        path = RandomizePath(origin);

        StartCoroutine(FollowPath(path));

    }

    private void Update()
    { 


    }



    IEnumerator FollowPath(List<Transform> pathpositions)
    {

        for (int i = 0; i < pathpositions.Count; i++)
        {
            transform.position = pathpositions[i].position;
            yield return new WaitForSeconds(moveDelay);
        }

        //Här ändras inte origin
        Debug.Log(origin);

        path = RandomizePath(origin);
        StartCoroutine(FollowPath(path));
       
    }


    List<Transform> RandomizePath(int origin)
    {
        List<Transform> randomPath = new List<Transform>();

        if (origin < 3)
            randomPath = MakePath(origin);
        else if (origin >= 3)
        {
            randomPath = MakeReversePath(origin);
        }

        return randomPath;
    }

    List<Transform> MakePath(int origin)
    {
        List<Transform> newPath = new List<Transform>();
        int rn = Random.Range(0,3);

        for (int i = 0; i < 6; i++)
        {
            newPath.Add(positions.GetChild(origin).GetChild(rn).GetChild(i).transform);
        }

        Path endpos = positions.GetChild(origin).GetChild(rn).GetComponent<Path>();

        //Här ändras origin
        origin = endpos.end;

        return newPath;
    }
    
    List<Transform> MakeReversePath(int origin)
    {
        List<Transform> newReversePath = new List<Transform>();
        int rn = Random.Range(0, 3);
        newReversePath.Add(positions.GetChild(rn).transform);

        Path endpos = positions.GetChild(0).GetChild(0).GetComponent<Path>();

        switch (origin)
        {
            case 3:
                for (int i = 0; i < 6; i++)
                {
                    newReversePath.Add(positions.GetChild(rn).GetChild(0).GetChild(i).transform);
                }
                endpos = positions.GetChild(rn).GetChild(0).GetComponent<Path>();

                break;
            case 4:
                for (int i = 0; i < 6; i++)
                {
                    newReversePath.Add(positions.GetChild(rn).GetChild(1).GetChild(i).transform);
                }
                endpos = positions.GetChild(rn).GetChild(0).GetComponent<Path>();
                break;
            case 5:
                for (int i = 0; i < 6; i++)
                {
                    newReversePath.Add(positions.GetChild(rn).GetChild(2).GetChild(i).transform);
                }
                endpos = positions.GetChild(rn).GetChild(0).GetComponent<Path>();
                break;

        }

        origin = endpos.start;



        newReversePath.Reverse();
        return newReversePath;
    }
}
