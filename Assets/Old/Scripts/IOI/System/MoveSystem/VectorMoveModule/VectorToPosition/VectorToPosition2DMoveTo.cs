using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IOIntensiveFramework.Move
{
    public class VectorToPosition2DMoveTo : IVectorToPosition
    {
        [Tooltip("是否镜像")]
        public int direction;
        public VectorToPosition2DMoveTo(bool isMirror)
        {
            direction = isMirror ? -1 : 1;
        }
        public Vector3 VectorToPostion(Vector3 startPosition, Vector3 lastPosition, Vector3 currentPosition, Vector3 moveVector)
        {
            Vector3 vector3 = currentPosition + moveVector * direction;
            return new Vector3(vector3.x, vector3.y, startPosition.z);
        }
    }
}