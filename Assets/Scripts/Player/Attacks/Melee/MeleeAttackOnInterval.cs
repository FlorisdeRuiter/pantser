public class MeleeAttackOnInterval : AMeleeAttack
{
    protected override void Update()
    {
        base.Update();

        if (_timeUntilAttack > 0)
            return;

        Attack();
    }
}
