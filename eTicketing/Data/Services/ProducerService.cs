using eTicketing.Data.Base;
using eTicketing.Models;

namespace eTicketing.Data.Services
{
    public class ProducerService: EntityBaseRepository<Producer>, IProducerService
    {
        public ProducerService(AppDbcontext context): base(context) { }
        

        
    }
}
