using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    public GameObject instancePoint;
    public GameObject ball;
    public Material redBall, blueBall, greenBall;
    public GameObject TrajectoryPointPrefeb;

    public float timeTillNextBall;
    public float timer;

    public UiManager UIM;

    //used for showing the trajectory
    public bool showBallTrajectory;
    private int numOfTrajectoryPoints = 30;
    private List<GameObject> trajectoryPoints;

    void Start()
    {
        UIM = FindObjectOfType<UiManager>();
        trajectoryPoints = new List<GameObject>();

        for (int i = 0; i < numOfTrajectoryPoints; i++)
        {
            GameObject dot = (GameObject)Instantiate(TrajectoryPointPrefeb);
            dot.GetComponent<Renderer>().enabled = false;
            trajectoryPoints.Insert(i, dot);
        }

        //check to see if the files exist, if they dont create them
        if(!File.Exists(Application.dataPath + "/Resources/RedBall.json"))
        {
            Ball ballData = new Ball();
            ballData.color = Color.red;
            ballData.colorNumber = 0;
            ballData.ballVelocity = new Vector3(0, 9.5f, -9.5f);
            ballData.saveBall(Application.dataPath + "/Resources/RedBall.json");
        }

        if (!File.Exists(Application.dataPath + "/Resources/GreenBall.json"))
        {
            Ball ballData = new Ball();
            ballData.color = Color.green;
            ballData.colorNumber = 1;
            ballData.ballVelocity = new Vector3(0, 10, -10f);
            ballData.saveBall(Application.dataPath + "/Resources/GreenBall.json");
        }

        if (!File.Exists(Application.dataPath + "/Resources/BlueBall.json"))
        {
            Ball ballData = new Ball();
            ballData.color = Color.blue;
            ballData.colorNumber = 2;
            ballData.ballVelocity = new Vector3(0, 10.5f, -10.5f);
            ballData.saveBall(Application.dataPath + "/Resources/BlueBall.json");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        timer += Time.deltaTime;

        if(timeTillNextBall <= timer)
        {
            shootBall();
            timer = 0;
        }
    }

    public void toggle()
    {
        showBallTrajectory = !showBallTrajectory;
    }

    public void shootBall()
    {
        Ball ballData = new Ball();


        //determine which type of ball to load
        int tempValue = Random.Range(0,3);
        

        switch(tempValue)
        {
            case 0:
                ballData.loadBall(Application.dataPath + "/Resources/RedBall.json");
                break;

            case 1:
                ballData.loadBall(Application.dataPath + "/Resources/GreenBall.json");
                break;

            case 2:
                ballData.loadBall(Application.dataPath + "/Resources/BlueBall.json");
                break;
        }

        //apply the loaded ball's data to the ball prefab
        GameObject tempObject = Instantiate(ball, instancePoint.transform.position, instancePoint.transform.rotation);
        tempObject.GetComponent<MeshRenderer>().material.color = ballData.color;
        tempObject.GetComponent<Rigidbody>().velocity = ballData.ballVelocity;

        
        setTrajectoryPoints(instancePoint.transform.position, ballData.ballVelocity);

    }

    //used to calculate where the ball is going to land, in order to give the player a slight advantage
    void setTrajectoryPoints(Vector3 pStartPosition, Vector3 pVelocity)
    {
        float velocity = Mathf.Sqrt((pVelocity.z * pVelocity.z) + (pVelocity.y * pVelocity.y));
        float angle = Mathf.Rad2Deg * (Mathf.Atan2(pVelocity.y, pVelocity.z));
        float fTime = 0;

        fTime += 0.1f;
        for (int i = 0; i < numOfTrajectoryPoints; i++)
        {
            float dz = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
            float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime / 2.0f);
            Vector3 pos = new Vector3(2, pStartPosition.y + dy, pStartPosition.z + dz);
            trajectoryPoints[i].transform.position = pos;

            //controls weather the player had selected to see trajectories
            if (showBallTrajectory)
                trajectoryPoints[i].GetComponent<Renderer>().enabled = true;
            else
                trajectoryPoints[i].GetComponent<Renderer>().enabled = false;

            trajectoryPoints[i].transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(pVelocity.y - (Physics.gravity.magnitude) * fTime, pVelocity.z) * Mathf.Rad2Deg);
            fTime += 0.1f;
        }
    }
}
