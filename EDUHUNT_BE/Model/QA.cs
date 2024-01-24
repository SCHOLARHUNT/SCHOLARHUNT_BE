namespace EDUHUNT_BE.Model
{
    public class QA
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid askerId { get; set; }
        public Guid answerId { get; set; }

        public string question { get; set; }
        public string answer { get; set; }

        public DateTime SentAt { get; set; } = DateTime.UtcNow;

    }
}
