using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    string Scene;

    void Start()
    {
        Scene=SceneManager.GetActiveScene().name;
    }

    private void OnCollisionEnter2D(Collision2D colision)
    {
        if (colision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(Scene);
        }
    }
        
}
