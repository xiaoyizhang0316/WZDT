using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IOIntensiveFramework.Move
{
    public class PositionConstraintSquareEdge : IPositionConstraint
    {
        [Tooltip("是否使用边界")]
        public bool isEdge = true;
        [Tooltip("上边界")]
        public float moveUpEdge;
        [Tooltip("下边界")]
        public float moveDownEdge;
        [Tooltip("左边界")]
        public float moveLeftEdge;
        [Tooltip("右边界")]
        public float moveRightEdge;
        public PositionConstraintSquareEdge(float moveUpEdge, float moveDownEdge, float moveLeftEdge, float moveRightEdge)
        {
            this.moveUpEdge = moveUpEdge;
            this.moveDownEdge = moveDownEdge;
            this.moveLeftEdge = moveLeftEdge;
            this.moveRightEdge = moveRightEdge;
        }
        public Vector3 PositionConstraint(Vector3 startPosition, Vector3 lastPosition, Vector3 currentPosition, Vector3 targetPosition)
        {
            float hor = targetPosition.x - startPosition.x;
            float ver = targetPosition.y - startPosition.y;
            Vector3 move = new Vector3();
            if (isEdge)
            {
                if (hor > moveRightEdge)
                {
                    move.x = startPosition.x + moveRightEdge;
                }
                else if (hor < -moveLeftEdge)
                {
                    move.x = startPosition.x - moveLeftEdge;
                }
                else
                {
                    move.x = targetPosition.x;
                }
                if (ver > moveUpEdge)
                {
                    move.y = startPosition.y + moveUpEdge;
                }
                else if (ver < -moveDownEdge)
                {
                    move.y = startPosition.y - moveDownEdge;
                }
                else
                {
                    move.y = targetPosition.y;
                }
            }
            else
            {
                move = targetPosition;
                moveUpEdge = ver;
                moveDownEdge = ver;
                moveLeftEdge = hor;
                moveRightEdge = hor;
            }
            move.z = startPosition.z;
            return move;
        }
    }
}