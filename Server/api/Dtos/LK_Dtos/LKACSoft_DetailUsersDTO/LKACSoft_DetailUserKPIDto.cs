namespace api.Dtos.LK_Dtos.LKACSoft_DetailUsersDTO

{
    public class LKACSoft_DetailUserKPIDto
    {
        public required string UserID { get; set; }
        public int? InComplete { get; set; }
        public int? DoneOnTime { get; set; }
        public int? DoneBeforeDL { get; set; }
        public int? Late { get; set; }
    }
}
