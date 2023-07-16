using UnityEngine;

public class StarUI : MonoBehaviour
{
    [SerializeField] private GameObject Star1; 
    [SerializeField] private GameObject Star2; 
    [SerializeField] private GameObject Star3; 
    // Start is called before the first frame update
    void Start()
    {
        CheckStarCount(); 
    }

    // Update is called once per frame
    void CheckStarCount()
    {
        LevelManager.Instance.CheckStarCritera();
        if (LevelManager.Instance.starsCollected >= 1)
        {
            Star1.SetActive(true);
        }
        if (LevelManager.Instance.starsCollected >= 2)
        {
            Star2.SetActive(true);
        }
        if (LevelManager.Instance.starsCollected >= 3)
        {
            Star3.SetActive(true);
        }
    }
}
