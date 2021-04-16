using UnityEngine;
using UnityEngine.Events;

public class ActionTrigger : MonoBehaviour
{
    public LayerMask layerMask;

    public CharacterType characterType;

    public UnityEvent<Character> actionFeedback;

    private void OnTriggerEnter(Collider other)
    {
        if (1 << other.gameObject.layer == layerMask)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Mob"))
            {
                //Character character = other.GetComponent<Character>();
                //if (character != null && actionFeedback != null)
                //    actionFeedback.Invoke(character);

                ApplyLogic(other);
            }
        }

        if (characterType == CharacterType.ENEMY)
        {
            if (other.CompareTag("Player"))
            {
                ApplyLogic(other);
            }
        }
    }

    private void ApplyLogic(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if (character != null && actionFeedback != null)
            actionFeedback.Invoke(character);
    }
}
