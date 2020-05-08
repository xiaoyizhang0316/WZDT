using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IOIntensiveFramework.Move
{
    public class MoveTo : IMoveToTarget
    {
        public void StopMove(Transform transform, Vector3 startPosition, Vector3 lastConstraintTarget, Vector3 constraintTarget, Action complete)
        {
            transform.position = lastConstraintTarget;
            if (complete != null)
            {
                complete();
            }
        }
        public void MoveToTarget(Transform transform, Vector3 startPosition, Vector3 lastConstraintTarget, Vector3 constraintTarget, Action complete)
        {
            transform.position = constraintTarget;
            if (complete != null)
            {
                complete();
            }
        }
    }
}