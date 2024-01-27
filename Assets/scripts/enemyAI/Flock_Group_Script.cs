using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows.Speech;


public class Flock_Group_Script
{
    ContactFilter2D contactFilter = new ContactFilter2D();
    float maxSpeed;
    List<Collider2D> nearbyCollider = new List<Collider2D>();
    Vector2 move;
    Vector2 partialMove;
    Transform spawn;
    float chaseRange;
    float weightCohesion;
    float weightAvoidance;
    float weightAlignment;
    float weightStayWithinRadius;
    float speed;
    float[] weights = new float[4];
    float avoidanceThreshold;
    List<Zombie_Flock_Prefab_Script> agents;
   
    public Transform Spawn {  get { return spawn; } }

    Vector2 forward;
    public Flock_Group_Script(Transform spawnpoint, int agentCount,
        float chaseRange, float weightCohesion, float weightAvoidance, float weightAlignment,
        float weightStayWithinRadius,float speed, float avoidanceThreshold,int layerMask, float maxSpeed,
         List<Zombie_Flock_Prefab_Script> agents)
    {
        spawn = spawnpoint;
        contactFilter.layerMask = layerMask;

        for (int i=0; i<agentCount; i++)
        {

            this.agents = agents;

            this.agents[i].Flock_Agent.UpdateVariables(chaseRange,weightCohesion,weightAvoidance,weightAlignment,
                weightStayWithinRadius,speed);
        }
        weights[0] = weightAlignment;
        weights[1] = weightAvoidance;
        weights[2] = weightCohesion;
        weights[3] = weightStayWithinRadius;
        this.maxSpeed = maxSpeed;
        this.speed = speed;
        this.avoidanceThreshold = avoidanceThreshold;
    }
    public void UpdateVariables(float chaseRange, float weightCohesion, float weightAvoidance, float weightAlignment,
        float weightStayWithinRadius, float speed,float avoidanceThreshold)
    {
        foreach(Zombie_Flock_Prefab_Script agent in agents)
        {
            agent.Flock_Agent.UpdateVariables(chaseRange, weightCohesion, weightAvoidance, weightAlignment,
               weightStayWithinRadius, speed);
        }
        weights[0] = weightAlignment;
        weights[1] = weightAvoidance;
        weights[2] = weightCohesion;
        weights[3] = weightStayWithinRadius;
        this.speed = speed;
        this.avoidanceThreshold = avoidanceThreshold;
    }
    public void Update(Vector2 playerPos)
    {



        foreach (Zombie_Flock_Prefab_Script agent in agents)
        {

            
            agent.PlayerPos = playerPos;
            agent.ChaseRange = chaseRange;

           

            if (agent.WithinChaseRangeCheck())
            {
                forward = (playerPos - (Vector2)agent.gameObject.transform.position).normalized;

                move = Vector2.zero;

                nearbyCollider.Clear();
                agent.AgentCollider.OverlapCollider(contactFilter, nearbyCollider);


                if (nearbyCollider.Count != 0)
                {
                    for (int i = 0; i < 4; i++)
                    {


                        agent.Flock_Agent.Behaviors[i].SetForward(forward);
                        partialMove = agent.Flock_Agent.Behaviors[i].CalculateMove(agent.gameObject.transform.position,
                        nearbyCollider);


                        partialMove *= weights[i];

                        move += partialMove;
                    }
                    move.Normalize();
                    move *= speed;
                    if (move.magnitude > maxSpeed)
                    {
                        move = move.normalized * maxSpeed;
                    }
                    agent.transform.position += ((Vector3)(move)) * Time.deltaTime;
                    agent.Direction = move.normalized;
                }


            }
            else
            {
                move = Random.insideUnitCircle;

                nearbyCollider.Clear();
                agent.AgentCollider.OverlapCollider(contactFilter, nearbyCollider);

                agent.Flock_Agent.Behaviors[(int)FLOCK_BEHAVIOUR.behaviour_order.AVOIDANCE].SetAvoidanceThreshold(avoidanceThreshold);
                if (nearbyCollider.Count != 0)
                {
                    
                    partialMove = agent.Flock_Agent.Behaviors[(int)FLOCK_BEHAVIOUR.behaviour_order.AVOIDANCE].CalculateMove(
                        agent.gameObject.transform.position,
                    nearbyCollider);

                    if (partialMove == Vector2.zero)
                        move = Vector2.zero;
                    move += partialMove;
                    move.Normalize();
                    move *= speed;
                    if (move.magnitude > maxSpeed)
                    {
                        move = move.normalized * maxSpeed;
                    }
                    agent.transform.position += ((Vector3)(move)) * Time.deltaTime;
                    agent.Direction = move.normalized;
                }
            }








            

           

            forward = agent.Direction.normalized;

            if (agent.WithinChaseRangeCheck())
            {
                
            }
           




        }

    }
    
}




