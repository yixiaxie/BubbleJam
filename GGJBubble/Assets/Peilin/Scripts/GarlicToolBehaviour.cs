using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GarlicToolBehaviour : MonoBehaviour
{
    public Transform characterTransform;  // Reference to the character's transform
    public float speed = 5f;     // Speed at which the garlic moves towards the character
    public float disappearDistance = 0.5f;  // Distance at which the garlic disappears
    public Character character;
    private bool Moving = false;

    void Update()
    {
        if (Moving)
        {
            if (characterTransform == null) return;

            // Move the garlic towards the character
            transform.position = Vector3.MoveTowards(transform.position, characterTransform.position, speed * Time.deltaTime);

            // Check if the garlic is close enough to the character
            if (Vector3.Distance(transform.position, characterTransform.position) < disappearDistance)
            {
                character.ChangeCharacterSprite();
                Destroy(gameObject);  // Destroy the garlic
            }
        }
    }
    public void MoveToCharacter()
    {
        Moving = true;
    }
}
