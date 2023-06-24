using System;
using UnityEngine;

namespace _Scripts {
    public class OpeningAnim : MonoBehaviour {
        public SpriteRenderer[] cogSet;
        public SpriteRenderer[] cloudSet;
        public float[] cloudAlpha = { 0.5f, 0.25f, 0.5f, 0.25f, 0.3f, 0.3f };

        public SpriteRenderer[] studioLogo;
        public SpriteRenderer[] gameLogo;

        public GameObject rainManager;

        public int timer;
        public void FixedUpdate() {
            timer++;

            if (timer >= 100) {
                cogSet[0].color = cogSet[0].color.Appear(32f);
                cogSet[1].color = cogSet[0].color;
            }

            if (timer >= 300 && timer <= 600) {
                studioLogo[0].color = studioLogo[0].color.Appear(256f);
                studioLogo[1].color = studioLogo[1].color.Appear(512f);
            }

            if (timer >= 600) {
                studioLogo[0].color = studioLogo[0].color.Fade(32f);
                studioLogo[1].color = studioLogo[1].color.Fade(64f);
            }

            if (timer >= 800) {
                cogSet[2].color = cogSet[2].color.Appear(128f);
                cogSet[3].color = cogSet[2].color;
                cogSet[4].color = cogSet[2].color;
            }

            if (timer >= 900) {
                for (int i = 0; i <= 3; i++) {
                    if (timer >= 900 + i * 80f) {
                        gameLogo[i].color = gameLogo[i].color.Appear(128f);
                    }
                }
            }

            if (timer >= 1000) {
                for (int i = 0; i <= 5; i++) {
                    if (timer >= 900 + i * 50f) {
                        cloudSet[i].color = cloudSet[i].color.ApproachAlpha(cloudAlpha[i], 128f);
                    }
                }
            }

            if (timer >= 1100) {
                rainManager.SetActive(true);
                gameLogo[4].color = gameLogo[4].color.ApproachAlpha(0.3f, 128f);
                gameLogo[5].color = gameLogo[5].color.SetAlpha(Mathf.Sin(Mathf.Deg2Rad * timer - 1100) * 0.6f + 0.3f);
            }
        }

        public void Start() {
            foreach (var cog in cogSet) {
                cog.color = cog.color.SetAlpha(0f);
            }
            
            foreach (var cloud in cloudSet) {
                cloud.color = cloud.color.SetAlpha(0f);
            }

            foreach (var logo in studioLogo) {
                logo.color = logo.color.SetAlpha(0f);
            }
            foreach (var logo in gameLogo) {
                logo.color = logo.color.SetAlpha(0f);
            }
        }
        
        
    }
}
