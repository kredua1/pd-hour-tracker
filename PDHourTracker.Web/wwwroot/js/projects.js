
// The function to call to create project list
function loadProjectsAsList() {
    $('#project-list').empty();
    getProjectsAsList();
}

// Create list of projects
function createProjectList(projects) {
    var ul = $('<ul/>');
    $.each(projects, function (index, project) {
        ul.append(formatProjectAsListItem(project));
    });

    $('#project-list').append(ul);
}

// Get projects from server: api/projects
function getProjectsAsList() {

    var result = $.ajax({
        url: '/api/projects',
        method: 'GET',
        contentType: 'application/json'
    });

    result.done(function (projects) {
        createProjectList(projects);
    });
}

// Format project as list item showing project name only
function formatProjectAsListItem(project) {
    return $('<li/>', { text: project.projectName });
}