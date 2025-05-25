using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Dtos.LK_Dtos.LKACSoft_TaskDTO
{

    public class LKACSoft_TaskDashboardDto
    {
        public required int Year { get; set; }
        public required int? Quarter { get; set; } = 0;

        public List<string>? Months { get; set; } = new List<string>();

        public required List<StatsJsonDto>? statsJsonDto { get; set; } = new List<StatsJsonDto>();
    }

    public class StatsJsonDto
    {
        public string? Name { get; set; }
        public List<int>? AmountValuesIntType { get; set; } = new List<int>();
        public List<double>? AmountValuesDoubleType { get; set; } = new List<double>();
    }
}
