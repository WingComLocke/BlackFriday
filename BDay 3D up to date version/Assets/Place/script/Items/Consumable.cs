using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackFriday
{
    public class Consumable : Collectable
    {
        //protected float mass;
        //protected CapsuleCollider physicalSpace;
        //protected Human holder;
        //protected float pikcUpTimeInS;
        //protected int usageTimes;
        protected float duration;//istant item will have duration 0


        private void OnTriggerEnter(Collider other)
        {
            
        }
        protected override void Start()
        {
            ;
        }
        protected override void pickUpItem(PlayableCharacter player)
        {
            ;
        }
        protected void useItem(){
           ;
        }
    }
}