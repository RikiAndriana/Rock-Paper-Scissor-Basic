using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public Cards chosenCard;
    public Transform atkPosRef;
    private Tweener animationTweener;
    public float Health;
    public float MaxHealt;
    public TMP_Text healtText;
    public HealthBar healthBar;
    public AudioSource audioSource;
    public AudioClip damageClip;

    private void Start()
    {
        Health = MaxHealt;
    }
    public Attack? AttackValue
    {
        get => chosenCard == null ? null : chosenCard.AttackValue;
        // get
        // {
        //     if (chosenCard == null)
        //     {
        //         return null;
        //     }
        //     else
        //     {
        //         return chosenCard.AttackValue;
        //     }
        // }
    }
    public void Reset()
    {
        if (chosenCard != null)
        {
            chosenCard.Reset();
        }
        chosenCard = null;
    }
    public void SetChosenCard(Cards newCard)
    {
        if (chosenCard != null)
        {
            chosenCard.Reset();
        }
        chosenCard = newCard;
        chosenCard.transform.DOScale(chosenCard.transform.localScale * 1.2f, 0.2f);
    }

    public void ChangeHealt(float amount)
    {
        Health += amount;
        Health = Mathf.Clamp(Health, 0, 100);
        //healtbar
        healthBar.UpdateBar(Health / MaxHealt);
        //text
        healtText.text = Health + "/" + MaxHealt;

    }




    public void AnimateAttack()
    {
        animationTweener = chosenCard.transform
            .DOMove(atkPosRef.position, 0.5f);

    }


    public void AnimateDamage()
    {
        audioSource.PlayOneShot(damageClip);
        var image = chosenCard.GetComponent<Image>();
        animationTweener = image
            .DOColor(Color.red, 0.1f)
            .SetLoops(3, LoopType.Yoyo)
            .SetDelay(0.2f);


    }
    public void AnimateDraw()
    {
        animationTweener = chosenCard.transform
            .DOMove(chosenCard.OriginalPosition, 1)
            .SetDelay(0.2f)
            .SetEase(Ease.InBack);
    }



    public bool IsAnimating()
    {
        return animationTweener.IsActive();
    }
    public void isClickable(bool value)
    {
        Cards[] cards = GetComponentsInChildren<Cards>();
        foreach (var card in cards)
        {
            card.SetClickable(value);
        }
    }
}
