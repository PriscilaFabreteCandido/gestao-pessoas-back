namespace gestao_pessoas_back.Reponse
{
    public class SuccessResponse
    {
        public int Status { get; set; } = 200;

        public object Message { get; set; }

        public SuccessResponse(object message)
        {
            Message = message;
        }
    }
}
