using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Score
{
    public int HighScore;
    public string json;
    public void saveScore(int scoreToSave)
    {
        Score score = new Score();
        score.HighScore = scoreToSave;
        string path = null;
        string json = JsonUtility.ToJson(score);


       
        path = Application.dataPath + "/Resources/ScoreData.json";
       

        
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

        
        //json = JsonUtility.ToJson(score);

        Debug.Log("Saving as JSON:" + json);
    }

    public void loadScore()
    {
        if(File.Exists(Application.dataPath + "/Resources/ScoreData.json"))
        {
            StreamReader sr = new StreamReader(Application.dataPath + "/Resources/ScoreData.json");
            string file = sr.ReadToEnd();
            Score score = JsonUtility.FromJson<Score>(file);
            HighScore = score.HighScore;
            Debug.Log("Highscore set to " + HighScore);
        }

        
    }

}
