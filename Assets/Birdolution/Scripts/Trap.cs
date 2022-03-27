using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private Animator anim;

    public void PlayTrap()
    {
        anim?.SetTrigger("trap");
    }
}
