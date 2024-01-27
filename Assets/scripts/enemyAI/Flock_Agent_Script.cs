using Behavior_Tree;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class Flock_Agent_Script : AI_Agent
{
    Idle idle_AI_Node;

    float chaseRange;
    float weightCohesion;
    float weightAvoidance;
    float weightAlignment;
    float weightStayWithinRadius;
    float speed;
    bool withinRange = false;
    flock_behavior[] behaviours= new flock_behavior[4];
   
    public flock_behavior[] Behaviors {  get { return behaviours; } }
    
    public Flock_Agent_Script()
    {
        

        behaviours[0] = new Alignment();
        behaviours[1] = new Avoidance();
        behaviours[2] = new Cohesion();
        behaviours[3] = new StayInRadiusBehavior();
    }
    public void UpdateVariables(float chaseRange, float weightCohesion, float weightAvoidance, float weightAlignment,
        float weightStayWithinRadius, float speed)
    {
        this.chaseRange = chaseRange;
        this.weightAlignment = weightAlignment;
        this.weightAvoidance = weightAvoidance;
        this.weightCohesion = weightCohesion;
        this.weightStayWithinRadius = weightStayWithinRadius;
        this.speed = speed;
    }
    

    public override void Evaluate_Tree()
    {
        

      

        root_AI_Node.Evaluate();

      

    }

    public override void Setup_Tree()
    {
        idle_AI_Node = new Idle();
       

        children_AI_Nodes = new List<Node>
        {
            idle_AI_Node,
       
        };

        root_AI_Node = new Root(children_AI_Nodes);

       
    }

    
}
