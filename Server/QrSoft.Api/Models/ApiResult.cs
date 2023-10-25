namespace QrSoft.Api.Models;


public class ApiErrorResult
{
    public object model { set; get; }
    public string? message { set; get; }

 
    public ApiErrorResult(string message,object model)
    {
        this.message = message;
        this.model = model;
    }


}


public class ApiSuccessResult
{
    public object model { set; get; }

    public ApiSuccessResult(object model)
    {
        this.model = model;
    }

}
