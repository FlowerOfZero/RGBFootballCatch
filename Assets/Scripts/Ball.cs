using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Ball
{
    public Color color;
    public int colorNumber;
    public Vector3 ballVelocity;

    public Ball()//default constructor
    {
        color = Color.red;
        colorNumber = 0;
    }

    public void saveBall(string path)
    {
        
        string json = JsonUtility.ToJson(this);

        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(json);
            }
        }
    #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
    #endif

        Debug.Log("Saving as JSON:" + json);
    }

    public void loadBall(string path)
    {
        if (File.Exists(path))
        {
            StreamReader sr = new StreamReader(path);
            string file = sr.ReadToEnd();
            Ball ball = JsonUtility.FromJson<Ball>(file);
            this.color = ball.color;
            this.colorNumber = ball.colorNumber;
            this.ballVelocity = ball.ballVelocity;
        }
        
    }
}
