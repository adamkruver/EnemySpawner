using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class CloneBox : MonoBehaviour
{
    [SerializeField] private UnityEvent<bool> IsInside;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.TryGetComponent<IDoorOpenable>(out IDoorOpenable unit))
            IsInside?.Invoke(true);
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.TryGetComponent<IDoorOpenable>(out IDoorOpenable unit))
            IsInside?.Invoke(false);
    }
}
