using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;

public class ObjectPickup : MonoBehaviour, IInteractable
{
    public GameManager manager;
    public GameObject winOverlay;
    public Slider evidenceSlider;


    public string GetDescription()
    {
        //Debug.Log("Object pick up");
        return "To Pick up";

    }

    public void Interact()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.gameObject.tag == "Battery")
            {
                manager.inventory.Add(hitInfo.collider.gameObject);
                Debug.Log("object was added");

                hitInfo.collider.gameObject.SetActive(false);
                //Destroy(hitInfo.collider.gameObject);

            }

            if (hitInfo.collider.gameObject.tag == "Evidence")
            {
                manager.inventory.Add(hitInfo.collider.gameObject);
                manager.UpdateEvidence();   
                Debug.Log("object was added");

                hitInfo.collider.gameObject.SetActive(false);
                //Destroy(hitInfo.collider.gameObject);

            }
        }
    }
}

