using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackFriday
{
    public class PlayableCharacter : MonoBehaviour
    {

        protected string characterName;//constructor
        protected float healthPoint;
        protected float cashScore;
        protected float goalScore;
        protected float movementSpeed;
        protected bool isDead;

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

