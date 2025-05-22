using System.Collections.Generic;
using UnityEngine;

public class HurtboxController : MonoBehaviour
{
    public RangeCheckerController rangeCheckerController;
    public List<int> GameObjectsCollided {get; private set;} = new List<int>();
    
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // Gets root parent in case hitbox and range checker are on different objects
        // but share same root parent
        GameObject rootParent = otherCollider.transform.root.gameObject;
        int rootParentID = rootParent.GetInstanceID();
        GameObjectsCollided.Add(rootParentID);

        // Logic that checks if collider has hit hurtbox AND 
        // range checkers have collider to decide it should hit
        if(GameObjectsCollided.Contains(rootParentID) && rangeCheckerController.GameObjectsInRange.Contains(rootParentID))
            Debug.Log("BULLET HAS HIT!");
    }

    void OnTriggerExit2D(Collider2D otherCollider)
    {
        GameObject rootParent = otherCollider.transform.root.gameObject;
        int rootParentID = rootParent.GetInstanceID();
        GameObjectsCollided.Remove(rootParentID);
    }
}
