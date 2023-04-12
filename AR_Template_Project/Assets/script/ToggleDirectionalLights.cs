using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleDirectionalLights : MonoBehaviour
{
    public Light[] directionalLights;

    public void ToggleLights()
    {
        foreach (Light light in directionalLights)
        {
            light.enabled = !light.enabled;
        }
    }
}
