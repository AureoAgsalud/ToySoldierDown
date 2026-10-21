using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightEffect : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float duration = 0.1f; // How long the light stays visible

    private Light targetLight;
    private float timer;

    private void Awake()
    {
        // Automatically grab the attached Light component
        targetLight = GetComponent<Light>();

        // Ensure the light starts turned off
        targetLight.enabled = false;
    }

    private void Update()
    {
        // 1. Listen to your PlayerInput singleton for the left click/primary shoot action
        if (PlayerInput.Instance != null && PlayerInput.Instance.primaryShootPressed)
        {
            ActivateLight();
        }

        // 2. Handle turning the light off automatically after the duration passes
        if (targetLight.enabled)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                targetLight.enabled = false;
            }
        }
    }

    private void ActivateLight()
    {
        targetLight.enabled = true;
        timer = duration; // Reset the timer
    }
}