using System;
using System.Timers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts {
    public class PeaAnim : MonoBehaviour {
        // 勾选is snow初值设定雪花豌豆
        public ParticleAnim particlePrefab;
        public bool isSnow;
        public Sprite sprSnow;

        public int timer;

        private void Start() {
            if (isSnow) GetComponent<SpriteRenderer>().sprite = sprSnow;
        }

        public void SetSnow() {
            isSnow = true;
            GetComponent<SpriteRenderer>().sprite = sprSnow;
        }
        
        private void FixedUpdate() {
            if (timer % 5 == 0) {
                var randPos = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                ParticleAnim particle = Instantiate(particlePrefab, this.transform.position + randPos, Quaternion.Euler(180f * randPos));
                if (isSnow) particle.isSnow = true;
            }

            timer++;
        }
    }
}
