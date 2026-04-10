namespace GlassBoard.Response.Get
{
public class GetCheckResourceResultHttpResponse {
    public List<CheckResourceResultModel> Items { get; set; } = new();
}

public class CheckResourceResultModel {
    public string Id { get; set; }
    public string ResourceId { get; set; }
    public string CheckId { get; set; }
    public string CheckName { get; set; }
    public bool IsSuccess { get; set; }
    public string Result { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
}
}
