using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public abstract class flock_behavior
{
    
    public abstract Vector2 CalculateMove(flock_agent agent, Collider2D[] context,float squareAvoidanceRadius);
}
public class Alignment : flock_behavior
{
    public override Vector2 CalculateMove(flock_agent agent, Collider2D[] context, float squareAvoidanceRadius)
    {
        
        if (context.Length == 0)
            return agent.transform.forward;

    
        Vector2 alignmentMove = Vector2.zero;
       
        foreach (Collider2D item in context)
        {
            alignmentMove += (Vector2)item.transform.forward;
        }
        alignmentMove /= context.Length;

        return alignmentMove;
    }
}

public class Avoidance : flock_behavior
{
    public override Vector2 CalculateMove(flock_agent agent, Collider2D[] context, float squareAvoidanceRadius)
    {

       
        if (context.Length == 0)
            return agent.transform.forward;


        Vector2 avoidanceMove = Vector2.zero;
        int nAvoid = 0;

        foreach (Collider2D item in context)
        {
            if (Vector2.SqrMagnitude(item.transform.position - agent.transform.position) < squareAvoidanceRadius)
            {
                nAvoid++;
                avoidanceMove += (Vector2)(agent.transform.position - item.transform.position);
            }
        }
        if (nAvoid > 0)
            avoidanceMove /= nAvoid;

        return avoidanceMove;
    }
}