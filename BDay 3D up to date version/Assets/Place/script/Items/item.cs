using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackFriday
{
    public abstract class Item : MonoBehaviour
    {
        protected float mass;
        protected CapsuleCollider physicalSpace;

        // Use this for initialization
        protected abstract void Start();

        // Update is called once per frame
        protected abstract void Update();
    }




}