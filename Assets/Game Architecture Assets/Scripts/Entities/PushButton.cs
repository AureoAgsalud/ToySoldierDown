using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PushButton : MonoBehaviour, ISelectable
{
    [SerializeField] private Material _default;
    [SerializeField] private Material _hoverColor;
    [SerializeField] private MeshRenderer _renderer;

    public UnityEvent _onPush;
    public void OnHoverEnter()
    {
        _renderer.material = _hoverColor;
    }
    public void OnSelect()
    {
        _onPush?.Invoke();
    }
    public void OnHoverExit() 
    {
        _renderer.material = _default;
    }
}
