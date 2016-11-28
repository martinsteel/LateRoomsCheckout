namespace LateRoomsCheckout
{
    public interface ICheckout
    {
        bool Scan(string sku);
        int GetTotalPrice();
    }
}