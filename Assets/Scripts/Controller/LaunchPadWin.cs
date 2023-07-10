using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaunchPadWin : MonoBehaviour
{
    [SerializeField] UnityEvent winEvent;
    [SerializeField] int levelNumber; //game needs to know what level you're on
    [SerializeField] LevelManager levelManager;
    [SerializeField] float losingVelocity = 20f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Add code for finishing level
            Debug.Log("landed!! ");

            winEvent?.Invoke();

            //check if the player is going too fast


            /*
            if (other.GetComponentInParent<MainForwardThruster>().velocityRead > losingVelocity)
                Debug.Log("You lose!");
            else
                winEvent?.Invoke();
                */

        }
    }

    public void WinEndLevel()
    {
        //dont increase progress if youve already finished the level
        if (GameManager.Instance != null && GameManager.Instance.ReadProgress() <= levelNumber - 1)
        {
            GameManager.Instance.SetProgress(GameManager.Instance.ReadProgress() + 1);
            GameManager.Instance.Save();
        }


        //go back to level select
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
    }
}
