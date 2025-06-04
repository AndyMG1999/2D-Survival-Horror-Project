using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtboxController : MonoBehaviour
{
    public SpriteRenderer spriteToFlash; // Reference to the SpriteRenderer
    public float flashDuration = 2;    // Duration of the flash
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
        {
            if(spriteToFlash)StartCoroutine(FlashWhite());
        }
    }

    void OnTriggerExit2D(Collider2D otherCollider)
    {
        GameObject rootParent = otherCollider.transform.root.gameObject;
        int rootParentID = rootParent.GetInstanceID();
        GameObjectsCollided.Remove(rootParentID);
    }

    IEnumerator FlashWhite()
    {
        // Save the original color
        Color originalColor = spriteToFlash.color;
        Color flashColor = Color.red;
        flashColor.a = 0.7f;
        // Change the color to white
        spriteToFlash.color = flashColor;

        // Wait for the flash duration
        yield return new WaitForSeconds(flashDuration);

        // Revert to the original color
        spriteToFlash.color = originalColor;
    }
}
