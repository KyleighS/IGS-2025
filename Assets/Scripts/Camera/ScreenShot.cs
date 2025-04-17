using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class ScreenShot : MonoBehaviour
{
    public GameManager gameManager;
    public CreatureScript creatureScript;
    public CameraUI cameraUI;
    public GameObject photoAlbum;
    public EvidenceInView evidenceInView;
    public GameObject cursor;

    [Header("Photo")]
    public Image photoDisplayArea;
    public GameObject photoFrame;
    public GameObject camOverlay;
    public GameObject interactUI;
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
        cursor.SetActive(true);
    }

    private void Update()
    {
        //checking if the camera ui is active and the player hits the right button
        if(Input.GetMouseButtonDown(0) && camOverlay.activeSelf && cameraUI.canTakePic)
        {
            //makes sure a previous photo isnt still up
            if(!viewingPhoto)
            {
                //calls the TakePhoto function
                StartCoroutine(TakePicture());
                if (evidenceInView.evidenceOnScreen || creatureScript.creatureInView)
                {
                    gameManager.evidenceSlider.value++;
                }
            }
        }
        //pulls up the photo album if Tab is hit also disables the camera UI
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            photoAlbum.SetActive(!photoAlbum.activeSelf);
            camOverlay.SetActive(false);
        }
    }

    IEnumerator TakePicture()
    {
        camOverlay.SetActive(false);
        photoAlbum.SetActive(false);
        interactUI.SetActive(false); 
        cursor.SetActive(false);
        StartCoroutine(FlashEffect());
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
        Sprite photoSprite = Sprite.Create(screenCapture, 
            new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), 
            new Vector2(0.5f, 0.5f), 100.0f);

        photoDisplayArea.sprite = photoSprite;
        gameManager.pictures.Add(photoSprite);

        photoFrame.SetActive(true);
        fadingAnimation.Play("PhotoFadeIn");
        camOverlay.SetActive(true);
        cursor.SetActive(true);

        StartCoroutine(FadePictureOUt());

        for (int i = 0; i < gameManager.picSlots.Count; i++)
        {
            Sprite screenshot_sprite = gameManager.pictures[i];
            gameManager.picSlots[i].transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = screenshot_sprite;
        }
    }

    IEnumerator FadePictureOUt()
    {
        //waits for 2 second before starting the animation to move the piture off the screen 
        yield return new WaitForSeconds(2);
        MovingPhoto.Play("MoveandShrink");
        StartCoroutine(RemovePhoto());
    }

    IEnumerator RemovePhoto()
    {
        //waits for the previous animation to finish then deactiviates the photo UI
        yield return new WaitForSeconds(6);
        viewingPhoto = false;
        photoFrame.SetActive(false);

        //sets the photo UI back to its original position and scale
        photoFrame.transform.localPosition = ogPos;
        photoFrame.transform.localScale = ogScale;
    }

    IEnumerator FlashEffect()
    {
        camFlash.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        camFlash.SetActive(false);
    }
}
