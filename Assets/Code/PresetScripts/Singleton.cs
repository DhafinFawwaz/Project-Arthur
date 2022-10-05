using UnityEngine;
using UnityEngine.Audio;

public class Singleton : MonoBehaviour
{
    public TransitionManager Transition;
    public AudioManager Audio;
    public SceneLoader Scene;
    public ResolutionManager Resolution;
    public SaveManager Save;
    public GameManager Game;
    public static Singleton Instance;
    
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
