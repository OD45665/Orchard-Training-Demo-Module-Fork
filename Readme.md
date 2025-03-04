# Lombiq Training Demo for Orchard Core

## About

Demo [Orchard Core](https://orchardcore.net/) module for training purposes guiding you to become an Orchard developer. Note that this module also has an Orchard 1.x version in the [dev-orchard-1 branch of the repository](https://github.com/Lombiq/Orchard-Training-Demo-Module/tree/dev-orchard-1).

If you prefer tutorial videos more then check out the [Dojo Course 3, the complete Orchard Core tutorial series](https://orcharddojo.net/orchard-training/dojo-course-3-the-full-orchard-core-tutorial).

**If you are interested in training resources and personalized Orchard training please visit [Orchard Dojo](https://orcharddojo.net/).**

## Prerequisites

The module assumes that you have a good understanding of basic Orchard concepts, and that you can get around the Orchard admin area (the [official documentation](https://docs.orchardcore.net/) and the [Dojo Course 3 tutorial series](https://orcharddojo.net/orchard-training/dojo-course-3-the-full-orchard-core-tutorial) may help you with that). You should also be familiar with how to use Visual Studio (or any other C# IDE) and write C#, as well as the concepts of ASP.NET Core MVC.

Before you dive deep into this module it'd be best if you make sure that you have done the following:

- You know how ASP.NET Core MVC works. It's important that you understand how ASP.NET Core MVC works or generally what MVC is about. If you are not familiar with the topic take a look at the tutorials at <https://learn.microsoft.com/en-us/aspnet/core/>.
- You've read through the documentation under <https://docs.orchardcore.net/> (at least the "About Orchard Core" section, but it would be great if you'd skim the whole documentation).
- You know Orchard Core from a user's perspective and understand the fundamental concepts underlying the system. (The [Dojo Course 3 tutorial series](https://orcharddojo.net/orchard-training/dojo-course-3-the-full-orchard-core-tutorial) may help you with that.)

## How to start learning Orchard Core with this module

The module comes with its own standalone solution and web app. So, to run it, simply do the following:

1. Clone the latest `dev` branch of this repository or [download the source](https://github.com/Lombiq/Orchard-Training-Demo-Module/archive/refs/heads/dev.zip).
2. Open the solution in your favorite IDE, like [Visual Studio](https://visualstudio.microsoft.com/), [Visual Studio Code](https://code.visualstudio.com/) or [Rider](https://www.jetbrains.com/rider/).
3. Make sure the `Lombiq.TrainingDemo.Web` project is the startup project (it should be; not needed for VS Code).
4. Start the app.
   - In Visual Studio and VS Code, you can do this with <kbd>Ctrl</kbd> + <kbd>F5</kbd>.
   - From the .NET CLI, run `dotnet run` in the _Lombiq.TrainingDemo.Web_ folder and then open <https://localhost:5001/> in a browser.
5. The site will be automatically set up with the "Training Demo" recipe (since we use Orchard Core's [Auto Setup feature](https://docs.orchardcore.net/en/latest/docs/reference/modules/AutoSetup/)). You'll be able to log in with the username "admin" and password "Password1!".

Once the app is running:

- Head over to the **[StartLearningHere.md](Lombiq.TrainingDemo/StartLearningHere.md)** file and start exploring all the great things you can do in Orchard Core.
- If you are brave enough to not follow any guide or you want to start the guide from somewhere else then go to the **[Map.cs](Lombiq.TrainingDemo/Map.cs)** file and jump to any class you are interested in. [StartLearningHere.md](Lombiq.TrainingDemo/StartLearningHere.md) also has training sections linked so you can go to a broader section.
- If you'd like to clean out comments from code files so you can just see the essence, then use the [Comment Remover VS extension](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.CommentRemover) to quickly do it.
- Take a look at the Recipes menu in the Admin screen to load in additional sample content. This is not included by default to reduce clutter.
- This module uses [Lombiq Helpful Libraries for Orchard Core](https://github.com/Lombiq/Helpful-Libraries) to make a few things simpler. But don't worry, we'll explain it.

## Further resources

Some further reading if you're hungry for more knowledge.

- You can also follow the [Dojo Course 3 tutorial series](https://orcharddojo.net/orchard-training/dojo-course-3-the-full-orchard-core-tutorial) if you like to learn from videos.
- This module is not about showing you how to create an Orchard Core application from scratch. For that, we recommend you use the `Initialize-OrchardCoreSolution` script from our [Utility Scripts project](https://github.com/Lombiq/Utility-Scripts).
- Keep in mind that your best living reference for how to do something in Orchard is [the official repo](https://github.com/OrchardCMS/OrchardCore) and our [Open-Source Orchard Core Extensions](https://github.com/Lombiq/Open-Source-Orchard-Core-Extensions) (OSOCE) solution. Clone both and keep the solutions open when you’re working on something so you can quickly look up anything. As a bonus, OSOCE also contains all of Lombiq's open-source Orchard themes and modules! Check it out for what we've already solved for you.
- You can also take a look at our [Walkthroughs](https://github.com/Lombiq/Orchard-Walkthroughs) module, for step-by-step walkthroughs, which are guided by pop-up windows right there in the UI.
- Be sure to check out the [Orchard Dojo Library for Orchard Core](https://orcharddojo.net/orchard-resources/CoreLibrary/) for a wealth of Orchard Core guidelines, best practices, development utilities (like scripts and snippets), and more as well!
- This project utilizes several [GitHub Actions](https://docs.github.com/en/actions) workflows, like [Build and Test Orchard Core solution](https://github.com/Lombiq/GitHub-Actions/blob/dev/Docs/Workflows/BuildDotNetCoreOrchardCore/BuildAndTestOrchardCoreSolution.md) to provide CI builds. We recommend using such workflows from our [Lombiq GitHub Actions project](https://github.com/Lombiq/GitHub-Actions) for automation.
- If you're ready to make a jump to fully automated Quality Assurance beyond unit testing as demonstrated here, check out the following projects:
  - [Lombiq .NET Analyzers](https://github.com/Lombiq/.NET-Analyzers): Orchard Core-optimized static code analysis of all C# code during editing and build.
  - [Lombiq Node.js Extensions](https://github.com/Lombiq/NodeJs-Extensions): Linting, build, and minification of SCSS/JS/Markdown files, integrated into the .NET build.
  - [Lombiq Testing Toolbox for Orchard Core](https://github.com/Lombiq/Testing-Toolbox): Helps you write unit tests for Orchard Core projects.
  - [Lombiq UI Testing Toolbox for Orchard Core](https://github.com/Lombiq/UI-Testing-Toolbox): Web UI testing toolbox for Orchard Core applications. Everything you need to do UI testing with Selenium for an Orchard app is there, including accessibility checks and security scanning.
  - [Lombiq GitHub Actions](https://github.com/Lombiq/GitHub-Actions): Ready-to-use, developer-friendly GitHub Actions CI/CD workflows that build Orchard Core projects, run static code analysis, execute unit/UI tests, and provide reports and artifacts. Automation for deploying and operating Azure-hosted apps is also included, as well as YAML linting.
  - [Lombiq PowerShell Analyzers](https://github.com/Lombiq/PowerShell-Analyzers): Static code analysis for PowerShell scripts, even if the scripts are part of an Orchard Core solution.
  - [Lombiq Hosting - Azure Application Insights for Orchard Core](https://github.com/Lombiq/Orchard-Azure-Application-Insights): Orchard Core module that enables easy integration of Azure Application Insights into Orchard. Detailed telemetry is collected about the app, enriched with Orchard-specific data points.

## Contributing and support

Bug reports, feature requests, comments, questions, code contributions and love letters are warmly welcome. You can send them to us via GitHub issues and pull requests. Please adhere to our [open-source guidelines](https://lombiq.com/open-source-guidelines) while doing so.

When adding new tutorials please keep in mind the following:

- Insert tutorial steps into the existing flow, either at the end or between two existing ones. Use "NEXT STATION" comments to indicate the next file the reader should check out.
- If it's a new training section then indicate as such by an "END OF TRAINING SECTION" comment at the end and add it to the list under [StartLearningHere.md](Lombiq.TrainingDemo/StartLearningHere.md).
- Add pointers to its classes/files in _Map.cs_.

This project is developed by [Lombiq Technologies](https://lombiq.com/). Commercial-grade support is available through Lombiq.
