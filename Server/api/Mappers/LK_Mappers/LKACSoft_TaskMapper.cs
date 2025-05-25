using api.Dtos.LK_Dtos.LKACSoft_TaskDTO;
using LKACSoftModel;

namespace api.Mappers.LK_Mappers
{
    public static class LKACSoft_TaskMapper
    {
        public static LKACSoft_TaskDashboardDto ToLKACSoft_AmountTaskStatusJsonDto(this LKACSoft_AmountTaskStatusDto LKACSoft_AmountTaskStatusDto)
        {
            double totalAmount = LKACSoft_AmountTaskStatusDto.DoneBeforeDL +
                         LKACSoft_AmountTaskStatusDto.DoneOnTime +
                         LKACSoft_AmountTaskStatusDto.Late +
                         LKACSoft_AmountTaskStatusDto.InComplete;
            return new LKACSoft_TaskDashboardDto
            {
                Year = LKACSoft_AmountTaskStatusDto.Year,
                Quarter = LKACSoft_AmountTaskStatusDto.Quarter,
                statsJsonDto = new List<StatsJsonDto>
                {
                    new StatsJsonDto
                    {
                        Name = "Hoàn thành trước hạn",
                        AmountValuesDoubleType = new List<double>
                        {
                            (double)LKACSoft_AmountTaskStatusDto.DoneBeforeDL / totalAmount * 100,
                        },
                    },
                    new StatsJsonDto
                    {
                        Name = "Hoàn thành đúng hạn",
                        AmountValuesDoubleType = new List<double>
                        {
                            (double)LKACSoft_AmountTaskStatusDto.DoneOnTime / totalAmount * 100,
                        },
                    },
                    new StatsJsonDto
                    {
                        Name = "Hoàn thành quá hạn",
                        AmountValuesDoubleType = new List<double>
                        {
                            (double) LKACSoft_AmountTaskStatusDto.Late / totalAmount * 100,
                        },
                    },
                    new StatsJsonDto
                    {
                        Name = "Chưa hoàn thành",
                        AmountValuesDoubleType = new List<double>
                        {
                            (double) LKACSoft_AmountTaskStatusDto.InComplete / totalAmount * 100,
                        },
                    },
                }
            };
        }

        public static LKACSoft_TaskDashboardDto ToLKACSoft_AmountRetriedTaskJsonDto(this LKACSoft_AmountRetriedTaskDto LKACSoft_AmountRetriedTaskDto)
        {
            return new LKACSoft_TaskDashboardDto
            {
                Year = LKACSoft_AmountRetriedTaskDto.Year,
                Quarter = LKACSoft_AmountRetriedTaskDto.Quarter,
                Months = new List<string>
                {
                    LKACSoft_AmountRetriedTaskDto.Month1,
                    LKACSoft_AmountRetriedTaskDto.Month2,
                    LKACSoft_AmountRetriedTaskDto.Month3,
                },
                statsJsonDto = new List<StatsJsonDto>
                {
                    new StatsJsonDto
                    {
                        Name = "IsRetried",
                        AmountValuesIntType = new List<int>
                        {
                            LKACSoft_AmountRetriedTaskDto.IsRetriedMonth1,
                            LKACSoft_AmountRetriedTaskDto.IsRetriedMonth2,
                            LKACSoft_AmountRetriedTaskDto.IsRetriedMonth3,
                        },
                    },
                    new StatsJsonDto
                    {
                        Name = "Perfect",
                        AmountValuesIntType = new List<int>
                        {
                            LKACSoft_AmountRetriedTaskDto.PerfectMonth1,
                            LKACSoft_AmountRetriedTaskDto.PerfectMonth2,
                            LKACSoft_AmountRetriedTaskDto.PerfectMonth3,
                        },
                    },
                },
            };
        }

        public static LKACSoft_TaskDashboardDto ToLKACSoft_TaskVisualizationJsonDto(this LKACSoft_TaskVisualizationDto LKACSoft_TaskVisualizationDto)
        {
            return new LKACSoft_TaskDashboardDto
            {
                Year = LKACSoft_TaskVisualizationDto.Year,
                Quarter = LKACSoft_TaskVisualizationDto.Quarter,
                Months = new List<string>
                {
                    LKACSoft_TaskVisualizationDto.Month1,
                    LKACSoft_TaskVisualizationDto.Month2,
                    LKACSoft_TaskVisualizationDto.Month3,
                },
                statsJsonDto = new List<StatsJsonDto>
                {
                    new StatsJsonDto
                    {
                        Name = "Số tác vụ",
                        AmountValuesIntType = new List<int>
                        {
                            LKACSoft_TaskVisualizationDto.AvgTaskAssignedPerUserMonth1,
                            LKACSoft_TaskVisualizationDto.AvgTaskAssignedPerUserMonth2,
                            LKACSoft_TaskVisualizationDto.AvgTaskAssignedPerUserMonth3,
                        },
                    },
                    new StatsJsonDto
                    {
                        Name = "Độ chính xác",
                        AmountValuesDoubleType = new List<double>
                        {
                            LKACSoft_TaskVisualizationDto.RedoMonth1,
                            LKACSoft_TaskVisualizationDto.RedoMonth1,
                            LKACSoft_TaskVisualizationDto.RedoMonth1,
                        },
                    },
                },
            };
        }

        public static LKACSoft_TaskDashboardDto ToLKACSoft_TaskAverageCompletionJsonDto(this LKACSoft_TaskAverageCompletionTimePerQuarterDto LKACSoft_TaskAverageCompletionTimePerQuarterDto)
        {
            return new LKACSoft_TaskDashboardDto
            {
                Year = LKACSoft_TaskAverageCompletionTimePerQuarterDto.Year,
                Quarter = LKACSoft_TaskAverageCompletionTimePerQuarterDto.Quarter,
                Months = new List<string>
                {
                    LKACSoft_TaskAverageCompletionTimePerQuarterDto.Month1,
                    LKACSoft_TaskAverageCompletionTimePerQuarterDto.Month2,
                    LKACSoft_TaskAverageCompletionTimePerQuarterDto.Month3,
                },
                statsJsonDto = new List<StatsJsonDto>
                {
                    new StatsJsonDto
                    {
                        Name = "Thời gian hoàn thành tác vụ trung bình",
                        AmountValuesDoubleType = new List<double>
                        {
                            LKACSoft_TaskAverageCompletionTimePerQuarterDto.AvgCompletionTimeInHoursMonth1,
                            LKACSoft_TaskAverageCompletionTimePerQuarterDto.AvgCompletionTimeInHoursMonth2,
                            LKACSoft_TaskAverageCompletionTimePerQuarterDto.AvgCompletionTimeInHoursMonth3,
                        },
                    },
                },
            };
        }

        public static LKACSoft_TaskDto ToLKACSoft_TaskDto(this LKACSoft_Task LKACSoft_Task)
        {
            var res = new LKACSoft_TaskDto
            {
                TaskID = LKACSoft_Task.TaskID,
                DateAssigned = LKACSoft_Task.DateAssigned,
                TaskDeadline = LKACSoft_Task.TaskDeadline,
                AssignedTo = LKACSoft_Task.AssignedTo,
                Title = LKACSoft_Task.Title,
                Detail = LKACSoft_Task.Detail,
                TaskStatusID = LKACSoft_Task.TaskStatusID,
                DateAccepted = LKACSoft_Task.DateAccepted,
                DateCompleted = LKACSoft_Task.DateCompleted,
                ReviewedBy = LKACSoft_Task.ReviewedBy,
                ReviewNote = LKACSoft_Task.ReviewNote,
                DateReview = LKACSoft_Task.DateReview,
                IsRetried = LKACSoft_Task.IsRetried,
                RelatedToExecution = LKACSoft_Task.RelatedToExecution,
                TaskType = LKACSoft_Task.TaskType,
                DesignatedNumberOfDocument = LKACSoft_Task.DesignatedNumberOfDocument,
                NumberOfCompletedDocument = LKACSoft_Task.NumberOfCompletedDocument,
                Priority = LKACSoft_Task.Priority,
                CreatedAt = LKACSoft_Task.CreatedAt
            };

            return res;
        }

        public static LKACSoft_Task CreateLKACSoft_TaskDto(this CreateLKACSoft_TaskDto CreateLKACSoft_Task)
        {
            return new LKACSoft_Task
            {
                //DateAssigned = CreateLKACSoft_Task.DateAssigned,
                TaskDeadline = CreateLKACSoft_Task.TaskDeadline,
                AssignedTo = CreateLKACSoft_Task.AssignedTo,
                Title = CreateLKACSoft_Task.Title,
                //Detail = CreateLKACSoft_Task.Detail,
                TaskStatusID = CreateLKACSoft_Task.TaskStatusID,
                //TaskStatusId = CreateLKACSoft_Task.TaskStatusId,
                //DateAccepted = CreateLKACSoft_Task.DateAccepted,
                //DateCompleted = CreateLKACSoft_Task.DateCompleted,
                //ReviewedBy = CreateLKACSoft_Task.ReviewedBy,
                //ReviewNote = CreateLKACSoft_Task.ReviewNote,
                //DateReview = CreateLKACSoft_Task.DateReview,
                //IsRetried = CreateLKACSoft_Task.IsRetried,
                RelatedToExecution = CreateLKACSoft_Task.RelatedToExecution,
                TaskType = CreateLKACSoft_Task.TaskType,
                //DesignatedNumberOfDocument = CreateLKACSoft_Task.DesignatedNumberOfDocument,
                //NumberOfCompletedDocument = CreateLKACSoft_Task.NumberOfCompletedDocument,
                Priority = CreateLKACSoft_Task.Priority,
                CreatedAt = CreateLKACSoft_Task.CreatedAt
            };
        }

        public static LKACSoft_Task UpdateLKACSoft_TaskDto(this UpdateLKACSoft_TaskDto UpdateLKACSoft_Task, string ID)
        {
            return new LKACSoft_Task
            {
                TaskID = ID,
                TaskDeadline = UpdateLKACSoft_Task.TaskDeadline,
                AssignedTo = UpdateLKACSoft_Task.AssignedTo,
                Title = UpdateLKACSoft_Task.Title,
                Detail = UpdateLKACSoft_Task.Detail,
                TaskStatusID = UpdateLKACSoft_Task.TaskStatusID,
                DateAccepted = UpdateLKACSoft_Task.DateAccepted,
                DateCompleted = UpdateLKACSoft_Task.DateCompleted,
                ReviewedBy = UpdateLKACSoft_Task.ReviewedBy,
                ReviewNote = UpdateLKACSoft_Task.ReviewNote,
                DateReview = UpdateLKACSoft_Task.DateReview,
                IsRetried = UpdateLKACSoft_Task.IsRetried,
                RelatedToExecution = UpdateLKACSoft_Task.RelatedToExecution,
                TaskType = UpdateLKACSoft_Task.TaskType,
                DesignatedNumberOfDocument = UpdateLKACSoft_Task.DesignatedNumberOfDocument,
                NumberOfCompletedDocument = UpdateLKACSoft_Task.NumberOfCompletedDocument,
                Priority = UpdateLKACSoft_Task.Priority
            };
        }


        public static LKACSoft_Task UpdateLKACSoft_Task_TaskStatusDto(this UpdateLKACSoft_Task_TaskStatusDto UpdateLKACSoft_Task_TaskStatus, string ID)
        {
            return new LKACSoft_Task
            {
                TaskID = ID,
                TaskStatusID = UpdateLKACSoft_Task_TaskStatus.TaskStatusID
            };
        }
    }
}
