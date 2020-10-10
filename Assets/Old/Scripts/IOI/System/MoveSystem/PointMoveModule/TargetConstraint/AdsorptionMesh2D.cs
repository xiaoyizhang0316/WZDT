using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IOIntensiveFramework.Move
{
    /// <summary>
    /// 吸附到网格中心
    /// </summary>
    public class AdsorptionMesh2D : ITargetConstraint
    {
        public AdsorptionMesh2D(float gridDiameter, int xGrid, int yGrid)
        {
            this.gridDiameter = gridDiameter;
            this.xGrid = xGrid;
            this.yGrid = yGrid;
        }
        /// <summary>
        /// 移动差值
        /// </summary>
        public float gridDiameter;
        /// <summary>
        /// 横向占用几个格子
        /// </summary>
        public int xGrid;
        /// <summary>
        /// 纵向占用几个格子
        /// </summary>
        public int yGrid;
        /// <summary>
        /// 对目标点进行约束
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="startPosition"></param>
        /// <param name="originalTarget"></param>
        /// <returns></returns>
        public Vector3 TargetConstraint(Transform transform, Vector3 startPosition, Vector3 originalTarget)
        {
            Vector3 constraintTarget = new Vector3();
            Vector3 adsorptionSite = originalTarget + new Vector3((xGrid * gridDiameter - 1) / 2, (yGrid * gridDiameter - 1) / 2, 0);
            float x = adsorptionSite.x % gridDiameter;
            float y = adsorptionSite.y % gridDiameter;
            if (x >= 0)
            {
                if (x >= gridDiameter / 2)
                {
                    constraintTarget.x = adsorptionSite.x - x + gridDiameter;
                }
                else
                {
                    constraintTarget.x = adsorptionSite.x - x;
                }
            }
            else
            {
                if (x <= -(gridDiameter / 2))
                {
                    constraintTarget.x = adsorptionSite.x - x - gridDiameter;
                }
                else
                {
                    constraintTarget.x = adsorptionSite.x - x;
                }
            }
            if (y >= 0)
            {
                if (y >= gridDiameter / 2)
                {
                    constraintTarget.y = adsorptionSite.y - y + gridDiameter;
                }
                else
                {
                    constraintTarget.y = adsorptionSite.y - y;
                }
            }
            else
            {
                if (y <= -(gridDiameter / 2))
                {
                    constraintTarget.y = adsorptionSite.y - y - gridDiameter;
                }
                else
                {
                    constraintTarget.y = adsorptionSite.y - y;
                }
            }
            constraintTarget -= new Vector3(xGrid * gridDiameter  / 2, yGrid * gridDiameter / 2, 0);
            return constraintTarget;
        }
    }
}