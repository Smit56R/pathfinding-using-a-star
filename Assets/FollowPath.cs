using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    Transform goal;
    public float speed = 5f;
    float accuracy = 1f;
    float rotSpeed = 2f;
    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int currentWP = 0;
    Graph g;

    // Start is called before the first frame update
    void Start()
    {
        wps = wpManager.GetComponent<WPManager>().waypoints;
        g = wpManager.GetComponent<WPManager>().graph;
        currentNode = wps[2];
    }

    public void GoToHeli()
    {
        g.AStar(currentNode, wps[0]);
        currentWP = 0;
    }

    public void GoToRuin()
    {
        g.AStar(currentNode, wps[4]);
        currentWP = 0;
    }

    public void GoToContainers()
    {
        g.AStar(currentNode, wps[6]);
        currentWP = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if(g.getPathLength() == 0 || currentWP == g.getPathLength())
        {
            return;
        }
        currentNode = g.getPathPoint(currentWP);

        if (Vector3.Distance(currentNode.transform.position, transform.position) < accuracy) currentWP++;

        if(currentWP < g.getPathLength())
        {
            goal = g.getPathPoint(currentWP).transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);
            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }
}
