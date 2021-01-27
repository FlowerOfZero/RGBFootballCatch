using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("KillBox"))
        {
            Debug.Log("Collided with killbox");
            gameObject.GetComponent<Renderer>().enabled = true;
            
        }
    }
}
