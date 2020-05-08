using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IOIntensiveFramework.Move
{
    public class VectorMoveCore
    {
        Transform transform;
        IVectorToPosition vectorToPosition;
        IPositionConstraint positionConstraint;
        IVectorConstraint vectorConstraint;
        Vector3 startPosition;
        Vector3 lastPostion;
        Vector3 moveVector;
        public Vector3 targetPosition;
        public VectorMoveCore(Transform transform, IVectorConstraint vectorConstraint, IVectorToPosition vectorToPosition, IPositionConstraint positionConstraint)
        {
            startPosition = transform.position;
            lastPostion = startPosition;
            this.transform = transform;
            this.vectorConstraint = vectorConstraint;
            this.vectorToPosition = vectorToPosition;
            this.positionConstraint = positionConstraint;
        }
        public void SetMoveVector(Vector3 moveVector)
        {
            this.moveVector = moveVector;
        }
        public void VectorMoveDelta(Vector3 moveVector)
        {
            transform.position += moveVector;
        }
        public Vector3 VectorMoveDelta()
        {
            if (vectorConstraint != null)
            {
                moveVector = vectorConstraint.VectorConstraint(startPosition, lastPostion, transform.position, moveVector);
            }
            if (moveVector != Vector3.zero)
            {
                targetPosition = Vector3.zero;
                if (vectorToPosition != null)
                {
                    targetPosition = vectorToPosition.VectorToPostion(startPosition, lastPostion, transform.position, moveVector);
                }
                if (positionConstraint != null)
                {
                    targetPosition = positionConstraint.PositionConstraint(startPosition, lastPostion, transform.position, targetPosition);
                }
                lastPostion = transform.position;
                transform.position = targetPosition;
            }
            return moveVector;
        }
    }
}