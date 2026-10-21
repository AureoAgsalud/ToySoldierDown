using Unity.VisualScripting;
using UnityEngine;

public abstract class Interactor : MonoBehaviour
{
    // - Deleted because we have singleton now
    //[SerializeField] protected PlayerInput _input;
    

    void Update()
    {
        Interact();
    }

    public abstract void Interact();
}
