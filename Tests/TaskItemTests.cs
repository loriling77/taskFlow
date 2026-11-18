using System.Reflection;
using taskFlow.Domain.Entities;
using taskFlow.Domain.Enums;
using Xunit;

namespace taskFlow.Domain.Tests;

public class TaskItemTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(50)]
    [InlineData(100)]
    public void UpdateProgress_WithValidValue_ShouldUpdateProgress(int progress)
    {
        var task = new TaskItem(
            Guid.NewGuid(),
            "Learn Unit Testing",
            null
        );
        task.UpdateProgress(progress);

        Assert.Equal(progress, task.Progress);
    }

    [Fact]
    public void UpdateProgress_WithInvalidValue_ShouldThrow()
    {
        var task = new TaskItem(
            Guid.NewGuid(),
            "Test",
            null
        );

        Assert.Throws<ArgumentOutOfRangeException>(
            () => task.UpdateProgress(110)
        );
    }

    [Fact]
    public void UpdateProgress_WhenTaskIsDone_ShouldThrowExecption()
    {
        var task = new TaskItem(
            Guid.NewGuid(),
            "Learn Unit Testing",
            null
        );
        task.ChangeStatus(TaskItemStatus.Done);

        Assert.Throws<InvalidOperationException>(() => task.UpdateProgress(50));
    }

    [Fact]
    public void ChangeStatus_WhenSetToDone_ShouldSetProgressTo100()
    {
        var task = new TaskItem(
            Guid.NewGuid(),
            "learn unit testing",
            null
        );

        task.ChangeStatus(TaskItemStatus.Done);


        Assert.Equal(TaskItemStatus.Done, task.Status);
        Assert.Equal(100, task.Progress);
    }
    [Fact]
    public void ChangeStatus_WhenReopeningTask_SHouldResetProgress()
    {
        var task = new TaskItem(
            Guid.NewGuid(),
            "Learn unit testing",
            null
        );
        task.ChangeStatus(TaskItemStatus.Done);
        task.ChangeStatus(TaskItemStatus.Doing);
        Assert.Equal(TaskItemStatus.Doing, task.Status);
        Assert.Equal(0, task.Progress);
    }

    [Fact]
    public void UpdateDueDate_WithUtcDate_ShouldUpdateDueDate()
    {
        var task = new TaskItem(
            Guid.NewGuid(),
            "learn unit testing",
            null
        );

        DateTime dueDate = DateTime.UtcNow.AddDays(7);

        task.UpdateDueDate(dueDate);
        Assert.Equal(dueDate, task.DueDate);
    }

    [Fact]
    public void UpdateDueDate_WithNull_ShouldClearDueDate()
    {
        var task = new TaskItem(
            Guid.NewGuid(),
            "Learn Unit Testing",
            null
        );

        task.UpdateDueDate(DateTime.UtcNow.AddDays(7));

        task.UpdateDueDate(null);
        Assert.Null(task.DueDate);

    }
    [Fact]
    public void UpdateDueDate_WithNonUtcDate_ShouldThrowExecption()
    {
        var task = new TaskItem(
            Guid.NewGuid(),
            "Learn new Item",
            null
        );

        DateTime dueDate = DateTime.UtcNow.AddDays(7);
        Assert.Throws<ArgumentException>(() => task.UpdateDueDate(dueDate));

    }

    [Fact]
    public void UpdatePriority_WithValidPriority_ShouldUpdatePriority()
    {
        var task = new TaskItem(
            Guid.NewGuid(),
            "learn unit testing",
            null
        );

        task.UpdatePriority(TaskItemPriority.High);
        Assert.Equal(TaskItemPriority.High, task.Priority);
    }


}
