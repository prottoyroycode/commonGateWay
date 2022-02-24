using Bkash_Service_API.Core.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Core
{
    public interface IUnitOfWork
    {
        IApplicationUserRepository ApplicationUserRepository { get; }
        Task CompleteAsync();

    }
}
