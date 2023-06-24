using System;
using UnityEngine;
using UnityEngine.Pool;

namespace _Scripts {
    public class ZombieAnimManager : MonoBehaviour
    {
        public static ZombieAnimManager Manager;

        private void Awake() {
            if (!Manager) {
                Manager = this;
            }
            else {
                Destroy(this.gameObject);
            }
        }

        public ObjectPool<ZombieAnim> zombieAnimPool;
        public ZombieAnim zombieAnimPrefab;
        private void Start() {
            zombieAnimPool = new ObjectPool<ZombieAnim>(
                () => Instantiate(zombieAnimPrefab, transform),
                p => {
                    p.gameObject.SetActive(true);
                }, p => {
                    p.body = null;
                    p.gameObject.SetActive(false);
                }, p => {
                    Destroy(p.gameObject);
                }, false, 50, 200);
        }
    }
}
