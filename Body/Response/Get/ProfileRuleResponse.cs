using GlassBoard.Request.Add;

public class ProfileRuleResponse
{
    public int Offset { get; set; }
    public int Limit { get; set; }
    public int TotalCount { get; set; }
    public List<ProfileRuleItem> Items { get; set; } = new();
}

public class ProfileRuleItem
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Order { get; set; }
    public MatchingRuleSet MatchingRuleSet { get; set; }
    public ProfileSet ProfileSet { get; set; }
    public List<ChildProfileRule> ChildrenProfileRules { get; set; } = new();
    public DateTime ModifiedOn { get; set; }
}