using System.Collections.Generic;
using UnityEngine;

public class Player_Data
//can add puzzle, scene/position.
{

    [SerializeField]
    public class Inventorydata
    {
        public string itemname;
        public int quantity;
        public string descritption;
    }
    //elemetns of inventory from inventory system.
    public List<Inventorydata> inventorySlots = new List<Inventorydata>();
}
