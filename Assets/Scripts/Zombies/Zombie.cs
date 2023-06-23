using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;

namespace Zombies {
    public enum ZombieName
    {
        RegularZombie,
        FlagZombie,
        ConeheadZombie,
        PoleVaultingZombie,
        BucketheadZombie,
        NewspaperZombie,
    }

    public abstract class Zombie : MonoBehaviour
    {
        public ZombieName zombieName;

        public ZombieAnim zombieAnim;
        public GameObject armor;
        public Material brokenArmor;

        public bool withArmor;
        public int armorHealth;
        public int bodyHealth;
        public int maxhealth;

        public int currentHealth;

        public float speed;
        protected float halfspeed;

        protected bool isEating;
        public int damage;
        protected float halfdamage;

        public bool isCold;
        public int coldTimer;

        public GameObject parent;
        public ZombieAnim anim;

        private void Start()
        {
            currentHealth = maxhealth;
            //TODO:here also
            //zombieAnim.maxHp = currentHealth / 20;
            halfspeed = speed / 2;
            halfdamage = damage * 1f / 2;
        }

        protected void FixedUpdate()
        {
            ArmorBreak();
            if (!isEating)
                Move();
            GetCold();
        }

        public virtual void ArmorBreak()
        {
            if (withArmor)
            {
                if (currentHealth - bodyHealth < armorHealth / 2)
                {
                    anim.armor.material = brokenArmor;
                }
                if (currentHealth <= bodyHealth)
                {
                    anim.armor.sprite = null;
                }
            }
        }

        protected virtual void GetCold()
        {
            coldTimer++;
            if (coldTimer >= 1000)
                isCold = false;
        }

        private void Move()
        {
            if(!isCold)
                transform.position += speed * Vector3.left * Time.fixedDeltaTime;
            else
                transform.position += speed/2 * Vector3.left * Time.fixedDeltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag=="Plant")
            {
                isEating = true;
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.tag == "Plant")
            {
                collision.GetComponent<Plant>().isTakingDamage = true;
                collision.GetComponent<Plant>().TakeDamage(damage);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.tag=="Plant")
            {
                isEating = false;
                collision.GetComponent<Plant>().isTakingDamage = false;
            }
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            //TODO:fix here later
            //zombieAnim.maxHp = currentHealth / 20;
        
            //Debug.Log(currentHealth);
            if (currentHealth <= 0)
                Die();
        }

        protected void Die()
        {
            GetComponent<Collider2D>().enabled = false;
            ZombieManager.instance.aliveZombies.Remove(this);
            ZombieManager.instance.currentWaveZombies.Remove(this);
            ZombieAnimManager.Manager.zombieAnimPool.Release(anim);
            //��������......

            Destroy(this.gameObject);
        }
    }
}