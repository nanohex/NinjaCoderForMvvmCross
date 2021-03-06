﻿// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SolutionService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Services
{
    using System.Collections.Generic;
    using Entities;
    using EnvDTE;
    using EnvDTE80;
    using Extensions;
    using Interfaces;

    /// <summary>
    ///  Defines the SolutionService type.
    /// </summary>
    public class SolutionService : ISolutionService
    {
        /// <summary>
        /// The solution.
        /// </summary>
        private readonly Solution solution;

        /// <summary>
        /// The solution2,
        /// </summary>
        private readonly Solution2 solution2;

        /// <summary>
        /// Initializes a new instance of the <see cref="SolutionService"/> class.
        /// </summary>
        /// <param name="solution">The solution.</param>
        public SolutionService(Solution solution)
        {
            this.solution = solution;
            this.solution2 = solution as Solution2;
        }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        public string FullName
        {
            get { return this.solution.FullName; } 
        }

        /// <summary>
        /// Creates the empty solution.
        /// </summary>
        /// <param name="solutionPath">The solution path.</param>
        /// <param name="solutionName">Name of the solution.</param>
        public void CreateEmptySolution(
            string solutionPath, 
            string solutionName)
        {
            this.solution2.CreateEmptySolution(solutionPath, solutionName);
        }

        /// <summary>
        /// Gets the directory name.
        /// </summary>
        /// <returns>The Directory path.</returns>
        public string GetDirectoryName()
        {
            return this.solution2.GetDirectoryName();
        }

        /// <summary>
        /// Gets the project template.
        /// </summary>
        /// <param name="templateName">ProjectName of the template.</param>
        /// <returns>The path of the template.</returns>
        public string GetProjectTemplate(string templateName)
        {
            return this.solution2.GetProjectTemplate(templateName);
        }

        /// <summary>
        /// Gets the item template.
        /// </summary>
        /// <param name="templateName">ProjectName of the template.</param>
        /// <returns>The path of the template.</returns>
        public string GetProjectItemTemplate(string templateName)
        {
            return this.solution2.GetProjectItemTemplate(templateName);
        }

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="solutionFolder">The solution folder.</param>
        /// <param name="path">The path.</param>
        /// <returns>
        /// The project item.
        /// </returns>
        public IProjectItemService AddSolutionItem(
            string solutionFolder, 
            string path)
        {
            ProjectItem projectItem = this.solution2.AddSolutionItem(solutionFolder, path);

            return projectItem != null ? new ProjectItemService(projectItem) : null;
        }

        /// <summary>
        /// Adds the project to solution.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="templatePath">The template path.</param>
        /// <param name="projectName">ProjectName of the project.</param>
        public void AddProjectToSolution(
            string path, 
            string templatePath, 
            string projectName)
        {
            this.solution.AddProjectToSolution(path, templatePath, projectName);
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <returns>The projects.</returns>
        public IEnumerable<Project> GetProjects()
        {
            return this.solution2.GetProjects();
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="projectName">Name of the project.</param>
        /// <returns>The project.</returns>
        public Project GetProject(string projectName)
        {
            return this.solution2.GetProject(projectName);
        }

        /// <summary>
        /// Gets the project service.
        /// </summary>
        /// <param name="projectName">Name of the project.</param>
        /// <returns>The project service.</returns>
        public IProjectService GetProjectService(string projectName)
        {
            Project project = this.GetProject(projectName);

            return project != null ? new ProjectService(project) : null;
        }

        /// <summary>
        /// Adds the item template to project.
        /// </summary>
        /// <param name="templateInfos">The template infos.</param>
        /// <param name="createFolder">if set to <c>true</c> [create folder].</param>
        /// <returns> The messages. </returns>
        public IEnumerable<string> AddItemTemplateToProjects(
            IEnumerable<ItemTemplateInfo> templateInfos, 
            bool createFolder)
        {
            return this.solution2.AddItemTemplateToProjects(templateInfos, createFolder);
        }

        /// <summary>
        /// Removes the folder.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        public void RemoveFolder(string folderName)
        {
            this.solution2.RemoveFolder(folderName);
        }

        /// <summary>
        /// Removes the comments.
        /// </summary>
        public void RemoveFileHeaders()
        {
            this.solution2.RemoveFileHeaders();
        }

        /// <summary>
        /// Removes the comments.
        /// </summary>
        public void RemoveComments()
        {
            this.solution2.RemoveComments();
        }

        /// <summary>
        /// Gets the solution item path.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>The solution item path.</returns>
        public string GetSolutionItemPath(string file)
        {
            return this.solution2.GetSolutionItemPath(file);
        }

        /// <summary>
        /// Writes the file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="contents">The contents.</param>
        public void WriteFile(
            string path, 
            string contents)
        {
            this.solution2.WriteFile(path, contents);
        }

        /// <summary>
        /// Creates the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        public void CreateFile(
            string file, 
            string contents)
        {
            this.solution2.CreateFile(file, contents);
        }

        /// <summary>
        /// Gets the project item service.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The project item service.</returns>
        public IProjectItemService GetProjectItemService(string path)
        {
            IEnumerable<Project> projects = this.GetProjects();

            foreach (Project project in projects)
            {
                ProjectItem projectItem = project.GetProjectItem(path);

                if (projectItem != null)
                {
                    return new ProjectItemService(projectItem);
                }
            }

            return null;
        }
    }
}
