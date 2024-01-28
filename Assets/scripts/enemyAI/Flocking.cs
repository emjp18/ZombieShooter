using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace FLOCK_BEHAVIOUR
{
    enum behaviour_order { ALIGNMENT = 0, AVOIDANCE = 1, COHESION = 2, STAY_IN_RADIUS = 3 };
}

public abstract class flock_behavior
{
   
    protected Vector2 forward;
    public void SetForward(Vector2 forward) { this.forward = forward; }

    protected float avoidThreshold;
    public void SetAvoidanceThreshold(float avoidThreshold) { this.avoidThreshold = avoidThreshold; }
    public abstract Vector2 CalculateMove(Vector2 agentPos, List<Collider2D> context);

    protected float radius;

    public void SetRadius(float r) { radius = r; }
    protected float alignRadius;

    public void SetAlignRadius(float r) { alignRadius = r; }
}
public class Alignment : flock_behavior
{
    public override Vector2 CalculateMove(Vector2 agentPos, List<Collider2D> context)
    {

        if (context.Count == 0)
            return Vector2.zero;

        Zombie_Flock_Prefab_Script component;

        Vector2 alignmentMove = forward;
       
        foreach (Collider2D item in context)
        {

            if (!item.gameObject.TryGetComponent<Zombie_Flock_Prefab_Script>(out component))
            {
                continue;
            }
            if ((agentPos - (Vector2)item.gameObject.transform.position).magnitude < alignRadius)
                alignmentMove += item.gameObject.GetComponent<Zombie_Flock_Prefab_Script>().Direction;
        }
     


        return alignmentMove.normalized;
    }
}

public class Avoidance : flock_behavior
{
    public override Vector2 CalculateMove(Vector2 agentPos, List<Collider2D> context)
    {

       
        if (context.Count == 0)
            return Vector2.zero;

       
        Vector2 avoidanceMove = Vector2.zero;
      
        foreach (Collider2D item in context)
        {
            
            if ((agentPos - (Vector2)item.gameObject.transform.position).magnitude < avoidThreshold)
            {
                avoidanceMove -= ((Vector2)item.gameObject.transform.position - agentPos);
            }
          


        }
       

        return avoidanceMove.normalized;
    }
}

public class Cohesion : flock_behavior
{
    public override Vector2 CalculateMove(Vector2 agentPos, List<Collider2D> context)
    {
        Zombie_Flock_Prefab_Script component;

        if (context.Count == 0)
            return Vector2.zero;

       
        Vector2 cohesionMove = Vector2.zero;
     
        foreach (Collider2D item in context)
        {
            if (!item.gameObject.TryGetComponent<Zombie_Flock_Prefab_Script>(out component))
            {
                continue;
            }

            if ((agentPos-(Vector2)item.gameObject.transform.position).magnitude<radius)
            {
                cohesionMove += (Vector2)item.gameObject.transform.position;
            }

          
               
        }
   

        return cohesionMove.normalized;


    }
}
public class StayInRadiusBehavior : flock_behavior
{
   

    public override Vector2 CalculateMove(Vector2 agentPos, List<Collider2D> context)
    {
        Zombie_Flock_Prefab_Script component;
        Vector2 center = Vector2.zero;
        foreach (Collider2D item in context)
        {
            if (!item.gameObject.TryGetComponent<Zombie_Flock_Prefab_Script>(out component))
            {
                continue;
            }

            center += (Vector2)item.gameObject.transform.position*0.5f;
        }

       
        return ((center - (Vector2)agentPos)).normalized;
        
    }
}

public class AvoidanceObstacle : flock_behavior
{
    public override Vector2 CalculateMove(Vector2 agentPos, List<Collider2D> context)
    {


        if (context.Count == 0)
            return Vector2.zero;

       
        Vector2 avoidanceMove = Vector2.zero;

        foreach (Collider2D item in context)
        {
            
            if ((agentPos - (Vector2)item.gameObject.transform.position).magnitude < avoidThreshold)
            {
                avoidanceMove -= ((Vector2)item.gameObject.transform.position - agentPos);
            }



        }


        return avoidanceMove.normalized*10;
    }
}