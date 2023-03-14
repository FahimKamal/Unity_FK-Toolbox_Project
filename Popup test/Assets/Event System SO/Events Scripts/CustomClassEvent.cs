using System;
using UnityEngine;

namespace Events_Scripts
{
    [CreateAssetMenu(menuName = "Events/Custom Class Event")]
    public class CustomClassEvent : BaseEvent<DataClass>
    {
    
    }

    [Serializable]
    public class DataClass
    {
        public string name;
        public int money;
        public float health;
        public int damage;

        public DataClass(string name, int money, float health, int damage)
        {
            this.name = name;
            this.money = money;
            this.health = health;
            this.damage = damage;
        }
    }
}