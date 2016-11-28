namespace LateRoomsCheckout
{

    public interface IProductRepository
    {
        Product Get(string sku);
    }
}
