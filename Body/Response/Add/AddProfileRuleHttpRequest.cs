namespace GlassBoard.Request.Add
{
    public class AddProfileRuleHttpRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public MatchingRuleSet MatchingRuleSet { get; set; } = new();
        public ProfileSet ProfileSet { get; set; } = new();
        public int Order { get; set; }
        public List<ChildProfileRule> ChildrenProfileRules { get; set; } = new();
    }

    public class MatchingRuleSet
    {
        public List<string> ResourceTypes { get; set; } = new();
        public string ResourceName { get; set; }
        public List<AttributeRule> Attributes { get; set; } = new();
    }

    public class AttributeRule
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class ProfileSet
    {
        public string CollectionProfileId { get; set; }
        public string CheckProfileId { get; set; }
        public string AlertProfileId { get; set; }
    }

    public class ChildProfileRule
    {
        public MatchingRuleSet MatchingRuleSet { get; set; } = new();
        public ProfileSet ProfileSet { get; set; } = new();
    }
}