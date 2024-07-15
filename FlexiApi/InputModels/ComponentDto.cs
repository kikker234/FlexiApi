using System.Text;

namespace FlexiApi.InputModels;

public class ComponentDto
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public Dictionary<string, string> Fields { get; set; }
    
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Name: " + Name + "\n");
        sb.Append("Type: " + Type + "\n");
        sb.Append("Description: " + Description + "\n");
        sb.Append("Fields: \n");
        foreach (var field in Fields)
        {
            sb.Append("\t" + field.Key + ": " + field.Value + "\n");
        }
        return sb.ToString();
    }
}