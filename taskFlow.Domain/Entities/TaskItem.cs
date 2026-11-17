using taskFlow.Domain.Enums;


namespace taskFlow.Domain.Entities;

public class TaskItem
{
    public Guid Id { get; private set; }
    public Guid ProjectId { get; private set; }
    public string Title { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public TaskItemPriority Priority { get; private set; } = TaskItemPriority.Low;

    public TaskItemStatus Status { get; private set; } = TaskItemStatus.Todo;

    public int Progress { get; private set; }

    public DateTime? DueDate { get; private set; }

    public TaskItem(Guid projectId, string title, string? description)
    {
        if (projectId == Guid.Empty)
        {
            throw new ArgumentException("Prject Id  cannot be empty.", nameof(projectId));
        }
        Id = Guid.NewGuid();
        ProjectId = projectId;
        CreatedAt = DateTime.UtcNow;
        Status = TaskItemStatus.Pending;
        Priority = TaskItemPriority.Low;
        Progress = 0;
        DueDate = DateTime.UtcNow;
        Rename(title);
        UpdateDescription(description);

    }

    public void Rename(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException(
              "Task name cannot be empty.", nameof(title)
            );
        }
        string trimmedName = title.Trim();
        if (trimmedName.Length > 100)
        {
            throw new ArgumentException("Name cannot exceed 100 characters", nameof(title));
        }
        ;

        Title = trimmedName;

    }
    public void UpdateDescription(string description)
    {

        if (string.IsNullOrWhiteSpace(description))
        {
            Description = null;
            return;
        }
        string trimmedDescription = description.Trim();

        if (trimmedDescription.Length > 1000)
        {
            throw new ArgumentException(
                "Description cannot exceed 1000 characters", nameof(description)
            );

        }
        Description = trimmedDescription;


    }

    public void ChangeStatus(TaskItemStatus status)
    {

        if (!Enum.IsDefined(status))
        {
            throw new ArgumentOutOfRangeException(nameof(status));

        }
        if (Status == status)
        {
            return;
        }
        if (status == TaskItemStatus.Done)
        {
            Progress = 100;
        }
        else if (Status == TaskItemStatus.Done)
        {
            Progress = 0;
        }


        Status = status;

    }

    public void UpdatePriority(TaskItemPriority priority)
    {
        if (!Enum.IsDefined(priority))
        {
            throw new ArgumentOutOfRangeException(nameof(priority));

        }
        Priority = priority;

    }

    public void UpdateProgress(int progress)
    {



        if (progress < 0 || progress > 100)
        {
            throw new ArgumentOutOfRangeException(
                 nameof(progress), " progress should be not less than 0 and more than 100."
            );
        }
        if (Status == TaskItemStatus.Done)
        {
            throw new InvalidOperationException("Cannot update progress of a completed task.");


        }
        Progress = progress;
    }

    public void UpdateDueDate(DateTime? dueDate)
    {
        if (dueDate == null)
        {
            DueDate = null;
            return;
        }

        if (dueDate.Value.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException(
             "Due date must be in UTC", nameof(dueDate)
            );
        }
        DueDate = dueDate;

    }

}
