using UnityEngine;

public class StarUI : MonoBehaviour
{
    [SerializeField] private GameObject Star1; 
    [SerializeField] private GameObject Star2; 
    [SerializeField] private GameObject Star3;

    [SerializeField] private AudioClip starSfx;
    [SerializeField] private AudioClip winSound;
    public bool playWin; 

    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>(); 
        CheckStarCount(); 
        if (GameManager.Instance.sound && playWin)
        {
            audio.PlayOneShot(winSound);
        }
    }

    // Update is called once per frame
    void CheckStarCount()
    {
        var stars = LevelManager.Instance.CheckStarCritera();
        Debug.Log(stars);
        if (stars >= 1)
        {
            Invoke(nameof(ActivateStar1), 0.5f); 
        }
        if (stars >= 2)
        {
            Invoke(nameof(ActivateStar2), 1f); 
        }
        if (stars >= 3)
        {
            Invoke(nameof(ActivateStar3), 1.5f); 
        }
    }

    public void ActivateStar1()
    {
        if (GameManager.Instance.sound)
        {
            audio.PlayOneShot(starSfx);
        }
        Star1.SetActive(true);
    }
    public void ActivateStar2()
    {
        if (GameManager.Instance.sound)
        {
            audio.PlayOneShot(starSfx);
        }
        Star2.SetActive(true);
        
    }
    public void ActivateStar3()
    {
        if (GameManager.Instance.sound)
        {
            audio.PlayOneShot(starSfx);
        }
        Star3.SetActive(true);
    }
}
