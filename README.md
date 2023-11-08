### Multi-language Pulumi components using .NET

This is a sample project that shows how to create a multi-language component using .NET with a simple API to register inputs, components and their outputs. The `SimpleProvider` automatically generates and exposes a Pulumi schema from the user-defined types.

```cs
using Pulumi.Experimental.Provider;

var provider = 
    SimpleProvider
        .Create("test")
        .RegisterComponent<TestComponentArgs, TestComponent>("test:index:Test", 
            (request, args) => new TestComponent(request.Name, args))
        .Build();

await provider.Serve(args);
```


<details>
<summary>Implementation of <code>TestComponent</code> and <code>TestArgs</code></summary>

```cs
using Pulumi;
using Pulumi.Random;

public class TestComponentArgs : ResourceArgs
{
    /// <summary>
    /// The length of the password to generate.
    /// </summary>
    [Input("passwordLength")]
    public int PasswordLength { get; set; }
}

/// <summary>
/// A component resource representing a test component.
/// </summary>
public class TestComponent : ComponentResource
{
    /// <summary>
    /// The generated password.
    /// </summary>
    [Output("passwordResult")]
    public Output<string> PasswordResult { get; private set; }
    public TestComponent(string name, TestComponentArgs args) 
        : base("test:index:Test", name, args)
    {
        var password = new RandomPassword($"{name}-database-password", new ()
        {
            Length = args.PasswordLength
        }, new CustomResourceOptions 
        {
            Parent = this
        });

        PasswordResult = password.Result;
        RegisterOutputs();
    }
}
```
</details>


</br >

This project uses a local build of the [Pulumi .NET SDK](https://github.com/pulumi/pulumi-dotnet) built from branch `zaid/authoring-providers-from-components`.

It also uses a locally generated .NET SDK of the random provider so we can use its resources as tes examples.

### Build

Build the MLC using the following command:
```
cd src && dotnet publish
```
It will generate the MLC binary in the `./src/bin/Debug/net7.0/publish` folder which is then referneced by the YAML program:
```yaml
plugins:
  providers:
    - name: test
      path: ./bin/Debug/net7.0/publish
```
This works becaue YAML looks for a binary called `pulumi-resource-test` and we have set the option 
```xml
<AssemblyName>pulumi-resource-test</AssemblyName>
```
Which generates a binary with that assembly name.

### Run the Pulumi YAML program

<details>
<summary>See the <code>YAML</code> program</summary>

```yaml
name: testingdotnetmlc
runtime: yaml

plugins:
  providers:
    - name: test
      path: ./bin/Debug/net7.0/publish

resources:
  testmlc:
    type: test:index:Test
    properties:
      passwordLength: 20

outputs:
  length: ${testmlc.passwordResult}
```
</details>

</br >

```
cd src && pulumi up --yes --stack <your-stack-name>
```

### Get the generated provider schema from the binary

After building the binary using `dotnet publish`, you can run the following command to get the generated schema:
```
cd src && pulumi package get-schema bin/Debug/net7.0/publish/pulumi-resource-test
```

<details>
<summary>Generated Pulumi Schema</summary>

```json
{
  "name": "test",
  "version": "0.1.0",
  "meta": {
    "moduleFormat": "(.*)"
  },
  "config": {},
  "provider": {
    "type": "object"
  },
  "resources": {
    "test:index:Test": {
      "description": "A component resource representing a test component.",
      "properties": {
        "passwordResult": {
          "type": "string",
          "description": "The generated password.",
          "language": {
            "csharp": {
              "name": "PasswordResult"
            }
          }
        }
      },
      "type": "object",
      "required": [
        "passwordResult"
      ],
      "inputProperties": {
        "passwordLength": {
          "type": "integer",
          "description": "The length of the password to generate.",
          "language": {
            "csharp": {
              "name": "PasswordLength"
            }
          }
        }
      },
      "requiredInputs": [
        "passwordLength"
      ],
      "isComponent": true
    }
  }
}
```
</details>