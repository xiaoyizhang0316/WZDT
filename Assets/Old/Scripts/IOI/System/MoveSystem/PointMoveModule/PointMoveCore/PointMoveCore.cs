using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IOIntensiveFramework.Move
{
    public class PointMoveCore
    {
        Transform transform;
        ITargetConstraint targetConstraint;
        IMoveToTarget moveToTarget;
        Vector3 startPostion;
        Vector3 originalTarget;
        Vector3 constraintTarget;
        Vector3 lastConstraintTarget;
        Vector3 lastPosition;
        public PointMoveCore(Transform transform, ITargetConstraint targetConstraint, IMoveToTarget moveToTarget)
        {
            this.transform = transform;
            this.targetConstraint = targetConstraint;
            this.moveToTarget = moveToTarget;
            this.startPostion = transform.position;
            lastPosition = startPostion;
        }
        public Vector3 MoveToTarget(Vector3 originalTarget, Action complete)
        {
            this.originalTarget = originalTarget;
            if (moveToTarget == null) { return originalTarget; }
            lastConstraintTarget = constraintTarget;
            if (targetConstraint != null)
            {
                constraintTarget = targetConstraint.TargetConstraint(transform, startPostion, originalTarget);
            }
            else
            {
                constraintTarget = originalTarget;
            }
            if(lastConstraintTarget!= constraintTarget)
            {
                lastPosition = constraintTarget;
            }
            moveToTarget.MoveToTarget(transform, startPostion, lastPosition, constraintTarget, complete);
            
            return constraintTarget;
        }
        public void StopMove(Action complete)
        {
            if (moveToTarget == null) { return; }
            moveToTarget.StopMove(transform, startPostion, lastPosition, constraintTarget, complete);
        }
    }
}