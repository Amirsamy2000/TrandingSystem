using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrandingSystem.Domain.Abstractions
{
    public interface IDomainInterface<Domain>
    {
        Domain Create(Domain Object);
        List<Domain> Read();
        Domain ReadById(int Id);
        Domain Update(Domain Element);
        Domain Delete(int Id);
    }
}
