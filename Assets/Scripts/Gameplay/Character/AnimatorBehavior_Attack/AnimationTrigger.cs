using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    public void TriggerAttack()
    {
        CharacterAttack.instance.TriggerAttack();
    }

    public void StopTrigger()
    {
        CharacterAttack.instance.StopTriggerAttack();
    }
}
