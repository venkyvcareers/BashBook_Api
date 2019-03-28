namespace BashBook.Model.Global
{
    public class EntityPreviewModel
    {
        public int EntityTypeId { get; set; }
        public int EntityId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Message { get; set; }
        public LastMessageModel LastMessage { get; set; }
    }

    public class LastMessageModel
    {
        public int ContentTypeId { get; set; }
        public string Text { get; set; }
        public long PostedOn { get; set; }
        public string PostedBy { get; set; }
    }
}
