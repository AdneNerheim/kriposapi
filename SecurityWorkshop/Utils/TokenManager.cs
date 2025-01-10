namespace SecurityWorkshop.Utils;

public class TokenManager
{
    private readonly string validToken = "SuperDuperSecretTokenIfThisLeaksWeAreFucked";

    public bool ValidateToken(string token)
    {
        return token == validToken;
    }

    public string getToken()
    {
        return validToken;
    }
}