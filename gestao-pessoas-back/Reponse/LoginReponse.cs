namespace gestao_pessoas_back.Reponse
{
    public class LoginResponse
    {
        public string tokenType {  get; set; }
        public string AccessToken { get; set; }

        public int ExpiresIn { get; set; }
    }
}
