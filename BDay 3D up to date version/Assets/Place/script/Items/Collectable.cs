using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFriday 
{
    public abstract class Collectable : Item
    {
        protected PlayableCharacter holder;
        protected float pikcUpTimeInS;
        protected int usageTimes;
        // Use this for initialization
        

        // Update is called once per frame
        protected override void Update()
        { 
            if (usageTimes == 0) Destroy(this.gameObject);
        }

        protected abstract void pickUpItem(PlayableCharacter player);
        
        
    }
}
