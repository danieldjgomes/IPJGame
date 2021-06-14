using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    public Animator animator;
    public Text damageText;

    // Start is called before the first frame update
    void Start()
    {
        
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
        

    }

    public void SetText(string text)
    {
        damageText.text = text;
    }

}
