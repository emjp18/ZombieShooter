using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;



public class Flock_Group_Script
{

   

    ContactFilter2D contactFilter = new ContactFilter2D();
    ContactFilter2D contactFilterWalls = new ContactFilter2D();
    float maxSpeed;
    List<Collider2D> nearbyCollider = new List<Collider2D>();
    List<Collider2D> nearbyColliderWalls = new List<Collider2D>();
    Vector2 move;
    Vector2 partialMove;
    Transform spawn;
    float chaseRange;
    float gridcellsize;
    float speed;
    float[] weights = new float[4];
    float avoidRadius;
    float AlignRadius;
    float CohesionRadius;
    List<Zombie_Flock_Prefab_Script> agents;
    int agentCount;
    public Transform Spawn {  get { return spawn; } }

    public bool allDead = false;
    Vector2 forward;
    public Flock_Group_Script(Transform spawnpoint, int agentCount,
        float chaseRange, float weightCohesion, float weightAvoidance, float weightAlignment,
        float weightStayWithinRadius,float speed, float avoidR, float alignR, float cohesionr,int layerMask, float maxSpeed,
         List<Zombie_Flock_Prefab_Script> agents, float gridcellsize)
    {
        spawn = spawnpoint;
        contactFilter.layerMask = layerMask;
        contactFilterWalls.layerMask = 7;
        for (int i=0; i<agentCount; i++)
        {

            this.agents = agents;

       
        }
        this.gridcellsize = gridcellsize;
        weights[0] = weightAlignment;
        weights[1] = weightAvoidance;
        weights[2] = weightCohesion;
        weights[3] = weightStayWithinRadius;
        this.maxSpeed = maxSpeed;
        this.speed = speed;
        avoidRadius = avoidR;
        AlignRadius = alignR;
        CohesionRadius = cohesionr;
        this.agentCount = agentCount;


    }
    public void UpdateVariables(float chaseRange, float weightCohesion, float weightAvoidance, float weightAlignment,
        float weightStayWithinRadius, float speed, float avoidR, float alignR, float cohesionr)
    {
        
        weights[0] = weightAlignment;
        weights[1] = weightAvoidance;
        weights[2] = weightCohesion;
        weights[3] = weightStayWithinRadius;
        this.speed = speed;
        avoidRadius = avoidR;
        AlignRadius = alignR;
        CohesionRadius = cohesionr;
        this.chaseRange = chaseRange;
    }
    public void Update(Vector2 playerPos, AStar2D navigation)
    {

        forward = Random.insideUnitCircle;// Vector2.zero;


        for (int i=0; i<agentCount; i++)
        {
            if(i<agents.Count)
            {
                if (agents[i] == null)
                {
                    agents.Remove(agents[i]);
                }
            }
            
        }
       

        foreach (Zombie_Flock_Prefab_Script agent in agents)
        {
           if(agent==null)
            {
              
                continue;
            }
                


            agent.PlayerPos = playerPos;
            agent.ChaseRange = chaseRange;
           

            bool chasePlayer = agent.WithinChaseRangeCheck();

            //forward = (playerPos - (Vector2)agent.gameObject.transform.position).normalized;
            if (chasePlayer)
            {
                forward = (playerPos - (Vector2)agent.gameObject.transform.position).normalized;
                agent.Root_AI_Node.SetData("withinChaseRange", true);
            }
                



            agent.Direction = forward;
            move = Vector2.zero;

            nearbyCollider.Clear();
            agent.AgentCollider.OverlapCollider(contactFilter, nearbyCollider);
            
          

            for (int i = 0; i < 3; i++)
            {


                agent.Flock_Agent.Behaviors[i].SetForward(forward);

                agent.Flock_Agent.Behaviors[i].SetAlignRadius(AlignRadius);
                agent.Flock_Agent.Behaviors[i].SetAvoidanceThreshold(avoidRadius);
                agent.Flock_Agent.Behaviors[i].SetRadius(CohesionRadius);

                if (nearbyCollider.Count != 0)
                {

                    partialMove = agent.Flock_Agent.Behaviors[i].CalculateMove(agent.gameObject.transform.position,
                    nearbyCollider);

                }
                

                partialMove *= weights[i];

                move += partialMove;


            }



           



            move.Normalize();

            //if (navigation.FindObstacleFlocking(agent.transform.position+(Vector3)(move*gridcellsize), navigation.Quadtree, 50))
            //{
            //    move -= (Vector2)agent.transform.position + (move * gridcellsize) - ((Vector2)agent.transform.position);
            //}

            move *= speed;
            if (move.magnitude > maxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
       
           
            agent.transform.position += ((Vector3)(move)) * Time.deltaTime;

            if(((Vector2)agent.transform.position-playerPos).magnitude<agent.attackRange)
            {
                agent.Root_AI_Node.SetData("withinAttackRange", true);
            }
            else
            {
                agent.Root_AI_Node.SetData("withinAttackRange", false);
            }
        }
    }
}




