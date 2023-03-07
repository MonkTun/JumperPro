using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

//This is a script of the goal.
public class Goal : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    private bool isDone;
    
    //When player reaches,
    private void OnTriggerEnter(Collider other)
    {
        if (isDone) return;
        
        //check if it is player
        if (other.TryGetComponent(out PlayerController pl))
        {
            isDone = true;
            print("done");
            Time.timeScale = 0.5f; //add slow effect
            canvas.SetActive(true); //open finished message

            StartCoroutine(LoadSceneAfterFive()); //coroutine for restart
        }
    }

    IEnumerator LoadSceneAfterFive()
    {
        yield return new WaitForSeconds(1.5f); //it's 3 seconds because timescale = 0.5f
        Time.timeScale = 1f; //reset slow effect
        SceneManager.LoadScene(0); //load the game again
    }
    
}
