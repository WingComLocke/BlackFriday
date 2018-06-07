using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackFriday
{
    public class PlayableCharacter : MonoBehaviour
    {
        //Basic Characteristics
        protected string characterName;//Name of the person, set during the constructor and nopt changed after

        //HP and manipulation methodes
        protected int healthPoint;//Abreviated to HP when refering in documentation
        protected int maxHealthPoint; //Maximum Health point possible
        protected void summationHealth(int deltaHP); //adds or subtracts health by a flat number
        protected void percentHPChangeIncreaseMax(int deltaHpPercentUpMax); //Percent adjustment of HP increase (Max health)
        protected void percentHPChangeDecreaseMax(int deltaHpPercentDownMax); //Percent adjustment of HP increase (Max Health)
        protected void percentHPChangeIncreaseCur(int deltaHpPercentUpCur); //Percent adjustment of HP increase (Current health)
        protected void percentHPChangeDecreaseCur(int deltaHpPercentDownCur); //Percent adjustment of HP increase (Current Health)
        
        //Score related functions
        protected int cashStore;//Current availible cash for purchasing
        protected int goalScore;//game victory condition. (Perhaps this should be part of the specific map as opposed to player?)

        //Mobility modifiers
        protected float movementSpeed;//Player movement speed (some Unit-per-tick)
        protected void summationMS(int deltaMS); //adds or subtracts MS by a flat number
        protected void percentMSChangeIncreaseCur(int deltaMSPercentUpCur); //Percent adjustment of MS increase (Current MS)
        protected void percentMSChangeDecreaseCur(int deltaMSPercentDownCur); //Percent adjustment of MS increase (Current MS)

        //Player condition/Status 
        protected bool isDead;//If the player is dead or not
        protected int playerStatus;//enumerated status
                                   // 0 = Despawned
                                   // 1 = Normal
                                   // 2 = Prone
                                   // 3 = Stunned
                                   // 4 = Dead
                                   // 5 = Jumping...

        //Jumping: Normally players would be blocked by certain objects and obstacles (collision on). "Jump" just toggles collision on for a few seconds.
        //(We can also actually make the jumper class where they can jump longer or something)         
       
        //These are the rigid bodyies for the player
        protected CapsuleCollider bodySpace; //The physical body of the player which will be textured
        protected SphereCollider vulnerabilitySpace; //this is basically the hitbox of the player

        //Gameplay elements 

        //Gamescore, win condition elements
        protected List<Merch> shoppingCart; //Current list of merch items collected 
        protected bool addMerchToCart(Merch addItem);//Mutator to add an item
        protected int cartSize; //Maximun number of items that can be placed within the cart
        protected int cartCounter; //Current max cart item slot used
        protected bool cartFull; //When the cart is full you cannot add any more (0 = not full, 1 = full)
        protected void merchPickup(Merch item); //the function which adds a merch item to the shopping cart and updates score.

        //consumable inventory related elements
        protected List<Consumable> inventory; //Inventory for consumable non-merch items, Current active item is in slot 0
        protected int invSize; //Maximum number of items that can be placed within the inventory
        protected bool invFull; //When the inventory is full you cannot add any more (0 = not full, 1 = full)     
        protected int invCounter; //current number of items 
        protected void collectablePickup(Collectable theItem); //function for mutating the collectable methodes
        protected void rotateInventory(); //rotates the inventory by 1 slot to the left (loops around) placing a new item in the active slot 



        //Constructors for player characters. Can be overwritten
        protected virtual PlayableCharacter(string Name, int HP, int maxHP, int cash, int goal, float moveSpeed, int cartS, int invS)
        {
            characterName = Name;
            healthPoint = HP;
            maxHealthPoint = maxHP;
            cashStore = cash;
            goalScore = goal;
            movementSpeed = moveSpeed;
            cartSize = cartS;
            invSize = invS;

            isDead = false;
            cartFull = false;
            invFull = false;

            cartCounter = 0;
            playerStatus = 0;

        }

        //Finalizer
        protected virtual ~PlayableCharacter()
        {
            inventory.Clear;
            shoppingCart.Clear;
        }

        /// <summary>
        /// HP Related Methodes Implementation
        /// </summary>

        //Overwriting the defult get and set functions to simultaniously update ifDead if dead
        protected int healthPoint
        {
            // get remains the same
            get
            {
                return healthPoint;
            }

            // Set automatically checks if if the person is dead
            set
            {
                if (value <= 0) {
                    healthPoint = 0;
                    isDead = true;
                    playerStatus = 4;
                    //move this to update 
                    //also we will deal with dropping of the loots
                }

                if (value >= maxHealthPoint)
                {
                    healthPoint = maxHealthPoint;
                }

                if(isDead == true && value > 0)
                {
                    healthPoint = value;
                    isDead = false;
                    playerStatus = 2;
                }

            }
        }

        //adds or subtracts health by a flat number
        protected void summationHealth(int deltaHP)
        {
            int temp = healthPoint;
            temp = temp + deltaHP;
            healthPoint = temp;
        }

        //Percent adjustment of HP increase (Max health)
        protected void percentHPChangeIncreaseMax(int deltaHpPercentUpMax)
        {
            int temp = healthPoint;
            temp = temp + maxHealthPoint * (1 + deltaHpPercentUpMax);
            healthPoint = temp;
        }

        //Percent adjustment of HP increase (Max Health)
        protected void percentHPChangeDecreaseMax(int deltaHpPercentDownMax)
        {
            int temp = healthPoint;
            temp = temp - maxHealthPoint * (deltaHpPercentUpMax);
            healthPoint = temp;
        }

        //Percent adjustment of HP increase (Current health)
        protected void percentHPChangeIncreaseCur(int deltaHpPercentUpCur)
        {
            int temp = healthPoint;
            temp = temp * (1 + deltaHpPercentUpCur);
            healthPoint = temp;
        }

        //Percent adjustment of HP increase (Current Health)
        protected void percentHPChangeDecreaseCur(int deltaHpPercentDownCur)
        {
            int temp = healthPoint;
            temp =temp * (deltaHpPercentDownCur);
            healthPoint = temp;
        }


        /// <summary>
        /// Movespeed mutators
        /// </summary>
   

        //adds or subtracts MS by a flat number
        protected void summationMS(int deltaMS)
        {
           float temp = movementSpeed;
            temp = temp + deltaMS;
            movementSpeed = deltaMS;
        }

        //Percent adjustment of MS increase (Current MS)
        protected void percentMSChangeIncreaseCur(int deltaMSPercentUpCur)
        {
            int temp = movementSpeed;
            temp = temp * (1 + deltaMSPercentUpCur);
            movementSpeed = temp;
        }

        //Percent adjustment of MS increase (Current MS)
        protected void percentMSChangeDecreaseCur(int deltaMSPercentDownCur)
        {
            int temp = movementSpeed;
            temp = temp * (1 - deltaMSPercentUpCur);
            movementSpeed = temp;
        }
        


        /// <summary>
        /// Cart Related Stuff
        /// </summary>

        //Update the CartCounter
        protected int CartCounter
        {
            get
            {
                return CartCounter;
            }
            set
            {
                if (value < 0) CartCounter = 0;
                else if (value >= cartSize)
                {
                    cartFull = true;
                    CartCounter = cartSize;
                }
                else if (value < cartSize)
                {
                    cartFull = false;
                }

            }
        }

        //Mutator to add an item
        protected bool addMerchToCart(Merch addItem)
        {

        }


        /// <summary>
        /// Consumable Inventory related stuff
        /// </summary>


        //function for mutating the collectable methodes
        protected void collectablePickup(Collectable theItem)
        {
            inventory.Add(theItem);
        }

        //rotates the inventory by 1 slot to the left (loops around) placing a new item in the active slot
        protected void rotateInventory()
        {
            
        }



        // Use this for initialization
        void Start()
        {
            isDead = false;
            cartFull = false;
            invFull = false;

            cartCounter = 0;
            playerStatus = 1;
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

