using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField] private Material _green;
    [SerializeField] private Material _blinkingRed;

    private readonly int _lampColorMaterialIndex = 1;

    public enum State {
        Awaiting,
        Releasing
    };

    public void SetState(State state) 
    {
        switch (state)
        {
            case State.Awaiting:
                SetGreenState();
                break;

            case State.Releasing:
                SetBlinkingRedState();
                break;
        }
    }    

    private void SetBlinkingRedState()
    {
        SetMaterial(_blinkingRed, _lampColorMaterialIndex);
    }

    private void SetGreenState() 
    {
        SetMaterial(_green, _lampColorMaterialIndex);
    }

    private void SetMaterial(Material material, int index = 0)
    {
        var renderer = GetComponent<MeshRenderer>();
        var materials = renderer.sharedMaterials;
        materials[index] = material;
        renderer.sharedMaterials = materials;
    }
}
