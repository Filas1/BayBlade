using UnityEngine;
[System.Serializable]
public class UltraBoost : Ability
{
    [SerializeField] private float speedBoost = 20f;
    public override bool Use(IInteractor interactor)
    {
        if(!CanBeUsed) return false;
        
        interactor.ApplyBoost(interactor.Speed + speedBoost);
        return true;
    }
}