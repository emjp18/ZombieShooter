using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Flock_Prefab_Script : MonoBehaviour
{
    Flock_Agent_Script flocking_script = new Flock_Agent_Script();
    CircleCollider2D agentCollider;

    Vector2 direction = Vector2.zero;
    Vector2 playerPos;

    float chaseRange;

    public float ChaseRange { set { chaseRange = value; } }
    public Vector2 PlayerPos { set { playerPos = value; } }
    public Vector2 Direction { get { return direction; } set { direction = value; } }
    public CircleCollider2D AgentCollider { get { return agentCollider; } }
    public Flock_Agent_Script Flock_Agent {  get { return flocking_script; } }
    void Start()
    {
      
        agentCollider = GetComponent<CircleCollider2D>();
    }

    public bool WithinChaseRangeCheck()
    {

        return ((Vector2)transform.position - playerPos).magnitude < chaseRange;



    }

}
