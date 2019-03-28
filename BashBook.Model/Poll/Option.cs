namespace BashBook.Model.Poll
{
    public class OptionModel
    {
        public int OptionId { get; set; }
        public int PollId { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
    }

    public class OptionResponseModel
    {
        public int OptionId { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public int Count { get; set; }
    }

    public class OptionCountModel
    {
        public int OptionId { get; set; }
        public int Count { get; set; }
    }
}
