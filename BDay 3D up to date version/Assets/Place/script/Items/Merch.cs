using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackFriday
{
    public class Merch : Collectable
    {
        //protected float mass;
        //protected CapsuleCollider physicalSpace;
        //protected Human holder;
        //protected float pikcUpTimeInS;
        //protected int usageTimes;
        protected float priceBeforeSaving;
        protected float priceAfterSaving;
        protected string merchName;


        protected override void Start()
        {
            throw new NotImplementedException();
        }
        protected override void pickUpItem (PlayableCharacter player){

        }

    }
}