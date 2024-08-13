using System;
using UnityEngine;

public class TriggerListener : MonoBehaviour
{
    public event Action<PlayerTriggered> OnTriggerEntered;

    private void OnTriggerEnter(Collider other)
    {
        var trigger = other.GetComponent<PlayerTriggered>();
        if (trigger)
        {
            trigger.ActivateTrigger();
            OnTriggerEntered?.Invoke(trigger);
        }
    }
}