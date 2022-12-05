
public class PlayerHealth : IDamagable
{
    public PlayerHealth(int health)
    {
        Value = health;
    }

    public int Value { get; private set; }
    public void TakeDamage(IDamageDealer damageDealer)
    {
        Value -= damageDealer.GetDamage();
        if (Value < 0) Value = 0;
    }
}
