function RenderData(data) {
  try {
    var temptasks = JSON.parse(data);
    let tasks = [];
    if (temptasks) {
      temptasks.forEach(p => {
        var tempTask = {
          id: p.id,
          label: p.label,
          start: new Date(p.start),
          duration: p.duration,
          progress: p.progress,
          type: p.type
        };
        if (p.description !== "") {
          tempTask.description = p.description;
          tempTask.descriptionVisible = true;
        } else {
          tempTask.descriptionVisible = false;
        }
        if (p.dependentOn && p.dependentOn.length > 0) {
          tempTask.dependentOn = p.dependentOn;
        }
        tasks.push(tempTask);
      });
    }
    Window.Instance.UpdateTasks(tasks);
  } catch (err) {
    console.error(err);
  }
}
