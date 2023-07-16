using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    [Range(0,3)]
    public int[] levelStars;

    public void UpdateStars()
    {
        levelStars[0] = GameManager.Instance.level1Stars; 
        levelStars[1] = GameManager.Instance.level2Stars; 
        levelStars[2] = GameManager.Instance.level3Stars; 
        levelStars[3] = GameManager.Instance.level4Stars; 
        levelStars[4] = GameManager.Instance.level5Stars; 
        levelStars[5] = GameManager.Instance.level6Stars; 
        levelStars[6] = GameManager.Instance.level7Stars; 
        levelStars[7] = GameManager.Instance.level8Stars; 
    }
}
