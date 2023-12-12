public class HealthUiBar : UiBar
{
    public void UpdateHealthBar(float currentAmount, float maxAmount)
    {
        _bar.value = currentAmount / maxAmount;
    }
}
