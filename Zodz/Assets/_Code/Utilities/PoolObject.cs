using UnityEngine;
using UnityEngine.Events;

public class PoolObject : MonoBehaviour
{
    public UnityEvent OnSpawn; //usado pelo container, não é interface para faciltiar vários scripts usando o evento
}
