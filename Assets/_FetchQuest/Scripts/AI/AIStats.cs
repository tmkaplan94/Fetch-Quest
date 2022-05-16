
using UnityEngine;

[CreateAssetMenu(fileName = "AIStats", menuName = "ScriptableObject/Stats/AI")]
public class AIStats : ScriptableObject
{
    [SerializeField] private float petAttraction;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float restTime;
    [SerializeField] private float talkTime;
    [SerializeField] private float pettingDistance; //Petting Range
    [SerializeField] private float pettingCooldown; //Petting Cooldown
    [SerializeField] private float talkingCooldown; //Talking Cooldown
    [SerializeField] private float workingCooldown; //Walking Cooldown
    [SerializeField] private bool isJanitor; //Checking to see if AI is the Janitor
    [SerializeField] private bool isBoss; //Checking to see if AI is the Boss

    public float PetAttraction => petAttraction;
    public float MovementSpeed => movementSpeed;
    public float RestTime => restTime;
    public float TalkTime => talkTime;
    public float PettingDistance => pettingDistance; //Ref
    public float PettingCooldown => pettingCooldown; //Ref
    public float TalkingCooldown => talkingCooldown; //Ref
    public float WorkingCooldown => workingCooldown; //Ref
    public bool IsJanitor => isJanitor; //Ref
    public bool IsBoss => isBoss; //Ref

}
