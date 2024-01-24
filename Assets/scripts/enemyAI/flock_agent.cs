using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flock_agent : MonoBehaviour 
{

    Alignment behaviorAlign = new Alignment();
    public Alignment BehaviorAlign { get { return behaviorAlign; } }

    Avoidance behaviorAvoid = new Avoidance();
    public Avoidance BehaviorAvoid { get { return behaviorAvoid; } }
    bool flocked = false;

    public bool Flocked
    {
        get { return flocked; }
        set { flocked = value; }

    }
    Vector2 moveVector;
    public Vector2 MoveVector { get { return moveVector; } set { moveVector = value; } }
    Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }

    public bool align = true;
    public bool avoid = true;
    public Transform player;
    public AiGrid grid;

    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
       
    }
    
    
}
