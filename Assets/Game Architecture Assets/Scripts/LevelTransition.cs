using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] string currentLevelName;
    public LevelManager manager;
    public PlayableDirector director;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            director.Play();

            GameManager.instance.currentLevel = manager;

            Debug.Log("Player has entered the level transition trigger.");
            //GameManager.instance.ChangeState(GameManager.GameState.LevelStart);

            // - Any other behavior we need for this level transition
            Debug.Log("Loading Level: " + currentLevelName);
            gameObject.SetActive(false);
        }
    }
}
