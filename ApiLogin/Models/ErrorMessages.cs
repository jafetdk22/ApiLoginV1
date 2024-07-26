namespace ApiLogin.Models
{
    public class ErrorMessages
    {
        public string LoginError { get; set; }
        public string DatabaseError { get; set; }
        public string GeneralError { get; set; }
        public string InvalidLogin { get; set; }
        public string UserInactive { get; set; }
    }
}
