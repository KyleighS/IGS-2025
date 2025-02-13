using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class GameManager : MonoBehaviour, IInteractable
{
    public List<string> inventory;

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
                inventory.Add(hitInfo.collider.gameObject.name);

                Destroy(hitInfo.collider.gameObject);
            }
        }
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hitInfo;

        //    if(Physics.Raycast(ray, out hitInfo))
        //    {
        //        if(hitInfo.collider.gameObject.tag == "Pickable")
        //        {
        //            inventory.Add(hitInfo.collider.gameObject.name);

        //            Destroy(hitInfo.collider.gameObject);
        //        }
        //    }
        //}
    }
}

