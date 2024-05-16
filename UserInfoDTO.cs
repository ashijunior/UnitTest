namespace UnitPractical.DTO
{
    public class UserInfoDTO
    {
        public int ID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string nationality { get; set; }
        public string currentResidence { get; set; }
        public int idNumber { get; set; }
        public string dateOfBirth { get; set; }
        public string gender { get; set; }

        //Additional questions
        public string personalInfo { get; set; }
        public string gradYear { get; set; }
        public string multipleChoices { get; set; }
        public bool rejection { get; set; }
        public int yearExperience { get; set; }
        public int dateOfRelocation { get; set; }
    }
}
