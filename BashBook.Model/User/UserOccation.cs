namespace BashBook.Model.User
{
    public class UserOccationModel
    {
        public int UserOccationId { get; set; }
        public int UserId { get; set; }
        public int OccationId { get; set; }
        public long Date { get; set; }
        public int CreatedBy { get; set; }
    }

    public class UserOccationViewModel
    {
        public int UserOccationId { get; set; }
        public int OccationId { get; set; }
        public long Date { get; set; }
        public UserGeneralViewModel CreatedUserInfo { get; set; }
    }
}
