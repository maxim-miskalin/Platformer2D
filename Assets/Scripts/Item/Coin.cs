public class Coin : Item
{
    public override float ToDestroyed()
    {
        Disappear.Invoke(this);
        Destroy(gameObject);
        return Value;
    }
}
