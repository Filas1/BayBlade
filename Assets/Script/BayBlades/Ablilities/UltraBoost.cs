using UnityEngine;
[System.Serializable]
public class UltraBoost : Ability, IRestriced
{
    [SerializeField] private UltraBoostData data;

    protected override BaseAbilityData Data => data;
    public uint AmountOfUses => data.AmountOfUses;

    public override bool Use(IInteractor interactor)
    {
        if(!CanBeUsed) return false;
        
        interactor.ApplyBoost(interactor.Speed + data.SpeedBoost);
        return true;
    }

}