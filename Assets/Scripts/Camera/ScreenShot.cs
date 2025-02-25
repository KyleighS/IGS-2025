using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class ScreenShot : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject photoAlbum;

    [Header("Photo")]
    public Image photoDisplayArea;
    public GameObject photoFrame;
    public GameObject camUI;
    public Vector2 ogPos;
    public Vector3 ogScale;

    [Header("Camera Flash")]
    public GameObject camFlash;
    public float flashTime;

    [Header("Fade Effect")]
    public Animator fadingAnimation;
    public Animator MovingPhoto;

    private Texture2D screenCapture;
    private bool viewingPhoto;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ogPos = this.transform.localPosition;
        ogScale = this.transform.localScale;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && camUI.activeSelf)
        {
            if(!viewingPhoto)
            {
                StartCoroutine(TakePicture());
            }
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            photoAlbum.SetActive(true);
            camUI.SetActive(false);

        }
    }

    IEnumerator TakePicture()
    {
        camUI.SetActive(false);
        photoAlbum.SetActive(false);
        StartCoroutine(FlashEffect());
        Debug.Log("Photo was takken");
        viewingPhoto = true;

        yield return new WaitForEndOfFrame();

        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);

        Texture2D newCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        newCapture.ReadPixels(regionToRead, 0, 0, false);
        newCapture.Apply();
        screenCapture = newCapture;
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
        gameManager.pictures.Add(photoSprite);

        photoFrame.SetActive(true);
        fadingAnimation.Play("PhotoFadeIn");
        camUI.SetActive(true);

        StartCoroutine(FadePictureOUt());

        for (int i = 0; i < gameManager.picSlots.Count; i++)
        {
            //    int col = i % 4;
            //    int row = (int)(i / 4);

            Sprite screenshot_sprite = gameManager.pictures[i];
            gameManager.picSlots[i].transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = screenshot_sprite;
        }
    }
    IEnumerator FlashEffect()
    {
        camFlash.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        camFlash.SetActive(false);
        StartCoroutine(RemovePhoto());
    }
    IEnumerator FadePictureOUt()
    {
        //Debug.Log("Photo is shrinking");
        yield return new WaitForSeconds(2);
        MovingPhoto.Play("MoveandShrink");
    }

    IEnumerator RemovePhoto()
    {
       //Debug.Log("Photo is removed");
        yield return new WaitForSeconds(6);
        viewingPhoto = false;
        photoFrame.SetActive(false);

        photoFrame.transform.localPosition = ogPos;
        photoFrame.transform.localScale = ogScale;
        //Debug.Log("Photo is reset");
    }
}
