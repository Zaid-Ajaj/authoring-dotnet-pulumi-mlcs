### Multi-language Pulumi components using .NET

This is a sample project that shows how to create a multi-language component using .NET with a simple API to register inputs, components and their outputs. The `SimpleProvider` automatically generates and exposes a Pulumi schema from the user-defined types.

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

```
cd src && pulumi up --yes --stack <your-stack-name>
```

### Get the generated provider schema from the binary

After building the binary using `dotnet publish`, you can run the following command to get the generated schema:
```
cd src && pulumi package get-schema bin/Debug/net7.0/publish/pulumi-resource-test
```