using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicMove : MonoBehaviour
{
    Animator myAnimator;
    Transform player;

    public bool move, rotate;
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        player = FindObjectOfType<Player>().transform;

        myAnimator.SetBool("move", move);
        myAnimator.SetBool("rotating", rotate);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
    }
}
