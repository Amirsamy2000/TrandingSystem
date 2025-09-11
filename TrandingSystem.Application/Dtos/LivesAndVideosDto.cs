using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Application.Dtos
{
   public class LivesAndVideosDto
   {
       public List<LiveSessionsDto> Lives { get; set; }
        public List<VideoDto> Videos { get; set; }
    }
}
