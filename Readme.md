# LateRooms Checkout Kata by Martin Steel

This is an implementation of the [LateRooms Checkout Kata](https://github.com/LateRoomsGroup/interview-katas/blob/master/checkout.md) by [Martin Steel](http://martinsteel.co.uk)

There is one public interface fo the Checkout, ```ICheckout``` defined below.

``` c#
public interface ICheckout
{
    bool Scan(string sku);
    int GetTotalPrice();
}
```

### Public methods

`Scan(string sku)` Add an item to be purchased identified by its Stock Keeping Unit (SKU)

`GetTotalPrice()` Once all items are added get the total cost of the items being purchased.

### SKUs and prices used in tests (from the Kata description)

| SKU  | Unit Price | Special Price |
| ---- | ---------- | ------------- |
| A    | 50         | 3 for 130     |
| B    | 30         | 2 for 45      |
| C    | 20         |               |
| D    | 15         |               |

## Implementation

A concrete implementation of the `ICheckout` interface can be found in the `Checkout` class. The public constructor requires an `IProductRepository` as an argument. The `IProductRepository` interface defines a public interface for retrieving products by SKU. In production I would expect this to be a products database or equivalent. 

## Testing

The `CheckoutTests` project uses [NUnit](https://www.nunit.org/) to test the `Checkout` implementation.

For testing purposes the `IProductRepository` interface is mocked with [Moq](https://github.com/moq/moq4) in the test SetUp function. The mocked implementation supports the four SKUs defined in the Kata. 

## Requirements

* Tested in Visual Studio 2017 RC2 (should also work in earlier versions)
* Requires .NET 4.6.2
* NuGet 3 (Package restore will install NUnit)
