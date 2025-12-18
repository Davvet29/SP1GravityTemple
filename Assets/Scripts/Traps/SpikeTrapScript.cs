using System;
using UnityEngine;

public class SpikeTrapScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    

    

    public void ActivateTrap()
    {
        animator?.Play("SpikeTrapActive");
    }
}
