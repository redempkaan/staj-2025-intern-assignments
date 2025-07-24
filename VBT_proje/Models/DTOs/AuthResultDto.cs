namespace VBT_proje.Models.DTOs
{
    // Defining a DTO which represents the result of an authentication operation
    public class AuthResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}