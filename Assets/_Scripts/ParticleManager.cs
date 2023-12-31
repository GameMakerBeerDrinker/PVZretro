using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace _Scripts {
    public class ParticleManager : MonoBehaviour {
        public SpriteRenderer rainPrefab;
        public RippleCtrl ripplePrefab;
        public ParticleAnim peaParPrefab;
        public int timer;

        public List<SpriteRenderer> rains;
        public ObjectPool<RippleCtrl> ripplePool;
        public ObjectPool<ParticleAnim> peaParPool;

        public Transform rainFather;
    
        public static ParticleManager Manager;

        private void Awake() {
            if (!Manager) {
                Manager = this;
            }
            else {
                Destroy(this.gameObject);
            }
        }

        public void Start() {
            ripplePool = new ObjectPool<RippleCtrl>(() => Instantiate(ripplePrefab,rainFather), 
                p => {
                    p.gameObject.SetActive(true);
                    p.transform.localScale = Vector3.zero;
                    p.sprRenderer.color = p.sprRenderer.color.SetAlpha(1f);
                }, p => {
                    p.gameObject.SetActive(false);
                }, p => {
                    Destroy(p.gameObject);
                }, false, 50, 200);
            
            peaParPool = new ObjectPool<ParticleAnim>(() => Instantiate(peaParPrefab,rainFather), 
                p => {
                    p.gameObject.SetActive(true);
                    p.transform.localScale = 0.3f * Vector3.one;
                }, p => {
                    p.gameObject.SetActive(false);
                }, p => {
                    Destroy(p.gameObject);
                }, false, 100, 2000);
        }
    
        public void FixedUpdate() {
            if (timer % 10 == 0 && timer <= 1000) {
                var ins = Instantiate(rainPrefab, rainFather);
                ins.transform.position = Random.Range(-10f, 10f) * Vector3.right + 10f * Vector3.up;
                ins.transform.localScale = Random.Range(0.5f, 1.5f) * (Vector3.right + Vector3.up);
                ins.color = ins.color.SetAlpha(Random.Range(0.3f, 0.6f));
                rains.Add(ins);
            }
            
            //0.3 0.6 -4.5 5
            foreach (var drop in rains) {
                drop.transform.position += (drop.color.a + 0.2f) * 20f * Time.fixedDeltaTime * Vector3.down;
                if (drop.transform.position.y <= -(drop.color.a - 0.45f) * 30f) {
                    drop.color = drop.color.Fade(16f);
                    if (drop.color.a.Equal(0f, 0.1f)) {
                        var ripple = ripplePool.Get();
                        ripple.transform.position = drop.transform.position;
                        drop.transform.position = Random.Range(-10f, 10f) * Vector3.right + 10f * Vector3.up;
                        drop.transform.localScale = Random.Range(0.5f, 1.5f) * (Vector3.right + Vector3.up);
                        drop.color = drop.color.SetAlpha(Random.Range(0.3f, 0.6f));
                    }
                }
            }
        
            timer++;
        }
    }
}
