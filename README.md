# Propeller MVC [![Build Status](https://travis-ci.org/galtrold/propeller.mvc.svg?branch=master)](https://travis-ci.org/galtrold/propeller.mvc)

## What is this?
Propeller MVC is a small library created to improve the developer workflow when building Sitecore solutions. Sitecore provides standard rendering of Sitecore items but in the real world, you often need a more fine grained access to the data properties. Secondly, more and more data is flowing between the server and browser via asynchronous calls which means we need the raw data instead of the HTML presentation.  

# Getting started

This section covers the basic process of using the framework and consists of four steps. 

1. Define a ViewModel
2. Map Sitecore template
3. instantiate ViewModel
4. Render ViewModel

## 1. Define ViewModel
---
The ViewModel contains the UI properties and must inherit from `PropellerViewModel`. 

The base ViewModel requires a generic type which should be its self. The reason for this is to provide static typing in the razor-view (more on that later).    

```cs
public class CharacterViewModel : PropellerViewModel<CharacterViewModel>
{
    public string Species { get; set; }

    public string Gender { get; set; }

    public string Occupation { get; set; }

    public string Affiliation { get; set; }

    public string Homeworld { get; set; }

    public CharacterViewModel(){}

    public CharacterViewModel(Item dataItem) : base(dataItem){}

    public CharacterViewModel(Rendering rendering) : base(rendering){}

}
```

## 2. Map Sitecore template
---

Propeller MVC uses guids to map Sitecore Items to code. To lighting the burden of mapping, the framework uses a code-first approach which provides static typing. Each ViewModel is mapped using a ```ConfigurationMap``` class as shown where.
```cs
public class CharacterMap : ConfigurationMap<CharacterViewModel>
{
    public CharacterMap()
    {
        SetProperty(p => p.Species    ).Map(new ID("{FF80B838-B0E4-4266-9E6A-2918585C4EB7}"));
        SetProperty(p => p.Gender     ).Map(new ID("{7079E001-680A-460C-BB86-91E31C3EA2A5}"));
        SetProperty(p => p.Occupation ).Map(new ID("{73FF41AA-2ACF-45F8-9FA3-8C4F6374F217}"));
        SetProperty(p => p.Affiliation).Map(new ID("{EE444D4A-7FDF-4D94-8ABA-6054A898E721}"));
        SetProperty(p => p.Homeworld  ).Map(new ID("{E8D236D1-E473-4816-89BD-EE4CCD613972}"));
    }
}
```

## 3. Instantiate ViewModel
----
The Controller part is the simplest step. Basically the Controller has two tasks, 1) instantiate the ViewModel 2) provide the Sitecore item for the ViewModel.
Generally this item ( regardless of it being the page item or rendering item) is accessible via the RenderingContext and the ViewModel will automatically  resolve this.

```cs
public class CharacterController : Controller
{
    public ActionResult Index()
    {
        var characterViewModel = new CharacterViewModel(RenderingContext.Current.Rendering);
        return View(characterViewModel);
    }
}
```

## 4. Render ViewModel
---
Use the ViewModel as the ```@model``` in the razor view. In order to support Sitecores Experience editor the ViewModel provides a ```Render``` method which uses an expression for selecting the property to render. 

```html
<form class="form-horizontal">
    <div class="form-group">
        <label class="col-sm-2 control-label">Name</label>
        <div class="col-sm-10">
            <p class="form-control-static">@Model.DisplayName</p>
        </div>
        <label class="col-sm-2 control-label">Species</label>
        <div class="col-sm-10">
            <p class="form-control-static">@Model.Render(p => p.Species)</p>
        </div>
        <label class="col-sm-2 control-label">Gender</label>
        <div class="col-sm-10">
            <p class="form-control-static">@Model.Render(p => p.Gender)</p>
        </div>
    </div>
</form>
```

# Documentation

1. Configuration
2. ViewModel

## 1. Configuration

Configuration is mainly about mapping the Model with the Sitecore Item. While it is possible to do this mapping by using the template ___field name___ and the ViewModels ___property name___ (thus using a convension based mapping), a decision was made not to use this approch. The reason being that renaming any property (in code or Sitecore) would break the mapping. Secondly it would make it harder to debug, especially with identical property names in different parts of the solution. Using item guids is a much more solid solution and makes it easy to trace down the mapping.

The actual task of mapping is done by using a  ```ConfigurationMap``` which takes a generic type defining the ViewModel to map and must be done for every ViewModel.
```cs
public class CharacterMap : ConfigurationMap<CharacterViewModel>
{
    public CharacterMap()
    {
        SetProperty(p => p.Species    ).Map(new ID("{FF80B838-B0E4-4266-9E6A-2918585C4EB7}"));
        SetProperty(p => p.Gender     ).Map(new ID("{7079E001-680A-460C-BB86-91E31C3EA2A5}"));
        SetProperty(p => p.Occupation ).Map(new ID("{73FF41AA-2ACF-45F8-9FA3-8C4F6374F217}"));
        SetProperty(p => p.Affiliation).Map(new ID("{EE444D4A-7FDF-4D94-8ABA-6054A898E721}"));
        SetProperty(p => p.Homeworld  ).Map(new ID("{E8D236D1-E473-4816-89BD-EE4CCD613972}"));
    }
}
```

### 1.1 Basic mapping
The mapping takes place inside the Constructor method where the ```SetProperty()``` method dictates which property to map. This method returns a configuration object enabling multiple configurations on the same property by chaining.

The first configuration method is the ```Map()``` method and is the only required configuration. It simply maps the property with the Sitecore template field Id, like this:

```cs
SetProperty(p => p.Affiliation).Map(new ID("{EE444D4A-7FDF-4D94-8ABA-6054A898E721}"));
```
This mapping only registers the field Id together with the ViewModel property. This means that the ViewModel properties remain null/empty but you can use the ViewModels ```Render()``` in the Razor view to output the content as html.

### 1.2 Include values
If you want to populate the ViewModel with actual data, the configuration system provides at method automating this. The configuration method is called ```Include()```.
```cs
SetProperty(p => p.Affiliation).Map(new ID("{EE444D4A-7FDF-4D94-8ABA-6054A898E721}")).Include();
```

### 1.3 Editable values
The ViewModel contains a ```Commit()``` method which saves edited properties. It will however only work on properties which have been configured with the ```Editable()``` method (Editable configuration implicitly triggers the Include configuration).

```cs
SetProperty(p => p.Affiliation).Map(new ID("{EE444D4A-7FDF-4D94-8ABA-6054A898E721}")).Editable();
```

### 1.4 Inheritance 
ViewModels should not inherit from other ViewModels. If a base class is needed one should use an ```Interface``` to define the base properties. In order to only map the base class properties once, configuration is applied to the Interface and not the ViewModel implmenting the Interface.  

```cs
public class BaseViewModelMap : ConfigurationMap<ICharacterViewModel>
{
    public CharacterMap()
    {
        SetProperty(p => p.Species    ).Map(new ID("{FF80B838-B0E4-4266-9E6A-2918585C4EB7}"));
        SetProperty(p => p.Gender     ).Map(new ID("{7079E001-680A-460C-BB86-91E31C3EA2A5}"));
        SetProperty(p => p.Occupation ).Map(new ID("{73FF41AA-2ACF-45F8-9FA3-8C4F6374F217}"));
        SetProperty(p => p.Affiliation).Map(new ID("{EE444D4A-7FDF-4D94-8ABA-6054A898E721}"));
        SetProperty(p => p.Homeworld  ).Map(new ID("{E8D236D1-E473-4816-89BD-EE4CCD613972}"));
    }
}
```

## 2. ViewModel

This ViewModel can render the data in a number of ways. We will cover these methods with a series of use cases.

### 2.1 Default properties
Without doing anything you have access to the ItemName, DisplayName and url. 
### 2.2 Basic Sitecore rendering
The ViewModel has a Render method which uses Sitecores FieldRenderer.Render method. This means you get the standard rendering provided by Sitecore which also supports the Experience editor for editing content. The “problem” with Sitecores render method is that you need to provide it the field Id for the property you want to render or the property name (in the configuration chapter we discussed why we avoid field name as an identifier). The ViewModel hides the field identification away by providing an expression with a generic type of its self.  This enables static typing of the property to render. Because the ConfigurationMap has mapped the property name (fully qualified propertyname) with the field, the ViewModel is able to lookup the field Id based on the expression. No more magic Ids or Constant files lying around. The ViewModel provides a tight mapping to the Sitecore item which cannot ‘leak’ outside the scope of the 
```cs
ViewModel.Model.Render(p => p.Species)
```

### 2.3 Referenced items
Basic rendering works great with simple data types but are not very useful when dealing with referenced data items. If you were to Render a DropLink or TreeList, the result be the Id of referenced item(s), not  particular useful. In order to deal with referenced items, the ViewModel has the GetItemReference method.
```cs 
TP GetItemReference<TP>(Expression<Func<T, object>> expression) 
```
It returns the generic type it is provided (the type mush be a ViewModel) and as the Render method it provides an expression to specifi which property holds the referenced item. The returned ViewModel is now available for further rendering.
The benefits of GetItemReference is 1) referenced items are automatically instantiated as complex objects and 2) you get static typing throughout the whole object tree.
### 2.4 
…to be continued.


