using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//**Auteur Maxime Galarneau 27/03/2020**//

public class Scr_Avatar : MonoBehaviour
{
    private Transform tr;
    //private int Speed = 5;
    public Animator animatorAvatar;
    public bool canMove = true;
    public LayerMask ground;
    public bool inCollision;
    public float rayon = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        tr = this.transform;
        animatorAvatar = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inCollision = Physics2D.OverlapCircle(tr.position, rayon, ground);
        if (!inCollision)
        {
            tr.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f) * Time.deltaTime * 100f, Space.World); //hori AD; verti WS, pas de Z, Time.deltaTime * Vitesse ajustable

            if (Input.GetAxisRaw("Vertical") > 0.0f)
            {
                animatorAvatar.SetBool("isGoingLeft", false);
                animatorAvatar.SetBool("isGoingRight", false);
                animatorAvatar.SetBool("isGoingDown", false);
                animatorAvatar.SetBool("isGoingUp", true);
            }
            if (Input.GetAxisRaw("Vertical") < 0.0f)
            {
                animatorAvatar.SetBool("isGoingLeft", false);
                animatorAvatar.SetBool("isGoingRight", false);
                animatorAvatar.SetBool("isGoingUp", false);
                animatorAvatar.SetBool("isGoingDown", true);
            }

            if (Input.GetAxisRaw("Horizontal") > 0.0f)
            {
                animatorAvatar.SetBool("isGoingUp", false);
                animatorAvatar.SetBool("isGoingDown", false);
                animatorAvatar.SetBool("isGoingLeft", false);
                animatorAvatar.SetBool("isGoingRight", true);
            }
            if (Input.GetAxisRaw("Horizontal") < 0.0f)
            {
                animatorAvatar.SetBool("isGoingUp", false);
                animatorAvatar.SetBool("isGoingDown", false);
                animatorAvatar.SetBool("isGoingRight", false);
                animatorAvatar.SetBool("isGoingLeft", true);
            }

            if (Input.GetAxisRaw("Horizontal") == 0.0f && Input.GetAxis("Vertical") == 0.0f)
            {
                animatorAvatar.SetBool("isGoingUp", false);
                animatorAvatar.SetBool("isGoingDown", false);
                animatorAvatar.SetBool("isGoingRight", false);
                animatorAvatar.SetBool("isGoingLeft", false);
            }
        }
    }
}
