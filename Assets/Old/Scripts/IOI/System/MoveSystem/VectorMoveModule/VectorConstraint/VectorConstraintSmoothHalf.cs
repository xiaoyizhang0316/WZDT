using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IOIntensiveFramework.Move
{
    public class VectorConstraintSmoothHalf : IVectorConstraint
    {
        /// <summary>
        /// 平滑精度：越小滑动时间越长，不能等于0
        /// </summary>
        public float smoothPrecision;
        /// <summary>
        /// 衰弱系数：越小滑动时间越长,小于1，大于0
        /// </summary>
        public float gradientCoefficient;


        [Tooltip("移动系数")]
        public float moveCoefficient;


        public VectorConstraintSmoothHalf(float smoothPrecision,float gradientCoefficient, float moveCoefficient)
        {
            SetSmoothPrecision(smoothPrecision);
            SetGradientCoefficient(gradientCoefficient);
            this.moveCoefficient = moveCoefficient;
        }
        public void SetSmoothPrecision(float smoothPrecision)
        {
            this.smoothPrecision = smoothPrecision;
        }
        public void SetGradientCoefficient(float gradientCoefficient)
        {
            this.gradientCoefficient = gradientCoefficient;
        }
        public Vector3 VectorConstraint(Vector3 startPosition, Vector3 lastPosition, Vector3 currentPosition, Vector3 moveVector)
        {
            if (moveVector.sqrMagnitude < smoothPrecision)
            {
                return Vector3.zero;
            }
            else
            {
                return moveVector*(1- gradientCoefficient)* moveCoefficient;
            }
        }
    }
}