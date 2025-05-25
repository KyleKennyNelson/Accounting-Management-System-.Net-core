using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Dtos.LK_Dtos.LKACSoft_TaskDTO
{
    public class LKACSoft_TaskVisualizationDto
    {
        public required int Year { get; set; }
        public required int Quarter { get; set; } = 0;
        public required string Month1 { get; set; }
        public required string Month2 { get; set; }
        public required string Month3 { get; set; }
        public double RedoMonth1 { get; set; } = 0;
        public double RedoMonth2 { get; set; } = 0;
        public double RedoMonth3 { get; set; } = 0;
        public int AvgTaskAssignedPerUserMonth1 { get; set; } = 0;
        public int AvgTaskAssignedPerUserMonth2 { get; set; } = 0;
        public int AvgTaskAssignedPerUserMonth3 { get; set; } = 0;
    }
}
