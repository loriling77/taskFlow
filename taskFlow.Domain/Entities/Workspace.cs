
namespace taskFlow.Domain.Entities;

public class Workspace
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public DateTime CreatedAt { get; private set; }

    private readonly List<Project> _projects = [];

    public IReadOnlyCollection<Project> Projects =>
        _projects.AsReadOnly();

    // EF Core 使用的无参构造函数
    private Workspace()
    {
        Name = string.Empty;
    }

    // 创建 Workspace
    public Workspace(string name)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;

        Rename(name);
    }

    // 修改 Workspace 名称
    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Workspace name cannot be empty.",
                nameof(name));
        }

        string trimmedName = name.Trim();

        if (trimmedName.Length > 100)
        {
            throw new ArgumentException(
                "Workspace name cannot exceed 100 characters.",
                nameof(name));
        }

        Name = trimmedName;
    }
}
