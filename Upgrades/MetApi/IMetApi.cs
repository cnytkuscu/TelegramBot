using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Upgrades.Models;

namespace Upgrades.MetApi
{
    public interface IMetApi
    {
        Task<CollectionObjects> GetCollectionObjectsAsync();
        Task<CollectionItem> GetCollectionItemAsync(string objectNum);
        Task<CollectionObjects> SearchCollectionAsync(string query);
    }
}
