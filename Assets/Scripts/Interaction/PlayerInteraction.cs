using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{

    public Camera mainCam;
    public CameraUI camUI;
    public float interactionDistance;

    public GameObject interactionUI;
    public TextMeshProUGUI interactionText;
    public GameObject cursor;


    private void Update()
    {
        if (!camUI.camOverlay.activeSelf)
        {
            InteractionRay();
        }
    }

    void InteractionRay()
    {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;

        bool hitSomething = false;
        interactionUI.SetActive(false);
        cursor.SetActive(true);

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                hitSomething = true;
                //Debug.Log("Description was called");
                interactionText.text = interactable.GetDescription();
                interactionUI.SetActive(hitSomething);
                cursor.SetActive(false);

                if (Input.GetKeyDown(KeyCode.E) && !camUI.camOverlay.activeSelf)
                {
                    Debug.Log("Interaction was called");
                    interactable.Interact();
                }
            }
        }
    }
}
