﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_NPC : MonoBehaviour
{
    //private Transform tr;

    public Scr_Manager elManager; //pour pouvoir avoir acces au SCORE du Manager
    
    public int produit_1 = 0;
    public int produit_2 = 0;
    private int nbrProduitMax = 5 + 1;
    public bool besoinAtteint= false;
    public bool boolOnce;


    public Text NPCNeed;

    public float satisfactionPoint;
    public float satisfactionMaxPoint = 100f;
    public Slider satisfactionBar;

    public float score = 0f;
    public bool canGiveScore = true;

    
    public float moveSpeed = 50f;

   
    public  Transform[] waypoints; //tableau de waypoint dans Inspector
    private int waypointIndex = 0; //demarre au waypoint 0

    // Start is called before the first frame update
    void Start()
    {
        boolOnce = true;
        elManager = GameObject.Find("Manager").GetComponent<Scr_Manager>();
        //tr = this.transform;
        NeedCreator(); //appele la creation du besoin
        satisfactionPoint = satisfactionMaxPoint; //necessaire pour faire un % du slide de la satisfactionBar
        satisfactionBar.value = CalculBar();

        transform.position = waypoints[waypointIndex].transform.position; //fait apparaitre le NPC sur le 1er Waypoint 0
       
    }

    // Update is called once per frame
    void Update()
    {
        //UI des Besoins et Invetaire du NPC
        //NPCNeed.text = ("Potion : {0} / {1} n/Mask : {2} / {3}", currentProduit_1, produitBesoin_1, currentProduit_2, produitBesoin_2);
        //probleme de String a Int  

        if (transform.position == waypoints[1].transform.position && besoinAtteint == false)
        {
            transform.position = waypoints[1].transform.position;
        }
        else
        {
            Move();
        }
        
        //Satisfaction du NPC. Points qui descendent avec le temps
        satisfactionPoint -= 2f * Time.deltaTime; //ajustement si necessaire     
        satisfactionBar.value = CalculBar();

        if (produit_1 == 0 && produit_2 == 0)
        {
            elManager.score += this.satisfactionPoint;
            canGiveScore = false;
            besoinAtteint = true;
        }
        if (transform.position == waypoints[4].transform.position)
        {
            Destroy(this.gameObject);
        }


    }

    
    //Mes Fonctions
    void NeedCreator() //function pour creer le Randomizer de Besoin
    {
        produit_1 = Random.Range(0, nbrProduitMax);
        produit_2 = Random.Range(0, nbrProduitMax);
        
    }

    float CalculBar() //donne un pourcentage pour le Slider
    {
        return satisfactionPoint / satisfactionMaxPoint; 
    }

    void Move() //faire bouger le NPC sur les waypoints
    {

        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);
       
        if (transform.position == waypoints[waypointIndex].transform.position)
        {
            waypointIndex++;
        }

    }
    
}
