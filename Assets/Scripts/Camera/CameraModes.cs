using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class CameraModes : MonoBehaviour
{
    [SerializeField]
    public Volume volume;
    public GameObject camcorderOverlay;
    public GameObject thermalCam;
    public bool nightVisionState;
    public CameraUI camUI;
    public GameObject questBar;
    public GameObject timeTxt;
    private bool volumeToneMapping;

    private void Start()
    {
        questBar.SetActive(true);
        camcorderOverlay.SetActive(false);
        thermalCam.SetActive(false);
        nightVisionState = false;
        SetNightVision(false);
        camUI.batteryCharge = camUI.fullBattery;

        volumeToneMapping = volume.profile.TryGet(out Tonemapping tonemapping);
        tonemapping.active = true;

    }

    // Update is called once per frame
    void Update()
    {  
        //pulls the camera up if right click is pressed
        if (Input.GetMouseButtonDown(1))
        {
            camcorderOverlay.SetActive(!camcorderOverlay.activeSelf);
      
            bool volumeGrain = volume.profile.TryGet(out FilmGrain grain);
            grain.active = !grain.active;

            bool toneMapping = volume.profile.TryGet(out Tonemapping tonemapping);
            tonemapping.active = !tonemapping.active;


            SetNightVision(false);
            thermalCam.SetActive(false);
            questBar.SetActive(!questBar.activeSelf);
            timeTxt.SetActive(!timeTxt.activeSelf);
        }

        //activates nightvision if the camera is up and E is pressed
        if (Input.GetKeyDown(KeyCode.E) && camcorderOverlay.activeSelf && camUI.canNightvision)
        {
            SetNightVision(!nightVisionState);
        }

        //activates thermal vision if the camera is up and F is pressed
        if (Input.GetKeyDown(KeyCode.F) && camcorderOverlay.activeSelf && camUI.canThermalVision)
        {
            SetNightVision(false, thermalCam.activeSelf);
            thermalCam.SetActive(!thermalCam.activeSelf);

            bool toneMapping = volume.profile.TryGet(out Tonemapping tonemapping);
            tonemapping.active = false;
        }

        ////for testing only, will be moved once we have a menu for it
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Application.Quit();
        //}

        //Reloads battery when R is pressed even if the camera is down
        if (Input.GetKeyDown(KeyCode.R))
        {
            camUI.ReloadBattery();
        }
    }

    //function to turn on everything for night vision mode
    public void SetNightVision(bool active = false, bool thermal = false)
    {
        thermalCam.SetActive((false||thermal));

        bool volumeColor = volume.profile.TryGet(out ColorAdjustments colorAdjustments);
        bool volumeBloom = volume.profile.TryGet(out Bloom bloom);
        bool volumeVignette = volume.profile.TryGet(out Vignette vignette);
        bool volumeToneMapping = volume.profile.TryGet(out Tonemapping tonemapping);

        colorAdjustments.active = active;
        bloom.active = active;
        vignette.active = active;
        tonemapping.active = !tonemapping.active;
        nightVisionState= active;

    }
}
