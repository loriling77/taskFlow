using System;

namespace taskFlow.Domain.Entities;

public class Project
{
    public Guid Id { get; private set; }
    public Guid WorkspaceId { get; private set; }
    public string Name { get; private set; } = string.Empty;

    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private readonly List<TaskItem> _tasks = [];

    public IReadOnlyCollection<TaskItem> Tasks => _tasks.AsReadOnly();

    public Project(Guid workspaceId, string name, string? description)
    {

        if (workspaceId == Guid.Empty)
        {
            throw new ArgumentException("workspaceId cannot be empty.", nameof(workspaceId));
        }
        Id = Guid.NewGuid();
        WorkspaceId = workspaceId;
        CreatedAt = DateTime.UtcNow;
        Rename(name);
        UpdateDescription(description);
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Project name cannot be empty", nameof(name)
            );
        }
        ;


        string trimmedName = name.Trim();

        if (trimmedName.Length > 150)
        {
            throw new ArgumentException(
                "Project name cannot exceed 150 characters", nameof(name)
            );
        }
        ;
        Name = trimmedName;
    }

    public void UpdateDescription(string? description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            Description = null;
            return;
        }
        string trimmedDescription = description.Trim();


        if (trimmedDescription.Length > 300)
        {
            throw new ArgumentException(
                "Project description cannot exceed 300 characters.", nameof(description)
            );
        }
            ;

        Description = trimmedDescription;



    }



}
