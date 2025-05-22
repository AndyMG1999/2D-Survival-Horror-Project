using System.Collections.Generic;
using UnityEngine;

public class RangeCheckerController : MonoBehaviour
{
    List<string> gameObjectsInRange;
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // Gets root parent in case hitbox and range checker are on different objects
        // but share same root parent
        GameObject rootParent = otherCollider.transform.root.gameObject;
        Debug.Log("Game Object in Range!: "+rootParent.name);
    }
    void OnTriggerExit2D(Collider2D otherCollider)
    {
        GameObject rootParent = otherCollider.transform.root.gameObject;
        Debug.Log("Game Object No Longer in Range!: "+rootParent.name);
    }
}

//Todo: Create a similar situation for the hurtbox which checks the list of
// gameObjectsInRange, if the hitbox and range check both have the same rootParent
// then the projectile can do damage