using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Nightvision : MonoBehaviour
{
    [SerializeField]
    public Volume volume;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(volume.profile.TryGet(out ColorAdjustments colorAdjustments))
            {
                colorAdjustments.active = !colorAdjustments.active;
            }
        }
    }
}
