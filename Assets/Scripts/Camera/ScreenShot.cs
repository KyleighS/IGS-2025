using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class ScreenShot : MonoBehaviour
{
    [Header("Photo")]
    public Image photoDisplayArea;
    public GameObject photoFrame;
    public GameObject camUI;

    [Header("Camera Flash")]
    public GameObject camFlash;
    public float flashTime;

    [Header("Fade Effect")]
    public Animator fadingAnimation;

    private Texture2D screenCapture;
    private bool viewingPhoto;
    public List<Sprite> pictures;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && camUI.activeSelf)
        {
            if(!viewingPhoto)
            {
                StartCoroutine(TakePicture());
            }
            else
            {
                RemovePhoto();
            }
        }
    }

    IEnumerator TakePicture()
    {
        camUI.SetActive(false);
        StartCoroutine(FlashEffect());
        //Debug.Log("Photo was takken");
        viewingPhoto = true;

        yield return new WaitForEndOfFrame();

        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);

        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();
        DisplayPhoto();
    }

    public void DisplayPhoto()
    {
        //Debug.Log("Photo is displayed");
        Sprite photoSprite = Sprite.Create(screenCapture, 
            new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), 
            new Vector2(0.5f, 0.5f), 100.0f);

        //Debug.Log("Photo was turned into sprite");

        photoDisplayArea.sprite = photoSprite;
        pictures.Add(photoSprite);

        photoFrame.SetActive(true);
        fadingAnimation.Play("PhotoFade");
        camUI.SetActive(true);
    }
    IEnumerator FlashEffect()
    {
        camFlash.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        camFlash.SetActive(false);
    }

    private void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);
        //camUI.SetActive(true);
    }
}
