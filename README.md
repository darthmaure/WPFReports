# WPFReports
It is a simple WPF application showing how to write custom XAML/C# reports (or widgets - I just use both names). It also show some WPF/C# methods and solutions.


# Getting started
Clone the repository. Build solution and simple run using F5.


# Built with
* MVVMLight - most popular MVVM framework, mvvmlight.net

* MahApps.Metro - my favorite library of pretty controls, mahapps.com

* ModernUICharts - greate library of modern-looking charts for WPF/Silverlight/UWP, modernuicharts.codeplex.com

* Newtonsoft.Json - library to parse json data, newtonsoft.com

* Moq - for testing, github

* PKCode.Scripting - quite old library written by me to execute C# code


# How to use code
The solution contains two projects

* Statistics.Core.Widgets - this project contains whole reporting engine. Definitions and implementations of all interfaces/classes that are needed to work with widgets. Tha main iterface is _IWidgetService_, that can be used to load and run widgets from files. To work with designer, it is needed to use _IWidgetManagerService_. It simple loads, creates, saves and deletes widgets.

* WPFReports - this is an sample projects that shows how to use widgets library. I tried to use SOLID principles and MVVM. I put here some examples of real life WPF solutions (converters, navigation service, notification service, styles, behavior to bind CLR property of textbox etc.).

# Screenshots
Main menu

![Main menu](https://github.com/darthmaure/WPFReports/blob/master/src/Images/MainMenu.PNG "Main menu")

Loading items in view mode

![Laoding](https://github.com/darthmaure/WPFReports/blob/master/src/Images/LoadingItems.PNG "Loading items in view mode")

Display page. Simple widget with custom WPF control

![Display](https://github.com/darthmaure/WPFReports/blob/master/src/Images/Display.PNG "Display page. Simple widget with custom WPF control")

Designer. Report code

![Designer](https://github.com/darthmaure/WPFReports/blob/master/src/Images/DesignerCode.PNG "Designer. Report code")

Designer. Report layout XAML code

![Designer 2](https://github.com/darthmaure/WPFReports/blob/master/src/Images/DesignerLayout.PNG "Designer. Report layout XAML code")

Designer. Preview of created report

![Designer 3](https://github.com/darthmaure/WPFReports/blob/master/src/Images/DesignerPreview.PNG "Designer. Preview of created report")


# License
?
