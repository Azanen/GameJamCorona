using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ItemManagement : MonoBehaviour
{
    public Scr_Manager manager;
    public bool pickUpAllowed = false;
    public Scr_NPC npc;
    public bool dropOffAllowed = false;
    public bool servi = false;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<Scr_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pickUpAllowed && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }
        if (pickUpAllowed && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
        if (dropOffAllowed && Input.GetKeyDown(KeyCode.Q))
        {
            Give();
        }
        if (npc != null && npc.produit_1 == 0 && npc.produit_2 == 0)
        {
            servi = true;
            npc.besoinAtteint = true;
        }
        if (npc != null && npc.transform.position == npc.waypoints[3].transform.position)
        {
            if (npc.boolOnce && npc.name == "oto_NPC")
            {
                manager.SpawnNPC();
                npc.boolOnce = false;
                //Destroy(npc);
            }
            if (npc.boolOnce && npc.name == "testNPC")
            {
                manager.SpawnNPC2();
                npc.boolOnce = false;
            }
        }

    }
    // Fonctions
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Avatar") && !this.CompareTag("DropOff"))
        {
            pickUpAllowed = true;
        }
        if (other.CompareTag("Avatar") && this.CompareTag("DropOff"))
        {
            //manager.SpawnNPC();
            
            dropOffAllowed = true;
            
        }
        if (other.CompareTag("NPC"))
        {
            Scr_NPC newNpc = other.gameObject.GetComponent<Scr_NPC>();
            npc = newNpc;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        pickUpAllowed = false;
        dropOffAllowed = false;
        
    }
    private void PickUp()
    {
        if (this.CompareTag("PotionShelf") && manager.total < 5)
        {
            manager.potionNbr++;
            manager.total++;
           
        }
        if (this.CompareTag("MaskShelf") && manager.total < 5)
        {
            manager.maskNbr++;
            manager.total++;
        }
    }
    /*private void OnTriggerStay2D(Collider2D otherNpc)
    {
        Scr_NPC newNpc = otherNpc.gameObject.GetComponent<Scr_NPC>();
        npc = newNpc;
    }*/
    private void Drop()
    {
        if (this.CompareTag("PotionShelf") && manager.potionNbr >= 1)
        {
            manager.potionNbr--;
            manager.total--;
        }
        if (this.CompareTag("MaskShelf") && manager.maskNbr >= 1)
        {
            manager.maskNbr--;
            manager.total--;
        }
    }
    private void Give()
    {
        if (this.CompareTag("DropOff") && npc.produit_1 >= manager.potionNbr)
        {
            npc.produit_1 -= manager.potionNbr;
            manager.total -= manager.potionNbr;
            manager.potionNbr -= manager.potionNbr;
        }
        if (this.CompareTag("DropOff") && npc.produit_1 <= manager.potionNbr)
        {
            manager.total -= npc.produit_1;
            manager.potionNbr -= npc.produit_1;
            npc.produit_1 = 0;
        }
        if (this.CompareTag("DropOff") && npc.produit_2 >= manager.maskNbr)
        {
            npc.produit_2 -= manager.maskNbr;
            manager.total -= manager.maskNbr;
            manager.maskNbr -= manager.maskNbr;
        }
        if (this.CompareTag("DropOff") && npc.produit_2 <= manager.maskNbr)
        {
            manager.total -= npc.produit_2;
            manager.maskNbr -= npc.produit_2;
            npc.produit_2 = 0;
        }
    }
    
}
