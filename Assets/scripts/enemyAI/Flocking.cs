using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public abstract class flock_behavior
{
    
    public abstract Vector2 CalculateMove(flock_agent agent, List<Collider2D> context,float squareAvoidanceRadius);
}
public class Alignment : flock_behavior
{
    public override Vector2 CalculateMove(flock_agent agent, List<Collider2D> context, float squareAvoidanceRadius)
    {

        if (context.Count == 0)
            return Vector2.zero;



        Vector2 alignmentMove = Vector2.zero;
       
        foreach (Collider2D item in context)
        {
            //if (item.tag == "zombie")
                alignmentMove += (Vector2)item.gameObject.transform.up;
        }
        alignmentMove /= context.Count;

        Debug.Log(alignmentMove);

        return alignmentMove;
    }
}

public class Avoidance : flock_behavior
{
    public override Vector2 CalculateMove(flock_agent agent, List<Collider2D> context, float squareAvoidanceRadius)
    {

       
        if (context.Count == 0)
            return Vector2.zero;


        Vector2 avoidanceMove = Vector2.zero;
        int nAvoid = 0;

        foreach (Collider2D item in context)
        {
            if (Vector2.SqrMagnitude(item.gameObject.transform.position - agent.transform.position) < squareAvoidanceRadius)
            {
                nAvoid++;
                //if (item.tag == "zombie")
                    avoidanceMove += (Vector2)(agent.transform.position - item.gameObject.transform.position);
            }
        }
        if (nAvoid > 0)
            avoidanceMove /= nAvoid;
        Debug.Log(avoidanceMove);
        return avoidanceMove;
    }
}

public class Cohesion : flock_behavior
{
    public override Vector2 CalculateMove(flock_agent agent, List<Collider2D> context, float squareAvoidanceRadius)
    {

        
        if (context.Count == 0)
            return Vector2.zero;

     
        Vector2 cohesionMove = Vector2.zero;
     
        foreach (Collider2D item in context)
        {
            //if(item.tag=="zombie")
                cohesionMove += (Vector2)item.gameObject.transform.position;
        }
        cohesionMove /= context.Count;

      
        cohesionMove -= (Vector2)agent.transform.position;

        Debug.Log(cohesionMove);

        return cohesionMove;
    }
}
