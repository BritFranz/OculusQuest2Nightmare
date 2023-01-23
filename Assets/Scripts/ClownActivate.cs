using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownActivate : MonoBehaviour
{
    [SerializeField] CollectorScript orbs;
    [SerializeField] GameObject ClownBox;
    [SerializeField] GameObject ClownTrigger;
    [SerializeField] GameObject ClownBoxOpen;
    public Animator anim;

   

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (null != orbs)
        {
            if (orbs.orbsCollected == 2)
            {
                ClownBox.SetActive(false);
                ClownTrigger.SetActive(true);
                ClownBoxOpen.SetActive(true);

                anim.SetBool("play", true);

            }
        }else
        {
            Debug.LogError( $"Orbs {orbs} = null." );
        }
    }
}
