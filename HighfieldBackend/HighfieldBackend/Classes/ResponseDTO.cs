using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HighfieldBackend.Classes
{
    public class ResponseDTO
    {
        public List<UserEntity> users { get; set; }
        public List<AgePlusTwentyDTO> ages { get; set; }
        public List<TopColoursDTO> topColours { get; set; }
    }
}