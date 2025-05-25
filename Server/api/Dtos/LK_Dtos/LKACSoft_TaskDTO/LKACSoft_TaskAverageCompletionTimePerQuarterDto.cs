using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Dtos.LK_Dtos.LKACSoft_TaskDTO
{
    public class LKACSoft_TaskAverageCompletionTimePerQuarterDto
    {
        public required int Year { get; set; }
        public int? Quarter { get; set; } = 0;
        public required string Month1 { get; set; }
        public required string Month2 { get; set; }
        public required string Month3 { get; set; }
        public double AvgCompletionTimeInHoursMonth1 { get; set; } = 0;
        public double AvgCompletionTimeInHoursMonth2 { get; set; } = 0;
        public double AvgCompletionTimeInHoursMonth3 { get; set; } = 0;
    }
}
