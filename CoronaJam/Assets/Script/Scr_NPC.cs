using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scr_NPC : MonoBehaviour
{
    //private Transform tr;

    public Scr_Manager elManager; //pour pouvoir avoir acces au SCORE du Manager
    
    public int produit_1 = 0; //potions
    public int produit_2 = 0; //masks
    private int nbrProduitMax = 5 + 1;
    public bool besoinAtteint= false;
    public bool boolOnce;


    //public Text NPCNeed;

    public float satisfactionPoint;
    public float satisfactionMaxPoint = 100;
    public Slider satisfactionBar;
    public bool countdownActive = true;

    public float score = 0f;
    public bool canGiveScore = true;

    
    public float moveSpeed = 50f;

   
    public  Transform[] waypoints; //tableau de waypoint dans Inspector
    private int waypointIndex = 0; //demarre au waypoint 0

    public TextMeshProUGUI needing;

    public Animator animatorNPC;

    // Start is called before the first frame update
    void Start()
    {
        animatorNPC = this.GetComponent<Animator>();
        needing = this.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        boolOnce = true;
        elManager = GameObject.Find("Manager").GetComponent<Scr_Manager>();
        //tr = this.transform;
        NeedCreator(); //appele la creation du besoin
        satisfactionPoint = satisfactionMaxPoint; //necessaire pour faire un % du slide de la satisfactionBar
        satisfactionBar.value = CalculBar();

        transform.position = waypoints[waypointIndex].transform.position; //fait apparaitre le NPC sur le 1er Waypoint 0
       // animatorNPC.SetBool("walkingRight", true);

    }

    // Update is called once per frame
    void Update()
    {
        needing.text = "I need " + produit_1.ToString() + " potions and " + produit_2.ToString() + " masks";
        if (produit_1 == 0 && produit_2 == 0 && canGiveScore)
        {
            elManager.score += satisfactionPoint;
            canGiveScore = false;
           
        }

        if (transform.position == waypoints[1].transform.position && besoinAtteint == false)
        {
            transform.position = waypoints[1].transform.position;
        }
        else
        {
            Move();
        }

        //Satisfaction du NPC. Points qui descendent avec le temps
        if (produit_1 == 0 && produit_2 == 0) // si besoin est atteint...
        {
            needing.text = "Thank you!";
            besoinAtteint = true;
            countdownActive = false;
            if (waypoints[waypointIndex].name == "Waypoint (2)")
            {
                animatorNPC.SetBool("walkingDown", true);
            }
            if (waypoints[waypointIndex].name == "WP2")
            {
                animatorNPC.SetBool("walkingDown", true);
            }

        }
        if (transform.position == waypoints[4].transform.position) //va se deplacer hors de l'ecran
        {
            Destroy(this.gameObject);
        }

        if (countdownActive) //si besoin n'est pas atteint
        {
            satisfactionPoint -= 2f * Time.deltaTime; //son taux de satisfaction descend. Ajustement si nessaire
            satisfactionBar.value = CalculBar(); //indique a la barre Slider comment descendre
        }
        

        if (satisfactionPoint <= 0) // si attends trop longtemps...
        {
            countdownActive = false;
            satisfactionPoint = 0;
            needing.text = "Way too long... I'm losing my time here!";
            Move();
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
            // left lane
            if (waypoints[waypointIndex].name == "Waypoint")
            {
                animatorNPC.SetBool("walkingRight", true);
            }
            if (waypoints[waypointIndex].name == "Waypoint (1)")
            {
                animatorNPC.SetBool("walkingRight", false);
            }

            if (waypoints[waypointIndex].name == "Waypoint (2)")
            {
                animatorNPC.SetBool("walkingDown", false);

                animatorNPC.SetBool("walkingLeft", true);
            }
            //right lane
            if (waypoints[waypointIndex].name == "WP")
            {
                animatorNPC.SetBool("walkingLeft", true);
            }
            if (waypoints[waypointIndex].name == "WP1")
            {
                animatorNPC.SetBool("walkingLeft", false);
            }

            if (waypoints[waypointIndex].name == "WP2")
            {
                animatorNPC.SetBool("walkingDown", false);

                animatorNPC.SetBool("walkingRight", true);
            }

            waypointIndex++;


        }

    }
    
}
