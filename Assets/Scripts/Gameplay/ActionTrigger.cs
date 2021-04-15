using UnityEngine;
using UnityEngine.Events;

public class ActionTrigger : MonoBehaviour
{
    public LayerMask layerMask;

    public UnityEvent<Character> actionFeedback;

    private void OnTriggerEnter(Collider other)
    {
        if (1 << other.gameObject.layer == layerMask)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Mob"))
            {
                Character character = other.GetComponent<Character>();
                if (character != null && actionFeedback != null)
                    actionFeedback.Invoke(character);
            }
        }
    }
}
