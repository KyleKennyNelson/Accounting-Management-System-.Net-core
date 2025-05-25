using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Dtos.LK_Dtos.LKACSoft_TaskDTO
{
    public class LKACSoft_AmountTaskStatusDto
    {
        public required int Year { get; set; }
        public required int Quarter { get; set; } = 0;

        public int InComplete { get; set; } = 0;
        public int DoneOnTime { get; set; } = 0;
        public int DoneBeforeDL { get; set; } = 0;
        public int Late { get; set; } = 0;
    }
}
