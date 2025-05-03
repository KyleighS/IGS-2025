using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CreatureAwareness : MonoBehaviour, IAwareness
{
    public List<Collider> collidersInAwareness
    {
        get;
        set;
    }

    public UnityAction<Collider> OnColliderEntersAwareness;
    public UnityAction<Collider> OnColliderExitsAwareness;

    public void Awake()
    {
        collidersInAwareness = new List<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.ToString());
        AddToAwarenessList(other);
    }

    private void OnTriggerExit(Collider other)
    {
        RemoveFromAwarenessList(other);
    }

    public void AddToAwarenessList(Collider collider)
    {
        //Debug.Log(collider.name + " entered the list");
        collidersInAwareness.Add(collider);
        //After the item has been added to the list, we tell all subscribers of this event that it has entered awareness of this object
        //We add the ? to indicate that we will only invoke this event if it's not null
        OnColliderEntersAwareness?.Invoke(collider);
    }

    public void RemoveFromAwarenessList(Collider collider)
    {
        //Debug.Log(collider.name + " left the list");
        collidersInAwareness.Remove(collider);
        //After the item has been removed from the list, we tell all subscribers of this event that it has exited awareness of this object
        //We add the ? to indicate that we will only invoke this event
        OnColliderExitsAwareness?.Invoke(collider);
    }

    /// <summary>
    /// Returns true or false depending on whether there is a collider in range with the tag "tag". The resulting collider is returned as "col"
    /// </summary>
    /// <param name="tag">The tag we're searching for.</param>
    /// <param name="col">The first collider in awareness range to have that tag.</param>
    /// <returns></returns>

    public bool IsTagInRange(string tag, out Collider col)
    {
        col = collidersInAwareness.Find(x => x.CompareTag(tag));
        return col != null;
    }
}
