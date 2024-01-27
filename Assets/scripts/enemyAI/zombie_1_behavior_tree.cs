using Behavior_Tree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class zombie_1_behavior_tree : flock_agent
{
    
 
    
   

}
//namespace Behavior_Tree
//{
//    public class Idle : Node
//    {
//        public Idle() : base() { }
//        public override NodeState Evaluate()
//        {
//            if ((bool)GetData("withinChaseRange"))
//            {
             
//                return NodeState.FAILURE;
//            }
//            SetData("movementDirection", Vector2.zero);
//            SetData("chasing", false);

//            return NodeState.RUNNING;
//        }
//    }
//    public class Chase : Node
//    {
//        public Chase() : base() { }
       
//        public override NodeState Evaluate()
//        {
//            if (!(bool)GetData("withinChaseRange"))
//            {
//                return NodeState.FAILURE;
//            }

//            SetData("chasing", true);


//            return NodeState.RUNNING;
//        }
//}
//}