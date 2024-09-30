using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoardState : MonoBehaviour
{
    public MonsterAI monsterAI;
    public Blackboard blackboard;

    [SerializeField] float curMeleeAttackCool;
    [SerializeField] float curSkillCool;
    [SerializeField] bool meleeAttackReady;
    [SerializeField] bool skillReady;


    private void Start()
    {
        blackboard = monsterAI.blackboard;
    }

    private void Update()
    {
        curMeleeAttackCool = blackboard.GetValue<float>("CurMeleeCool");
        curSkillCool = blackboard.GetValue<float>("CurSkillCool");
        meleeAttackReady = blackboard.GetValue<bool>("MeleeAttackReady");
        skillReady = blackboard.GetValue<bool>("SkillReady");
    }
}
