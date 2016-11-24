# Propeller MVC

## What is this?
Propeller MVC is a small library created to improve the developer workflow when building Sitecore solutuins. Sitecore provides standard rendering of Sitecore items but in the real world, elements sometimes require . Secondly, more and more data is flowing between the server and browser via asynchronous calls  

# Getting started
Using this framework is a four step process. 

1. Define a ViewModel
2. Map Sitecore template
3. Instansiate ViewModel
4. Render ViewModel

## 1. Define ViewModel
---
The ViewModel is a class containing rendering properties and logic. If only the raw data is needed the ViewModel should inherit from either `PropellerModel`. To use the ViewModel in the razor view, the ViewModel must inherit from `PropellerViewModel`.

The base ViewModel requires a generic type which should be its self. The reason for this is to provide static typing in the razor-view (more on that later).    

```cs
public class CharacterViewModel : PropellerViewModel<CharacterViewModel>
{
    public string Species { get; set; }

    public string Genger { get; set; }

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

```cs
public class CharacterMap : ConfigurationMap<CharacterViewModel>
{
    public CharacterMap()
    {
        SetProperty(p => p.Species    ).Map(new ID("{FF80B838-B0E4-4266-9E6A-2918585C4EB7}"));
        SetProperty(p => p.Genger     ).Map(new ID("{7079E001-680A-460C-BB86-91E31C3EA2A5}"));
        SetProperty(p => p.Occupation ).Map(new ID("{73FF41AA-2ACF-45F8-9FA3-8C4F6374F217}"));
        SetProperty(p => p.Affiliation).Map(new ID("{EE444D4A-7FDF-4D94-8ABA-6054A898E721}"));
        SetProperty(p => p.Homeworld  ).Map(new ID("{E8D236D1-E473-4816-89BD-EE4CCD613972}"));
    }
}
```

## 3. Instansiate ViewModel
----
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
            <p class="form-control-static">@Model.Render(p => p.Genger)</p>
        </div>
    </div>
</form>
```

# Documentation

1. [ViewModel](#example)
2. [Configuration](#example2)
3. [dsd](#third-example)