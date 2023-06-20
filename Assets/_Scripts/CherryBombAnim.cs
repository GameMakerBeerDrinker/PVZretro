using System;
using UnityEngine;

namespace _Scripts {
    public class CherryBombAnim : MonoBehaviour {
        public Transform leftCore;
        public Transform rightCore;

        public int explodeTime;
        public int timer;

        private void FixedUpdate() {
            leftCore.transform.position = Vector3.left + (Mathf.Abs(0.5f * Mathf.Sin(5f * timer * Mathf.Deg2Rad)) - 0.2f) * Vector3.up;
            rightCore.transform.position =
                3f * Vector3.right + (3f - Mathf.Abs(0.5f * Mathf.Sin(5f * timer * Mathf.Deg2Rad) - 0.2f)) * Vector3.up;

            leftCore.transform.localScale =
                (0.1f * Mathf.Abs(Mathf.Sin(5f * timer * Mathf.Deg2Rad)) + 1.2f) * Vector3.one;
            rightCore.transform.localScale =
                (0.1f * Mathf.Abs(Mathf.Sin(5f * timer * Mathf.Deg2Rad)) + 0.5f) * Vector3.one;
            timer++;
        }
    }
}
