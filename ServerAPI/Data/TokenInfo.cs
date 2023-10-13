namespace ServerAPI.Data
{
    public class TokenInfo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
