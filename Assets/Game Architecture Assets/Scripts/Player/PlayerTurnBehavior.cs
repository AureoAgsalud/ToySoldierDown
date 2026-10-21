using Unity.VisualScripting;
using UnityEngine;

public class PlayerTurnBehavior : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;

    [Header("Player Turn")]
    [SerializeField] private float _turnSpeed;
    void Update()
    {
        RotatePlayer();
    }

    void RotatePlayer()
    {
        transform.Rotate(Vector3.up * _turnSpeed * Time.deltaTime * _input.mouseX);
    }
}
