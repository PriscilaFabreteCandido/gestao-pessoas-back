namespace gestao_pessoas_back.Exceptions;

public class BusinessException : Exception
{
    public BusinessException(string message) : base(message) { }   
}