using UnityEngine;
using UnityEngine.Events;

public class StartButtonScript : MonoBehaviour
{
    [SerializeField] UnityEvent OnNewGamePressed; 
    [SerializeField] UnityEvent OnLoadGamePressed;
    [SerializeField] GameObject popupMenu; 

    public void CheckProgress()
    {
        //Check if there is already progress
        if(GameManager.Instance.ReadProgress() > 0)
        {
            popupMenu.SetActive(true);
        }
        else
        {
            NewGame(); 
        }
    }

    public void NewGame()
    {
        OnNewGamePressed?.Invoke();
    }
    public void LoadGame()
    {
        OnLoadGamePressed?.Invoke(); 
    }
}
