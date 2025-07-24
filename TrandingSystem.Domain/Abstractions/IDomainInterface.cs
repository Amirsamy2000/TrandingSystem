using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Shared.Response;

namespace TrandingSystem.Domain.Abstractions
{
    internal interface IDomainInterface<Domain,DTO>
    {
        Response<List<DTO>> Read();
        Response<DTO> ReadById(int Id);
        Response<DTO> Create(DTO Object);
        Response<DTO> Update(DTO Element);
        Response<DTO> Delete(int Id);
    }
}
