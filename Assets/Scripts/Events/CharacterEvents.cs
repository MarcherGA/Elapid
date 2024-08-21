using UnityEngine;
using UnityEngine.Events;

public class CharacterEvents
{

    public static UnityAction<GameObject, int> characterTookDamage;
    public static UnityAction<GameObject, int> characterHealed;

}