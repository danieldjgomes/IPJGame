using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bull : Obstacle
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
            if (GameUtils.Utility.IsEnoughDistance(this.gameObject,p.gameObject, 3* GameUtils.Utility.GetBlockSize(), true))
            {
                if (!this.owner.IsMyTeammate(p))
                {
                    owner.battle.DoDamage(this.owner.phisicalDamage, p, Battle.AttackType.SKILL);
                    Destroy(this.gameObject);
                }
            }
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Player player = collision.gameObject.GetComponent<Player>();
    //    print(collision.gameObject.transform.name);
    //    if (player)
    //    {
    //        if (!this.owner.IsMyTeammate(player))
    //        {
    //            owner.battle.DoDamage(this.owner.phisicalDamage, player, Battle.AttackType.SKILL);
    //            Destroy(this.gameObject);
    //        }
    //    }

    //}
}
