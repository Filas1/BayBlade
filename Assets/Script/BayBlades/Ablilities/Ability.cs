using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public abstract class Ability
{
    protected abstract BaseAbilityData Data { get; }
    protected bool CanBeUsed = true;
    /// <summary>
    /// 0 symbolises done
    /// </summary>
    protected float CooldownProgress;

    public abstract bool Use(IInteractor interactor);

    protected async Task PerformCooldown()
    {
        CanBeUsed = false;
        CooldownProgress = Data.CooldownInSeconds;
        while (CooldownProgress <= Data.CooldownInSeconds)
        {
            CooldownProgress -= Time.deltaTime;
            await Task.Yield();
        }
        CanBeUsed = true;
        CooldownProgress = 0;
    }
}
public interface IRestriced
{
    public uint AmountOfUses { get; }
}
public interface IInteractor
{
    public float Speed { get; }
    public void ApplyBoost(float boostAmount);
}