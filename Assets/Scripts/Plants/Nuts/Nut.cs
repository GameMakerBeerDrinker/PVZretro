using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Nut : Plant
{
    public _Scripts.WalNutAnim walNutAnim;

    protected enum InjureState
    {
        Healthy,
        Injured,
        Dying,
    }

    protected InjureState injureState;

    private new void Start() {
        base.Start();
        walNutAnim.SetCurHpPercentage(100);
    }
    private new void FixedUpdate()
    {
        base.FixedUpdate();
        ChangeState();
        if(isTakingDamage)
        {
            walNutAnim.SetTakingDamageTrue();
            walNutAnim.SetCurHpPercentage((int)((float)currentHealth/maxHealth * 100f));
        }
        else
        {
            walNutAnim.SetTakingDamageFalse();
        }
    }

    private void ChangeState()
    {
        if(currentHealth>=maxHealth*2/3)
        {
            injureState = InjureState.Healthy;
        }
        else if(currentHealth>=maxHealth/3&&currentHealth<maxHealth*2/3)
        {
            injureState = InjureState.Injured;
        }
        else if(currentHealth<maxHealth/3)
        {
            injureState = InjureState.Dying;
        }
        
    }
}
