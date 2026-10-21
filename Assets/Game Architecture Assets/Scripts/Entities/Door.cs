using System;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] BlockDetection[] blockDetectors;
    [SerializeField] Renderer[] unlockLights;

    Animator anim;
    bool[] unlocks;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        foreach (BlockDetection blockDetector in blockDetectors)
        {
            blockDetector.door = this;
        }
        foreach (Renderer light in unlockLights)
        {
            light.material.SetColor("_EmissionColor", Color.red * 20f);
        }
        unlocks = new bool[blockDetectors.Length];
        if (blockDetectors.Length == 0)
        {
            Debug.LogWarning("Door has no pressure plates!!"); 
        }
    }
    public void OpenWithButton()
    {
        if(anim != null)
        {
            anim.Play("OpenClose", 0, Mathf.Clamp(anim.GetCurrentAnimatorStateInfo(0).normalizedTime, 0f, 1f));
            anim.SetFloat("Speed", 1f);
        }
    }

    public void Unlock(BlockDetection blockDetection)
    {
        unlocks[Array.IndexOf(blockDetectors, blockDetection)] = true;
        unlockLights[Array.IndexOf(blockDetectors, blockDetection)].material.SetColor("_EmissionColor", Color.green * 20f);
        DoorOpenClose();
    }
    public void Lock(BlockDetection blockDetection)
    {
        unlocks[Array.IndexOf(blockDetectors, blockDetection)] = false;
        unlockLights[Array.IndexOf(blockDetectors, blockDetection)].material.SetColor("_EmissionColor", Color.red * 20f);
        DoorOpenClose();
    }

    void DoorOpenClose()
    {
        bool openDoor = true;
        foreach (bool unlock in unlocks)
        {
            if (!unlock) { openDoor = false; }
        }

        if (openDoor)
        {
            anim.Play("OpenClose", 0, Mathf.Clamp(anim.GetCurrentAnimatorStateInfo(0).normalizedTime, 0f, 1f));
            anim.SetFloat("Speed", 1f);
        }
        else
        {
            anim.Play("OpenClose", 0, Mathf.Clamp(anim.GetCurrentAnimatorStateInfo(0).normalizedTime, 0f, 1f));
            anim.SetFloat("Speed", -1f);
        }
    }
}