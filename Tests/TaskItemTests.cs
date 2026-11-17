using System.Reflection;
using taskFlow.Domain.Entities;
using taskFlow.Domain.Enums;
using Xunit;

namespace taskFlow.Domain.Tests;

public class TaskItemTests
{
    [Fact]
    public void UpdateProgress_WithValidValue_ShouldUpdateProgress()
    {
        var task = new TaskItem(
            Guid.NewGuid(),
            "Learn Unit Testing",
            null
        );
        task.UpdateProgress(50);

        Assert.Equal(50, task.Progress);
    }

}
