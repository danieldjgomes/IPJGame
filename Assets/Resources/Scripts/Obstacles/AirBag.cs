using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AirBag : Obstacle
{
    
   
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Player[] players = FindObjectsOfType<Player>();
        foreach (Player p in players)
        {
            if (p.transform.position.x == this.transform.position.x && p.transform.position.z == this.transform.position.z)
            {
                if (!this.owner.IsMyTeammate(p))
                {
                    owner.battle.DoDamage(this.owner.phisicalDamage, p, Battle.AttackType.SKILL);
                    Destroy(this.gameObject);
                }
            }
        }
    }

   
    }
