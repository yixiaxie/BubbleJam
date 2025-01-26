using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarlicToolBehaviour : MonoBehaviour
{
    public Transform characterTransform;  // Reference to the character's transform
    public float speed = 5f;     // Speed at which the garlic moves towards the character
    public float disappearDistance = 0.5f;  // Distance at which the garlic disappears
    public PKBarController controller;
    public float ratio;
    public Character character;
    private Coroutine garlicBuffCoroutine;

    public bool Moving = false;

    void Update()
    {

        if (Moving)
        {
            //if (characterTransform == null) return;

            // Move the garlic towards the character
            transform.position = Vector3.MoveTowards(transform.position, characterTransform.position, speed * Time.deltaTime);

            // Check if the garlic is close enough to the character
            if (Vector3.Distance(transform.position, characterTransform.position) < disappearDistance)
            {
                if (character.playerID == 1)
                {
                    GameObject hurtCharacter = GameObject.FindWithTag("Character2");
                    Character character = hurtCharacter.GetComponent<Character>();
                    character.isGarliced = true;
                    Destroy(gameObject);
                }
                if (character.playerID == 2)
                {
                    GameObject hurtCharacter = GameObject.FindWithTag("Character");
                    Character character = hurtCharacter.GetComponent<Character>();
                    character.isGarliced = true;
                    Destroy(gameObject);
                }
                //  character.ChangeCharacterSprite();
                
            }
        }
        else
        {
            if (controller.player1Ratio <= ratio && character.playerID==1)
            {
                MoveToCharacter();
            }
            if (controller.player2Ratio <= ratio && character.playerID == 2)
            {
                MoveToCharacter();
            }
        }
    }
    public void MoveToCharacter()
    {
        Moving = true;
    }

    
}
