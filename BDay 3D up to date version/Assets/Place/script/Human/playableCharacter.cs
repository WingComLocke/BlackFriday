using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackFriday
{
    public class PlayableCharacter : MonoBehaviour
    {

        protected string characterName;//Name of the person, set during the constructor and nopt changed after
        protected int healthPoint;//Abreviated to HP when refering in documentation
        protected int cashStore;//Current availible cash for purchasing
        protected int goalScore;//game victory condition. (Perhaps this should be part of the specific map as opposed to player?)
        protected float movementSpeed;//Player movement speed (some Unit-per-tick)
        protected bool isDead;//If the player is dead or not
        protected int playertatus;//enumerated status
                            // 0 = Despawned
                            // 1 = Normal
                            // 2 = Prone
                            // 3 = Stunned
                            // ...

        protected CapsuleCollider bodySpace;
        protected SphereCollider vulnerabilitySpace;

        protected List<Merch> shoppoingCart;
        protected int cartSize;
        protected bool cartFull;

        protected List<Consumable> inventory;
        protected int invSize;
        protected bool invFull;
        



        protected float HealthPoint
        {
            get
            {
                return healthPoint;
            }
            set
            {
                if (value < 0) {
                    healthPoint = 0;
                    isDead = true;//move this to update 
                    //also we will dael with dropping of the loots
                }
            }
        }

        protected int CartSize
        {
            get
            {
                return cartSize;
            }
            set
            {
                if (value < 0) cartSize = 0;

            }
        }

        

        // Use this for initialization

        protected void collectItem(Collectable theItem)
        {
            ;
        }
        void Start()
        {
            isDead = false;
        }

        // Update is called once per frame
        void Update()
        {
            //we can check this during item pick up
            if (inventory.Count == invSize) invFull = true;
            else invFull = false;
            //trigger during merch pick up
            if (shoppoingCart.Count == cartSize) cartFull = true;
            else cartFull = false;


        }
    }
}

