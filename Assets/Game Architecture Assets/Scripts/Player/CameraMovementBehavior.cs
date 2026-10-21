using System.ComponentModel.Design.Serialization;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovementBehavior : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [Header("Player Turn")]
    [SerializeField] private float _turnSpeed;
    [SerializeField] private bool _invertedMouse;

    private float _camXRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
    void Update()
    {
        RotateCamera();
    }
    void RotateCamera()
    {
        _camXRotation += Time.deltaTime * _input.mouseY * _turnSpeed * (_invertedMouse ? 1 : -1);
        _camXRotation = Mathf.Clamp(_camXRotation, -85f, 85f);

        transform.localRotation = Quaternion.Euler(_camXRotation, 0, 0);
    }
}
