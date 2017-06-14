namespace ITMeat.BusinessLogic.Models
{
    public class EmailMessageModel : BaseModel
    {
        public string Recipient { get; set; }

        public string Message { get; set; }

        public string From { get; set; }

        public string Subject { get; set; }

        public int FailureCount { get; set; }

        public string FailError { get; set; }

        public string FailErrorMessage { get; set; }
    }
}