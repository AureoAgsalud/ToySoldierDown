using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] public UnityEvent OnLevelStart;
    [SerializeField] public UnityEvent OnLevelEnd;

    private bool isFinalLevel = false;

    public void LevelStart()
    {
        OnLevelStart?.Invoke();

        //GameManager.instance.ChangeState(GameManager.GameState.LevelStart);
    }

    public void LevelEnd()
    {
        OnLevelEnd?.Invoke();

        //if (isFinalLevel)
        //{
        //    GameManager.instance.ChangeState(GameManager.GameState.GameEnd);
        //}

        //else
        //{
        //    GameManager.instance.ChangeState(GameManager.GameState.LevelEnd);
        //}
    }

}
