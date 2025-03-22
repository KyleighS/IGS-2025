using System.Collections.Generic;
using UnityEngine;

public interface IAwareness
{
    public List<Collider> collidersInAwareness
    {
        get;
        set;
    }
}
