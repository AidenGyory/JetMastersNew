using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaunchPadWin : MonoBehaviour
{
    [SerializeField] UnityEvent winEvent;
    [SerializeField] int levelNumber; //game needs to know what level you're on

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Add code for finishing level
            Debug.Log("landed!! ");
            winEvent?.Invoke();
        }
    }

    public void WinEndLevel()
    {
        //dont increase progress if youve already finished the level
        if (GameManager.Instance != null && GameManager.Instance.ReadProgress() <= levelNumber - 1)
            GameManager.Instance.SetProgress(GameManager.Instance.ReadProgress()+1);


        //go back to level select
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
    }
}
