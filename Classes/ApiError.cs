namespace gravimetry_api.Classes
{
  public class ApiError
  {
    public ApiError(string[] messages)
    {
      this.Messages = messages;
    }
    public string[] Messages { get; set; }
  }
}
