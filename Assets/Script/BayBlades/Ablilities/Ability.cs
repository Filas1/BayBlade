using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public abstract class Ability
{
    [SerializeField] protected float CooldownInSeconds;
    protected bool CanBeUsed = true;
    /// <summary>
    /// 0 symbolises done
    /// </summary>
    protected float CooldownProgress;

    public virtual bool Use(IInteractor interactor)
    {
        PerformCooldown();
        return true;
    }

    protected async Task PerformCooldown()
    {
        CanBeUsed = false;
        CooldownProgress = CooldownInSeconds;
        while (CooldownProgress <= CooldownInSeconds)
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