using System.Collections.Generic;
using UnityEngine;

public class RangeCheckerController : MonoBehaviour
{
    public List<int> GameObjectsInRange {get; private set;} = new List<int>();
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // Gets root parent in case hitbox and range checker are on different objects
        // but share same root parent
        GameObject rootParent = otherCollider.transform.root.gameObject;
        int rootParentID = rootParent.GetInstanceID();
        GameObjectsInRange.Add(rootParentID);
    }

    void OnTriggerExit2D(Collider2D otherCollider)
    {
        GameObject rootParent = otherCollider.transform.root.gameObject;
        int rootParentID = rootParent.GetInstanceID();
        GameObjectsInRange.Remove(rootParentID);
    }
}

//Todo: Create a similar situation for the hurtbox which checks the list of
// gameObjectsInRange, if the hitbox and range check both have the same rootParent
// then the projectile can do damage