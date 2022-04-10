
using UnityEngine;

[CreateAssetMenu(fileName = "AIStats", menuName = "ScriptableObject/Stats/AI")]
public class AIStats : ScriptableObject
{
    [SerializeField] private float petAttraction;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float restTime;

    public float PetAttraction => petAttraction;
    public float MovementSpeed => movementSpeed;
    public float RestTime => restTime;

}
