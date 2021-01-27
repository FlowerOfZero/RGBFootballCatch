using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionScript : MonoBehaviour
{
    public UiManager UIM;

    public void Start()
    {
        UIM = FindObjectOfType<UiManager>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            Debug.Log("Ball caught!");
            UIM.updateScore();
            Destroy(gameObject);
        }

        if (col.CompareTag("KillBox"))
        {
            Debug.Log("Ball Missed!");
            UIM.ballDropped();
            Destroy(gameObject);
        }
    }
}
