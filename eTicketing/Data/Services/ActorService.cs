using eTicketing.Data.Base;
using eTicketing.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicketing.Data.Services
{
    public class ActorService :EntityBaseRepository<Actor>, IActorService
    {
        //private readonly AppDbcontext _context;
        public ActorService(AppDbcontext context) : base(context) { }
        
        
    }
}
