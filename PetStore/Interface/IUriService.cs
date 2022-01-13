using PetStore.Helpers;
using System;

namespace PetStore.Interface
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}
