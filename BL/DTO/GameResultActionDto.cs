using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTO
{
    public class GameResultActionDto
    {
        public DateTime GameTs { get; set; }
        public GameResultDTO GameResult { get; set; }

        public Boolean Accept { get; set; }
    }
}
