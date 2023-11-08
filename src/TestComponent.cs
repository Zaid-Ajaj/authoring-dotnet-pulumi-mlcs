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