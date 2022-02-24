using AutoMapper;
using Bkash_Service_API.Core;
using Bkash_Service_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public readonly DataContext _context;
        public readonly IMapper _mapper;
        public IApplicationUserRepository ApplicationUserRepository { get; private set; }
        public UnitOfWork(DataContext context ,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            ApplicationUserRepository = new ApplicationUserRepository(_context ,mapper);
        }
        

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
            
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
