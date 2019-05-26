# AppSettings.Unity #

AppSettings.Unity is an extension to parse AppSettings in app.config( web.config) section into specified DOM. 

Default resolve your specified **appsetting model** as a Singleton (ContainerControlledLifetimemanager).

## **Usage**

### Add the package to your project.

Where you configure your **IUnityContainer** add this line (assume your custom appsetting model is **CustomAppSettings**):

```cs
Container.RegisterAppSettings<CustomAppSettings>();
```

### Resolve a dependent value

You can resove the appsetting model by Unity dependency injection.

- Construtor DI

    ```cs

    public class Demo
        {
            private readonly CustomAppSettings customAppSettings;

            public Demo(CustomAppSettings customAppSettings)
            {
                this.customAppSettings = customAppSettings;
            }

            public void DoSomething()
            {
                // Use the value of CustomAppSettings model.
                // ....
            }
    }
    ```

- Resolve a dependent value by configured Unity Container.

    ```cs
    var appSettings = Container.Resolve<CustomAppSettings>();
    ```