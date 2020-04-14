[![Build Status](https://dev.azure.com/Mobsites-US/Blazor%20Base%20Components/_apis/build/status/Build?branchName=master)](https://dev.azure.com/Mobsites-US/Blazor%20Base%20Components/_build/latest?definitionId=9&branchName=master)

# Blazor Base Components
by <a href="https://www.mobsites.com"><img align="center" src="./src/assets/mobsites-logo.png" width="36" height="36" style="padding-top: 20px;" />obsites</a>

Abstract components that provide a common member base to our various Blazor component types.

## For
* Our Blazor Components

## Dependencies

###### .NETStandard 2.0
* Microsoft.AspNetCore.Components (>= 3.1.2)
* Microsoft.AspNetCore.Components.Web (>= 3.1.2)

## Design and Development
The design and development of this shared Blazor component library was heavily guided by Microsoft's [Steve Sanderson](https://blog.stevensanderson.com/). He outlines a superb approach to building and deploying a reusable component library in this [presentation](https://youtu.be/QnBYmTpugz0) and [example](https://github.com/SteveSandersonMS/presentation-2020-01-NdcBlazorComponentLibraries).

This library allows common members (properties and event callbacks) and functionality (keeping state) to be shared across all of our current Blazor components and any future ones as well.

## Getting Started
1. Add [Nuget](https://www.nuget.org/packages/Mobsites.Blazor.BaseComponents/) package:

```shell
dotnet add package Mobsites.Blazor.BaseComponents --version 1.0.0-preview1
```

2. Choose a component base to inherit and use it to provide the basis for your next Blazor component.

## Recommendations
```c#
public abstract class MainComponent
```
Use this as the base of a component that is self-contained, may need to keep state, and is likely to have dependents. An example is `<AppDrawer>`.

```c#
public abstract class OrphanComponent<T> where T : MainComponent
```
Use this as the base of a component that is self-contained, may need to keep state, may or may not have dependents, but is likely to be used in other main components. This allows such a component to pick up on state changes, such as dark or light mode, automagically when nested in other main components. An example is `<Button>` or `<Icon>`.

```c#
public abstract class ChildComponent<T> where T : MainComponent
```

Use this for a dependent that **must** be a descendant to the main component that you are building, and is likely itself to have children. An example is `<AppDrawerContent>`.

```c#
public abstract class GrandChildComponent<T> where T : IParentComponentBase
```

Use this for a dependent that **must** be a descendant of one of the above and is unlikely to have children. An example is `<AppDrawerContentDivider>`.

```c#
public abstract class WrapperComponent
```
Use this as the base of a component that is necessary to enforce css rules or other arrangements outside of the main component with which it is associated. An example is `<AppContent>`.

***All other component bases in this library are for the foundation of all of the above, and, therefore, should not be used as a direct base for your component unless there is a use case that is not supported above.***