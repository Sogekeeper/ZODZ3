using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventAlternatives
{
    /*
        Unity Events com parametros
    */
    [System.Serializable]
    public class IntEvent : UnityEvent<int>
    {
    }

    [System.Serializable]
    public class StringEvent : UnityEvent<string>
    {
    }
    [System.Serializable]
    public class SkillEvent : UnityEvent<Skill>
    {
    }
    [System.Serializable]
    public class EntityEvent : UnityEvent<EntityStats>
    {
    }
}
