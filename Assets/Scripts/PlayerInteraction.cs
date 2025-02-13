using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{

    public Camera mainCam;
    public CameraUI mainUI;
    public float interactionDistance = 2f;

    public GameObject interactionUI;
    public TextMeshProUGUI interactionText;


    private void Update()
    {
        if (!mainUI.camOverlay.activeSelf)
        {
            InteractionRay();
        }
    }

    void InteractionRay()
    {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;

        bool hitSomething = false;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                hitSomething = true;
                interactionText.text = interactable.GetDescription();

                if (Input.GetMouseButtonDown(0))
                {
                    interactable.Interact();
                }
            }
        }

        interactionUI.SetActive(hitSomething);
    }
}
