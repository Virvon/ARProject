using System;
using UnityEngine;

namespace Assets.Sources.BaseLogic.EnvironmentObjectCreation
{
    public class EnvironmentObject : MonoBehaviour
    {
        Vector3 x;
        public void Draw(Vector3 worldPosition)
        {
            x = worldPosition;
        }

        private void OnDrawGizmos()
        {
            if (x == null)
                return;

            Gizmos.DrawSphere(x, 0.2f);
        }
    }
}
