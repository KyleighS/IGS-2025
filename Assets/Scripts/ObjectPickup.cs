using UnityEngine;

public class ObjectPickup : MonoBehaviour, IInteractable
{
    public GameManager manager;
    public string GetDescription()
    {
        return "Left Click to Pick up";
    }

    public void Interact()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.gameObject.tag == "Pickable")
            {
                manager.inventory.Add(hitInfo.collider.gameObject.name);

                Destroy(hitInfo.collider.gameObject);
            }
        }
    }
}

