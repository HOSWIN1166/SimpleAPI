using FirstSample01.API.Models.DomainAggregates;

namespace FirstSample01.API.Models.Contracts
{
    public interface IProductRepository<Tkey, TExist, TStatus> : IBaseRepository<Product?, Tkey, TExist, TStatus>
    {

    }
}
