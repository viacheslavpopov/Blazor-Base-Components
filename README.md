[![Build Status](https://dev.azure.com/Mobsites-US/Blazor%20Base%20Components/_apis/build/status/Build?branchName=master)](https://dev.azure.com/Mobsites-US/Blazor%20Base%20Components/_build/latest?definitionId=9&branchName=master)

# Blazor Base Components
by <a href="https://www.mobsites.com"><img align="center" src="./src/assets/mobsites-logo.png" width="36" height="36" style="padding-top: 20px;" />obsites</a>

Abstract components that provide a common member base to our various Blazor component types.

## For
* Our Blazor Components

## Dependencies

###### .NETStandard 2.0
* Microsoft.AspNetCore.Components (>= 3.1.3)
* Microsoft.AspNetCore.Components.Web (>= 3.1.3)

## Design and Development
The design and development of this shared Blazor component library was heavily guided by Microsoft's [Steve Sanderson](https://blog.stevensanderson.com/). He outlines a superb approach to building and deploying a reusable component library in this [presentation](https://youtu.be/QnBYmTpugz0) and [example](https://github.com/SteveSandersonMS/presentation-2020-01-NdcBlazorComponentLibraries).

This library allows common members (properties and event callbacks) and functionality (keeping state) to be shared across all of our current Blazor components and any future ones as well.

## Getting Started
1. Add [Nuget](https://www.nuget.org/packages/Mobsites.Blazor.BaseComponents/) package:

```shell
dotnet add package Mobsites.Blazor.BaseComponents --version 1.0.0
```

2. Choose a component base to inherit and use it to provide the basis for your next Blazor component.

## Recommendations
```c#
public abstract class StatefulComponent
```
Use this as the base of a top-level UI component that is self-contained and may need to keep state for itself or any of its dependents (if any).

```c#
public abstract class MainComponent
```
Use this as the base of a top-level UI component that is self-contained but does not need to keep state for itself or any of its dependents (if any).

```c#
public abstract class Subcomponent<T> where T : MainComponent
```
Use this as the base of a UI component that functions as a descendant to one of the above and does not make sense to be used on its own.

```c#
public abstract class ChildComponent<T> where T : IParentComponentBase
```
Use this as the base of a UI component that functions as a descendant to a `Subcomponent` or another `ChildComponent` and does not make sense to be used on its own.

```c#
public abstract class WrapperComponent
```
Use this as the base of a component that is needed to wrap or contain content outside of the main UI component with which it is associated.

***All other component bases in this library are for the foundation of all of the above, and, therefore, should not be used as a direct base for your component unless there is a use case that is not supported above.***