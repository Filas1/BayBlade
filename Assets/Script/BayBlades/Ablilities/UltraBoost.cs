using UnityEngine;
[System.Serializable]
public class UltraBoost : Ability, IRestriced
{
    [SerializeField] private UltraBoostData data;
    [SerializeField] private float speedBoost = 20f;

    protected override BaseAbilityData Data => data;
    public uint AmountOfUses => data.AmountOfUses;

    public override bool Use(IInteractor interactor)
    {
        if(!CanBeUsed) return false;
        
        interactor.ApplyBoost(interactor.Speed + speedBoost);
        return true;
    }

}