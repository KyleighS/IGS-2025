using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraModes : MonoBehaviour
{
    [SerializeField]
    public Volume volume;
    public GameObject camcorderOverlay;
    public GameObject thermalCam;
    public bool nightVisionState;

    private void Start()
    {
        camcorderOverlay.SetActive(false);
        thermalCam.SetActive(false);
        nightVisionState = false;
        SetNightVision(false);
    }

    // Update is called once per frame
    void Update()
    {  
        if (Input.GetKeyDown(KeyCode.E) && camcorderOverlay.activeSelf)
        {
            SetNightVision(!nightVisionState);
        }

        if (Input.GetMouseButtonDown(1))
        {
            camcorderOverlay.SetActive(!camcorderOverlay.activeSelf);
            SetNightVision(false);
            thermalCam.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.F) && camcorderOverlay.activeSelf)
        {
            SetNightVision(false, thermalCam.activeSelf);
            thermalCam.SetActive(!thermalCam.activeSelf);
        }
    }

    public void SetNightVision(bool active = false, bool thermal = false)
    {
        thermalCam.SetActive((false||thermal));

        bool volumeColor = volume.profile.TryGet(out ColorAdjustments colorAdjustments);
        bool volumeGrain = volume.profile.TryGet(out FilmGrain grain);
        bool volumeBloom = volume.profile.TryGet(out Bloom bloom);
        bool volumeVignette = volume.profile.TryGet(out Vignette vignette);

        colorAdjustments.active = active;
        grain.active = active;
        bloom.active = active;
        vignette.active = active;
        nightVisionState= active;
    }
}
