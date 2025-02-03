using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraModes : MonoBehaviour
{
    [SerializeField]
    public Volume volume;
    public GameObject camcorderOverlay;

    private void Start()
    {
        camcorderOverlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) /*&& camcorderOverlay.activeSelf*/)
        {
            bool volumeColor = volume.profile.TryGet(out ColorAdjustments colorAdjustments);
            bool volumeGrain = volume.profile.TryGet(out FilmGrain grain);
            bool volumeBloom = volume.profile.TryGet(out Bloom bloom);
            bool volumeVignette = volume.profile.TryGet(out Vignette vignette);

            colorAdjustments.active = !colorAdjustments.active;
            grain.active = !grain.active;
            bloom.active = !bloom.active;
            vignette.active = !vignette.active;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            camcorderOverlay.SetActive(!camcorderOverlay.activeSelf);
        }
    }
}
