using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Domain.Abstractions;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Domain.Interfaces
{
    public interface IVideoRepository:IDomainInterface<Video>
    {
         IQueryable<Video> GetAllVideosForCouse(int CousreId);
       

    }
}
