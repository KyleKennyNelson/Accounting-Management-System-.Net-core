using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Dtos.LK_Dtos.LKACSoft_TaskDTO
{
    public class LKACSoft_AmountRetriedTaskDto
    {
        public required int Year { get; set; }
        public required int Quarter { get; set; } = 0;

        public required string Month1 { get; set; }
        public required string Month2 { get; set; }
        public required string Month3 { get; set; }

        public int IsRetriedMonth1 { get; set; } = 0;
        public int PerfectMonth1 { get; set; } = 0;

        public int IsRetriedMonth2 { get; set; } = 0;
        public int PerfectMonth2 { get; set; } = 0;

        public int IsRetriedMonth3 { get; set; } = 0;
        public int PerfectMonth3 { get; set; } = 0;
    }
}
